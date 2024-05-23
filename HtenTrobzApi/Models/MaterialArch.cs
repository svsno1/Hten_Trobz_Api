using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class MaterialArch
    {
        public long Id { get; set; }
        public string? CodePlant { get; set; }
        public int PlantNo { get; set; }
        public int? ComNum { get; set; }
        public string? CodeMaterial { get; set; }
        public string? NameMaterial { get; set; }
        public int? Type { get; set; }
        public string? UnitMaterial { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string? ModeRun { get; set; }
        public long? BatchId { get; set; }
        public long? SheetId { get; set; }
        public string? RecipeId { get; set; }
        public double? SpRecipe { get; set; }
        public double? SpTarget { get; set; }
        public double? Moist { get; set; }
        public double? PvActualy { get; set; }
        public int? DecimalNo { get; set; }
        public string? Sync { get; set; }
        public bool? Sim { get; set; }
        public double? Abmoist { get; set; }
    }
}
