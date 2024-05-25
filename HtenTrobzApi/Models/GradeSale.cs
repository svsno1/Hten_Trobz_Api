using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class GradeSale
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Description01 { get; set; }
        public string? Description02 { get; set; }
        public string? Type { get; set; }
        public string? Slump { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateLog { get; set; }
        public DateTime? LastModifyLog { get; set; }
        public string? UserCreate { get; set; }
        public string? UserChange { get; set; }
        public string? RecipeList { get; set; }
        public long? CatalogId { get; set; }
        public bool? IsActive { get; set; }
        public byte[]? Rv { get; set; }
    }
}
