using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class TblProvider
    {
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public byte[]? Logo { get; set; }
        public string? Contact { get; set; }
        public string? Mail { get; set; }
        public string? Address { get; set; }
        /// <summary>
        /// Ghi chu
        /// </summary>
        public string? Note { get; set; }
        public string? HotKey { get; set; }
        public DateTime? CreateLog { get; set; }
        public DateTime? LastModifyLog { get; set; }
        public string? UserCreated { get; set; }
        public string? UserChanged { get; set; }
        public string? Syn { get; set; }
        public DateTime? LastSynchTime { get; set; }
    }
}
