using System;
using System.Collections.Generic;

namespace HtenTrobzApi.TruckModels
{
    public partial class TblTicket
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public Guid? MaterialRef { get; set; }
        public Guid? ProviderRef { get; set; }
        public Guid? CustomerRef { get; set; }
        public string? DriverName { get; set; }
        public string? TruckPlateNumber { get; set; }
        public double? NetWeight { get; set; }
        public double? GrossWeight { get; set; }
        public double? TareWeight { get; set; }
        public Guid? GrossOperConfirm { get; set; }
        public Guid? TareOperConfirm { get; set; }
        public Guid? GrossAccountantConfirm { get; set; }
        public Guid? TareAccountantConfirm { get; set; }
        public DateTime? GrossDatetime { get; set; }
        public DateTime? TareDatetime { get; set; }
        public string? KepChi { get; set; }
        public string? StationId { get; set; }
        public string? Syn { get; set; }
        public DateTime? LastSynchTime { get; set; }
        public string? MatGroup { get; set; }
        public Guid? ShipperRef { get; set; }
        public double? SuplierWeight { get; set; }
        public double? SuplierM3 { get; set; }
        public double? AdjustWeight { get; set; }
        public long? SheetNoCbp { get; set; }
        public string? SealNoCbp { get; set; }
        public int? PlantNoCbp { get; set; }
        public string? Sync { get; set; }

        public virtual TblUser? GrossOperConfirmNavigation { get; set; }
        public virtual TblMaterial? MaterialRefNavigation { get; set; }
        public virtual TblProvider? ProviderRefNavigation { get; set; }
        public virtual TblUser? TareOperConfirmNavigation { get; set; }
    }
}
