using System;
using System.Collections.Generic;

namespace HtenTrobzApi.TruckModels
{
    public partial class TblTicketReceivedCbp
    {
        public long Idticket { get; set; }
        public string? CodePlant { get; set; }
        public int PlantNo { get; set; }
        public long SheetNo { get; set; }
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
        public double? M3ThisTicket { get; set; }
        public DateTime? DateTimePrint { get; set; }
        public DateTime? DateTimeMix { get; set; }
        public double? KlCan { get; set; }
        public bool? DaCan { get; set; }
        public DateTime? DateTimeCan { get; set; }
        public string? History { get; set; }
        public string? Note { get; set; }
        public string? Syn { get; set; }
        public DateTime? LastSynchTime { get; set; }
    }
}
