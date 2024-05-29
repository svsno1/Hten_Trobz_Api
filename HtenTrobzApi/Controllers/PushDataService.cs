using HtenTrobzApi.Models;
using HtenTrobzApi.TruckModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace HtenTrobzApi
{
    public class PushDataService : BackgroundService
    {
        private readonly MyConfigs _configs;

        public PushDataService(IOptions<MyConfigs> configs)
        {
            _configs = configs.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _ = PushData();
                _ = PushWeightData();

                await Task.Delay(TimeSpan.FromSeconds(_configs.IntervalTimer), stoppingToken);
            }
        }

        public async Task PushData()
        {
            var context = new HtenContext();

            try
            {
                var tickets = context.Tickets
                    .Where(t => t.Sync != "2" && t.DateTimeMix >= _configs.FromDate)
                    .Take(_configs.MaxRecord)
                    .ToList();

                var deliveryData = tickets.Select(ticket => new
                {
                    YourID = ticket.TicketNo + "_" + ticket.CodePlant + "_" + ticket.PlantNo,
                    VcNo = ticket.OrderDescription01,
                    CusCode = ticket.CustomerCode,
                    JobCode = ticket.SiteCode,
                    VehicleCode = ticket.TruckCode,
                    DriverCode = ticket.DriverCode,
                    ContractCode = ticket.TicketNo,
                    Note = ticket.Note,
                    VcDate = (ticket.DateTimeMix ?? new DateTime(1900, 1, 1)).ToString("yyyy-MM-dd"),
                    Status = "done",
                    Items = new List<object>
                    {
                        new
                        {
                            Code = ticket.RcDescription01,
                            Quantity = ticket.M3ThisTicket,
                            Uom = "m3",
                            Sl_Dat = ticket.M3Ordered,
                            AccumulatedQTY = ticket.M3Delivered
                        }
                    }
                }).ToList();

                var jsonDeliveryData = JsonConvert.SerializeObject(deliveryData);

                var issueData = (from ticket in tickets
                                 join materialArch in context.MaterialArches
                                 on new { ticket.CodePlant, ticket.PlantNo, SheetNo = (long?)ticket.SheetNo }
                                 equals new { materialArch.CodePlant, materialArch.PlantNo, SheetNo = materialArch.SheetId }
                                 select new
                                 {
                                     ticket.Idticket,
                                     ticket.TicketNo,
                                     ticket.CodePlant,
                                     ticket.PlantNo,
                                     ticket.CustomerCode,
                                     ticket.SiteCode,
                                     ticket.TruckCode,
                                     ticket.DriverCode,
                                     ticket.OrderDescription01,
                                     ticket.Note,
                                     ticket.DateTimeMix,
                                     ticket.M3Ordered,
                                     ticket.M3Delivered,
                                     ticket.RcDescription01,
                                     materialArch.Id,
                                     materialArch.CodeMaterial,
                                     materialArch.NameMaterial,
                                     materialArch.PvActualy,
                                     materialArch.UnitMaterial
                                 }).ToList();

                var groupedIssueData = issueData
                    .GroupBy(ticket => new
                    {
                        ticket.Idticket,
                        ticket.TicketNo,
                        ticket.CodePlant,
                        ticket.PlantNo,
                        ticket.CustomerCode,
                        ticket.SiteCode,
                        ticket.TruckCode,
                        ticket.DriverCode,
                        ticket.OrderDescription01,
                        ticket.Note,
                        ticket.DateTimeMix,
                        ticket.M3Ordered,
                        ticket.M3Delivered,
                        ticket.RcDescription01
                    })
                    .Select(g => new
                    {
                        YourID = g.Key.TicketNo + "_" + g.Key.CodePlant + "_" + g.Key.PlantNo,
                        VcNo = g.Key.OrderDescription01,
                        CusCode = g.Key.CustomerCode,
                        JobCode = g.Key.SiteCode,
                        VehicleCode = g.Key.TruckCode,
                        DriverCode = g.Key.DriverCode,
                        Note = g.Key.Note,
                        ProductCode = g.Key.RcDescription01,
                        Items = g.Select(materialArch => new
                        {
                            Code = materialArch.CodeMaterial,
                            Name = materialArch.NameMaterial,
                            Quantity = materialArch.PvActualy,
                            Uom = materialArch.UnitMaterial
                        }).ToList()
                    })
                    .ToList();

                var jsonIssueData = JsonConvert.SerializeObject(groupedIssueData);

                using (var client = new HttpClient())
                {
                    var deliveryContent = new StringContent(jsonDeliveryData, Encoding.UTF8, "application/json");
                    var deliveryRequest = new HttpRequestMessage(HttpMethod.Post, "https://phudoanh-staging.trobz.com/setDelivery")
                    {
                        Content = deliveryContent
                    };
                    deliveryRequest.Headers.Add("Authorization", "b972feef41f0f0d55acfebf0985ef1b89fb12c84");
                    var deliveryResponse = await client.SendAsync(deliveryRequest);
                    deliveryResponse.EnsureSuccessStatusCode();
                    var deliveryResult = await deliveryResponse.Content.ReadAsStringAsync();
                    var deliveryRes = JsonConvert.DeserializeObject<TrobzResponse>(deliveryResult);

                    var issueContent = new StringContent(jsonIssueData, Encoding.UTF8, "application/json");
                    var issueRequest = new HttpRequestMessage(HttpMethod.Post, "https://phudoanh-staging.trobz.com/setIssue")
                    {
                        Content = issueContent
                    };
                    issueRequest.Headers.Add("Authorization", "b972feef41f0f0d55acfebf0985ef1b89fb12c84");
                    var issueResponse = await client.SendAsync(issueRequest);
                    issueResponse.EnsureSuccessStatusCode();
                    var issueResult = await issueResponse.Content.ReadAsStringAsync();
                    var issueRes = JsonConvert.DeserializeObject<TrobzResponse>(issueResult);

                    if (deliveryRes != null && issueRes != null)
                    {
                        if (deliveryRes.error == null && issueRes.error == null)
                        {
                            tickets.ForEach(ticket =>
                            {
                                ticket.Sync = "2";
                            });

                            context.SaveChanges();
                        }
                        else
                        {
                            if (deliveryRes.error != null)
                            {
                                FastErrorLog log = new FastErrorLog()
                                {
                                    Message = "setDelivery: " + deliveryRes.error,
                                    CreatedDate = DateTime.Now,
                                };
                                context.FastErrorLogs.Add(log);
                            }

                            if (issueRes.error != null)
                            {
                                FastErrorLog log = new FastErrorLog()
                                {
                                    Message = "setIssue: " + issueRes.error,
                                    CreatedDate = DateTime.Now,
                                };
                                context.FastErrorLogs.Add(log);
                            }

                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FastErrorLog log = new FastErrorLog()
                {
                    Message = ex.Message,
                    CreatedDate = DateTime.Now,
                };

                context.FastErrorLogs.Add(log);
                context.SaveChanges();
            }
        }

        public async Task PushWeightData()
        {
            var context = new HtenContext();
            var truckContext = new TruckContext();

            try
            {
                var query = (from ticket in truckContext.TblTickets
                             join material in truckContext.TblMaterials on ticket.MaterialRef equals material.Id
                             join provider in truckContext.TblProviders on ticket.ProviderRef equals provider.Id
                             join user in truckContext.TblUsers on ticket.GrossOperConfirm equals user.Id into userGroup
                             from user in userGroup.DefaultIfEmpty()
                             join delivery in truckContext.TblTicketReceivedCbps on ticket.SheetNoCbp equals delivery.Idticket into deliveryGroup
                             from delivery in deliveryGroup.DefaultIfEmpty()
                             where ticket.Sync != "2" && ticket.GrossDatetime >= _configs.FromDate
                             select new
                             {
                                 ticket.Code,
                                 POCode = delivery.OrderDescription01,
                                 SOCode = delivery.OrderNo,
                                 MatCode = material.Code,
                                 MatName = material.Name,
                                 ticket.MatGroup,
                                 ticket.NetWeight,
                                 Uom = "kg",
                                 VendorName = provider.Name,
                                 TruckPlateNo = ticket.TruckPlateNumber,
                                 GrossDatetime = (ticket.GrossDatetime ?? new DateTime(1900, 1, 1)).ToString("yyyy-MM-dd HH:mm:ss"),
                                 ticket.DriverName,
                                 EmployeeName = user.UserName,
                                 LeadSealNo = ticket.KepChi,
                                 Note = ticket.KepChi
                             })
                         .Take(_configs.MaxRecord)
                         .ToList();

                var jsonResult = JsonConvert.SerializeObject(query);

                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://phudoanh-staging.trobz.com/setWeighbridge")
                    {
                        Content = content
                    };
                    request.Headers.Add("Authorization", "b972feef41f0f0d55acfebf0985ef1b89fb12c84");
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<TrobzResponse>(result);

                    if (res != null)
                    {
                        if (res.error == null)
                        {
                            var ticketCodes = query.Select(q => q.Code).ToList();
                            var ticketsToUpdate = truckContext.TblTickets.Where(t => ticketCodes.Contains(t.Code)).ToList();

                            foreach (var ticket in ticketsToUpdate)
                            {
                                ticket.Sync = "2";
                            }

                            truckContext.SaveChanges();
                        }
                        else
                        {
                            FastErrorLog log = new FastErrorLog()
                            {
                                Message = "setWeighbridge: " + res.error,
                                CreatedDate = DateTime.Now,
                            };
                            context.FastErrorLogs.Add(log);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FastErrorLog log = new FastErrorLog()
                {
                    Message = ex.Message,
                    CreatedDate = DateTime.Now,
                };

                context.FastErrorLogs.Add(log);
                context.SaveChanges();
            }
        }
    }
}
