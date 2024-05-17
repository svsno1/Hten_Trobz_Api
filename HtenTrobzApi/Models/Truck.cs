using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class Truck
    {
        public string Code { get; set; } = null!;
        public string? PlateNumber { get; set; }
        public double? Capacity { get; set; }
        public double? MinimumCharging { get; set; }
        public double? M3Transported { get; set; }
        public double? KmCovered { get; set; }
        public DateTime? CreateLog { get; set; }
        public DateTime? LastModifyLog { get; set; }
        public string? UserCreate { get; set; }
        public string? UserChange { get; set; }
        public string? DriverCode { get; set; }
        public string? CodePlant { get; set; }
        public string? Dmnlcode { get; set; }
        public double? Sddk { get; set; }
        public byte[]? Rv { get; set; }
    }
}
