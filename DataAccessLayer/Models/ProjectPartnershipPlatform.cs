using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class ProjectPartnershipPlatform
    {
        public int GivingPlatformId { get; set; }
        public string Platform { get; set; }
        public string Number { get; set; }
        public int BranchId { get; set; }
        public int? Contact { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
