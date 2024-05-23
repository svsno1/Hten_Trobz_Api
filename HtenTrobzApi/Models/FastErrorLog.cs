using System;
using System.Collections.Generic;

namespace HtenTrobzApi.Models
{
    public partial class FastErrorLog
    {
        public long Id { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
