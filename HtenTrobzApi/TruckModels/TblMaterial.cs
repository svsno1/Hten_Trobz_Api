using System;
using System.Collections.Generic;

namespace HtenTrobzApi.TruckModels
{
    public partial class TblMaterial
    {
        public TblMaterial()
        {
            TblTickets = new HashSet<TblTicket>();
        }

        public Guid Id { get; set; }
        public string? Code { get; set; }
        /// <summary>
        /// Ten nguyen lieu
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Xuat xu
        /// </summary>
        public string? Origin { get; set; }
        /// <summary>
        /// Ma vach
        /// </summary>
        public string? BarCode { get; set; }
        /// <summary>
        /// Don vi tinh cua nguyen lieu
        /// </summary>
        public string? MaterialUnit { get; set; }
        /// <summary>
        /// Don gia nguyen lieu
        /// </summary>
        public double? UnitPrice { get; set; }
        /// <summary>
        /// Chi phi khac
        /// </summary>
        public double? AdditionFee { get; set; }
        /// <summary>
        /// Don vi tien te
        /// </summary>
        public string? Currency { get; set; }
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
        public string? MatGroup { get; set; }

        public virtual ICollection<TblTicket> TblTickets { get; set; }
    }
}
