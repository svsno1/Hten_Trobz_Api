using HtenTrobzApi.Models;
using HtenTrobzApi.TruckModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
                _ = PushDeliveryData();
                _ = PushIssueData();
                _ = PushWeightData();

                await Task.Delay(TimeSpan.FromSeconds(_configs.IntervalTimer), stoppingToken);
            }
        }

        public async Task PushDeliveryData()
        {
            var context = new HtenContext();

            try
            {
                var tickets = context.Tickets.Where(t => t.Sync != "2" && t.DateTimeMix >= _configs.FromDate).Take(_configs.MaxRecord).ToList();
                var queryDelivery = tickets.Select(ticket => new
                {
                    YourID = ticket.Idticket.ToString(),
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

                var jsonResultDelivery = JsonConvert.SerializeObject(queryDelivery);

                using (var client = new HttpClient())
                {
                    var contentDelivery = new StringContent(jsonResultDelivery, Encoding.UTF8, "application/json");
                    var requestDelivery = new HttpRequestMessage(HttpMethod.Post, "https://phudoanh-staging.trobz.com/setDelivery")
                    {
                        Content = contentDelivery
                    };
                    requestDelivery.Headers.Add("Authorization", "b972feef41f0f0d55acfebf0985ef1b89fb12c84");
                    var responseDelivery = await client.SendAsync(requestDelivery);
                    responseDelivery.EnsureSuccessStatusCode();
                    var resultDelivery = await responseDelivery.Content.ReadAsStringAsync();
                    var resDelivery = JsonConvert.DeserializeObject<TrobzResponse>(resultDelivery);

                    if (resDelivery != null)
                    {
                        if (resDelivery.error == null)
                        {
                            tickets.ForEach(ticket => 
                            {
                                ticket.Sync = "2";
                            });
                        }
                        else
                        {
                            FastErrorLog log = new FastErrorLog()
                            {
                                Message = "setDelivery: " + resDelivery.error,
                                CreatedDate = DateTime.Now,
                            };
                            context.FastErrorLogs.Add(log);
                        }

                        context.SaveChanges();
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

        public async Task PushIssueData()
        {
            var context = new HtenContext();

            try
            {
                var queryIssue = (from ticket in context.Tickets
                                  join materialArch in context.MaterialArches
                                  on new { ticket.CodePlant, ticket.PlantNo, SheetNo = (long?)ticket.SheetNo }
                                  equals new { materialArch.CodePlant, materialArch.PlantNo, SheetNo = materialArch.SheetId }
                                  where materialArch.Sync != "2" && ticket.DateTimeMix >= _configs.FromDate
                                  select new
                                  {
                                      ticket.Idticket,
                                      ticket.TicketNo,
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
                                  }).Take(_configs.MaxRecord);

                var groupedIssue = queryIssue
                    .GroupBy(ticket => new
                    {
                        ticket.Idticket,
                        ticket.TicketNo,
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
                        YourID = g.Key.Idticket.ToString(),
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

                var jsonResultIssue = JsonConvert.SerializeObject(groupedIssue);

                using (var client = new HttpClient())
                {
                    var contentIssue = new StringContent(jsonResultIssue, Encoding.UTF8, "application/json");
                    var requestIssue = new HttpRequestMessage(HttpMethod.Post, "https://phudoanh-staging.trobz.com/setIssue")
                    {
                        Content = contentIssue
                    };
                    requestIssue.Headers.Add("Authorization", "b972feef41f0f0d55acfebf0985ef1b89fb12c84");
                    var responseIssue = await client.SendAsync(requestIssue);
                    responseIssue.EnsureSuccessStatusCode();
                    var resultIssue = await responseIssue.Content.ReadAsStringAsync();
                    var resIssue = JsonConvert.DeserializeObject<TrobzResponse>(resultIssue);

                    if (resIssue != null)
                    {
                        if (resIssue.error == null)
                        {
                            var materialArchIds = queryIssue.Select(q => q.Id).Distinct().ToList();
                            var materialArches = context.MaterialArches.Where(m => materialArchIds.Contains(m.Id)).ToList();

                            materialArches.ForEach(materialArch =>
                            {
                                materialArch.Sync = "2";
                            });
                        }
                        else
                        {
                            FastErrorLog log = new FastErrorLog()
                            {
                                Message = "setIssue: " + resIssue.error,
                                CreatedDate = DateTime.Now,
                            };
                            context.FastErrorLogs.Add(log);
                        }

                        context.SaveChanges();
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
