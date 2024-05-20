using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class SaleContractDetail
    {
        public long Id { get; set; }
        public string? SaleContractCode { get; set; }
        public string? GradeSaleCode { get; set; }
        public string? Description01 { get; set; }
        public double? OrderedM3 { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public string? NotesProduct { get; set; }
        public bool? Duyet { get; set; }
        public bool? Locked { get; set; }
    }
}
