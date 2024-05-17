using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class OrderDetail
    {
        public long Id { get; set; }
        public string? CodePlant { get; set; }
        public int PlantNo { get; set; }
        public string? OrderCode { get; set; }
        public string? RecipeCode { get; set; }
        public string? Description01 { get; set; }
        public double? OrderedM3 { get; set; }
        public double? ProductedM3 { get; set; }
        public double? Price { get; set; }
        public string? Sync { get; set; }
        public int? TripNo { get; set; }
        public string? NotesProduct { get; set; }
        public bool? Duyet { get; set; }
        public bool? Locked { get; set; }
        public string? AdditivesCode { get; set; }
        public string? IntensityCode { get; set; }
        public string? SlumpCode { get; set; }
        public string? PumpList { get; set; }
        public string? RowId { get; set; }
    }
}
