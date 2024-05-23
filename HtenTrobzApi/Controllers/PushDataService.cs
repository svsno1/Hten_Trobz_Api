﻿using HtenTrobzApi.Models;
using System.Text;

namespace HtenTrobzApi
{
    public class PushDataService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _ = PushDeliveryData();
                _ = PushIssueData();

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        public async Task PushDeliveryData()
        {
            var context = new HtenContext();

            try
            {
                var queryDelivery = (from ticket in context.Tickets
                                    join materialArch in context.MaterialArches
                                    on new { ticket.CodePlant, ticket.PlantNo, ticket.SheetNo }
                                    equals new { materialArch.CodePlant, materialArch.PlantNo, SheetNo = materialArch.SheetId ?? 0 }
                                    where ticket.Sync != "2"
                                    select new
                                    {
                                        ticket,
                                        materialArch
                                    }).Take(100);

                var groupedResultDelivery = queryDelivery
                    .GroupBy(x => x.ticket)
                    .Select(g => new TicketDto
                    {
                        YourID = g.Key.Idticket.ToString(),
                        CusID = g.Key.CustomerCode ?? "",
                        JobID = g.Key.SiteCode ?? "",
                        VcNo = g.Key.TicketNo ?? "",
                        VehicleID = g.Key.TruckCode ?? "",
                        DriverID = g.Key.DriverCode ?? "",
                        ContractID = g.Key.OrderDescription01 ?? "",
                        Note = g.Key.Note ?? "",
                        VcDate = g.Key.DateTimeMix ?? new DateTime(1900, 1, 1),
                        Status = "done",
                        Items = g.Select(item => new MaterialArchDto
                        {
                            Code = item.materialArch.CodeMaterial ?? "",
                            Sl_Dat = g.Key.M3Ordered ?? 0,
                            Quantity = item.materialArch.PvActualy ?? 0,
                            AccumulatedQTY = g.Key.M3Delivered ?? 0,
                            Uom = item.materialArch.UnitMaterial ?? "",
                        }).ToList()
                    })
                    .ToList();

                var jsonResultDelivery = Newtonsoft.Json.JsonConvert.SerializeObject(groupedResultDelivery);

                // HTTP POST request for setDelivery
                using (var client = new HttpClient())
                {
                    var contentDelivery = new StringContent(jsonResultDelivery, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", "b972feef41f0f0f0d55acfebf0985ef1b89fb12c84");
                    var responseDelivery = await client.PostAsync("https://phudoanh-staging.trobz.com/setDelivery", contentDelivery);

                    if (responseDelivery.IsSuccessStatusCode)
                    {
                        // Update ticket.Sync to "2"
                        var ticketsToUpdate = queryDelivery.Select(q => q.ticket).ToList();
                        foreach (var ticket in ticketsToUpdate)
                        {
                            ticket.Sync = "2";
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
                                 on new { ticket.CodePlant, ticket.PlantNo, ticket.SheetNo }
                                 equals new { materialArch.CodePlant, materialArch.PlantNo, SheetNo = materialArch.SheetId ?? 0 }
                                 where materialArch.Sync != "2"
                                 select new
                                 {
                                     ticket,
                                     materialArch
                                 }).Take(100);

                var groupedResultIssue = queryIssue
                    .GroupBy(x => x.ticket)
                    .Select(g => new TicketIssue
                    {
                        YourID = g.Key.Idticket.ToString(),
                        CusID = g.Key.CustomerCode ?? "",
                        JobID = g.Key.SiteCode ?? "",
                        VcNo = g.Key.TicketNo ?? "",
                        VehicleID = g.Key.TruckCode ?? "",
                        DriverID = g.Key.DriverCode ?? "",
                        Note = g.Key.Note ?? "",
                        Status = "done",
                        ProductID = g.Key.RecipeCode ?? "",
                        Items = g.Select(item => new MaterialArchIssue
                        {
                            Code = item.materialArch.CodeMaterial ?? "",
                            Name = item.materialArch.NameMaterial ?? "",
                            Quantity = item.materialArch.PvActualy ?? 0,
                            Uom = item.materialArch.UnitMaterial ?? "",
                        }).ToList()
                    })
                    .ToList();

                var jsonResultIssue = Newtonsoft.Json.JsonConvert.SerializeObject(groupedResultIssue);

                // HTTP POST request for setIssue
                using (var client = new HttpClient())
                {
                    var contentIssue = new StringContent(jsonResultIssue, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", "b972feef41f0f0f0d55acfebf0985ef1b89fb12c84");
                    var responseIssue = await client.PostAsync("https://phudoanh-staging.trobz.com/setIssue", contentIssue);

                    if (responseIssue.IsSuccessStatusCode)
                    {
                        // Update materialArch.Sync to "2"
                        var materialArchesToUpdate = queryIssue.Select(q => q.materialArch).ToList();
                        foreach (var materialArch in materialArchesToUpdate)
                        {
                            materialArch.Sync = "2";
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

    }
}