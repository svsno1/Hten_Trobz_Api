using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class Customer
    {
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? Email { get; set; }
        public string? ContactMan { get; set; }
        public string? Locality { get; set; }
        public string? Telephone { get; set; }
        public double? CreditLimit { get; set; }
        public double? CreditTime { get; set; }
        public int? Type { get; set; }
        public string? Note { get; set; }
        public string? SaleMan { get; set; }
        public DateTime? CreateLog { get; set; }
        public DateTime? LastModifyLog { get; set; }
        public string? UserCreate { get; set; }
        public string? UserChange { get; set; }
        public byte[]? Rv { get; set; }
    }
}
