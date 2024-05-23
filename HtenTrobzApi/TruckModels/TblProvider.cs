using System;
using System.Collections.Generic;

namespace HtenTrobzApi.TruckModels
{
    public partial class TblProvider
    {
        public TblProvider()
        {
            TblTickets = new HashSet<TblTicket>();
        }

        public Guid Id { get; set; }
        public string? Code { get; set; }
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
        public string? CreateLog { get; set; }
        public string? LastModifyLog { get; set; }
        public string? UserCreated { get; set; }
        public string? UserChanged { get; set; }
        public string? Syn { get; set; }
        public DateTime? LastSynchTime { get; set; }

        public virtual ICollection<TblTicket> TblTickets { get; set; }
    }
}
