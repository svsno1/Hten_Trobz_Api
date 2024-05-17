using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class MaterialList
    {
        public string ComName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double? Density { get; set; }
        public int? Type { get; set; }
        public string? TypeMaterial { get; set; }
        public string? Sync { get; set; }
        public string? Unit { get; set; }
        public string? Cot { get; set; }
        public byte[]? Rv { get; set; }
    }
}
