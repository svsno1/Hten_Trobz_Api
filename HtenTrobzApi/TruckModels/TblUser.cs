using System;
using System.Collections.Generic;

namespace HtenTrobzApi.TruckModels
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblTicketGrossOperConfirmNavigations = new HashSet<TblTicket>();
            TblTicketTareOperConfirmNavigations = new HashSet<TblTicket>();
        }

        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? DepartmentName { get; set; }
        public string? FullName { get; set; }
        public string? Contact { get; set; }
        public string? Mail { get; set; }
        public bool? Active { get; set; }
        public string? ActiveEncypt { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? CreateLog { get; set; }
        public string? LastModifyLog { get; set; }
        public string? UserCreated { get; set; }
        public string? UserChanged { get; set; }
        public string? StationId { get; set; }
        public string? Syn { get; set; }
        public DateTime? LastSynchTime { get; set; }

        public virtual ICollection<TblTicket> TblTicketGrossOperConfirmNavigations { get; set; }
        public virtual ICollection<TblTicket> TblTicketTareOperConfirmNavigations { get; set; }
    }
}
