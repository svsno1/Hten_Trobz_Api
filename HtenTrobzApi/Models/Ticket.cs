using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class Ticket
    {
        public long Idticket { get; set; }
        public string? CodePlant { get; set; }
        public int PlantNo { get; set; }
        public long SheetNo { get; set; }
        public string? TicketNo { get; set; }
        public string? SealNo { get; set; }
        public string? DeliveryNoOrder { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderDescription01 { get; set; }
        public string? OrderDescription02 { get; set; }
        public string? CustomerCode { get; set; }
        public string? Customer { get; set; }
        public string? AddressCustomer { get; set; }
        public string? SiteCode { get; set; }
        public string? Site { get; set; }
        public string? AddressSite { get; set; }
        public string? RecipeCode { get; set; }
        public string? RcDescription01 { get; set; }
        public string? RcDescription02 { get; set; }
        public string? RcType { get; set; }
        public string? RcSlump { get; set; }
        public string? DriverCode { get; set; }
        public string? Driver { get; set; }
        public string? TruckCode { get; set; }
        public string? Truck { get; set; }
        public double? M3Ordered { get; set; }
        public double? M3Delivered { get; set; }
        public double? M3Balance { get; set; }
        public double? M3ThisTicket { get; set; }
        public double? M3PrintTicket { get; set; }
        public DateTime? DateTimePrint { get; set; }
        public int? BatchTotal { get; set; }
        public double? M3Reject { get; set; }
        public double? M3ThisOrder { get; set; }
        public DateTime? DateTimeMix { get; set; }
        public DateTime? DateTimeEnd { get; set; }
        public DateTime? DateTimeDisSite { get; set; }
        public DateTime? DateTimeEndSite { get; set; }
        public bool? Pump { get; set; }
        public string? WcRatio { get; set; }
        public string? Operator { get; set; }
        public bool? Finished { get; set; }
        public double? RatioWctarget { get; set; }
        public double? RatioWcactual { get; set; }
        public string? SlumpAtSite { get; set; }
        public string? CusCommentAtSite { get; set; }
        public string? History { get; set; }
        public string? Note { get; set; }
        public bool? Sim { get; set; }
        public string? Sync { get; set; }
        public bool? UpdatedSheet { get; set; }
        public string? PumpCode { get; set; }
        public string? AdditivesCode { get; set; }
        public string? IntensityCode { get; set; }
        public double? Distance { get; set; }
        public int? Coly { get; set; }
    }
}
