using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class TeamMember
    {
        public long TeamMemberId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int BranchId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsPublished { get; set; }
        public bool IsTopLeadership { get; set; }
        public bool IsDirector { get; set; }
        public bool? IsStaffMember { get; set; }
        public bool? IsBranchLeader { get; set; }
        public string ImageUrl { get; set; }
        public int PositionId { get; set; }
        public int projectArmId { get; set; }
        public bool IsEcmember { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual projectArm projectArm { get; set; }
        public virtual Position Position { get; set; }
     }
}
