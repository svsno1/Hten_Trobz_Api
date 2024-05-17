using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class H00member
    {
        public string MemberId { get; set; } = null!;
        public string MemberName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string MemberType { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool Locked { get; set; }
        public string MemberIdAllow { get; set; } = null!;
        public byte[]? CheckPass { get; set; }
        public string MaCbNv { get; set; } = null!;
        public bool? IsMemberId { get; set; }
        public string MaDvCsDefault { get; set; } = null!;
        public string? PlantList { get; set; }
    }
}
