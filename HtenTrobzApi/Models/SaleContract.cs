using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class SaleContract
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Description01 { get; set; }
        public string? Description02 { get; set; }
        public string? CustomerCode { get; set; }
        public string? SiteCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? CreateLog { get; set; }
        public DateTime? LastModifyLog { get; set; }
        public string? UserCreate { get; set; }
        public string? UserChange { get; set; }
        public string? EmployeeCode { get; set; }
    }
}
