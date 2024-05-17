using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class Order
    {
        public long Id { get; set; }
        public string? CodePlant { get; set; }
        public int PlantNo { get; set; }
        public string Code { get; set; } = null!;
        public string? Description01 { get; set; }
        public string? Description02 { get; set; }
        public string? CustomerCode { get; set; }
        public string? SiteCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public long? Delivery { get; set; }
        public DateTime? CreateLog { get; set; }
        public DateTime? LastModifyLog { get; set; }
        public string? UserCreate { get; set; }
        public string? UserChange { get; set; }
        public string? Sync { get; set; }
        public bool? Duyet { get; set; }
        public bool? Locked { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Contact { get; set; }
        public string? Category { get; set; }
        public string? BizdocType { get; set; }
    }
}
