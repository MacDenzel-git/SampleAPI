using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Qoute
    {
        public long QouteId { get; set; }
        public string QouteText { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string QouteImg { get; set; }
        public bool IsFeaturedOnHomePage { get; set; }
    }
}
