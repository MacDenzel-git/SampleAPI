using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Branch
    {
        public Branch()
        {
            GivingPlatforms = new HashSet<ProjectPartnershipPlatform>();
            TeamMembers = new HashSet<TeamMember>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public long DistrictId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string GoogleMapInfo { get; set; }
        public string LocationDescription { get; set; }
        public bool? IsCell { get; set; }
        public bool? IsPublished { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<ProjectPartnershipPlatform> GivingPlatforms { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
