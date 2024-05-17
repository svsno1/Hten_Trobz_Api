using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class Driver
    {
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public int? TripsNumber { get; set; }
        public double? KmCovered { get; set; }
        public DateTime? CreateLog { get; set; }
        public DateTime? LastModifyLog { get; set; }
        public string? UserCreate { get; set; }
        public string? UserChange { get; set; }
        public string? CodePlant { get; set; }
        public byte[]? Rv { get; set; }
    }
}
