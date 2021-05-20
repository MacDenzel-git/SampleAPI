using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Position
    {
        public Position()
        {
            TeamMembers = new HashSet<TeamMember>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string Abbreviation { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsPublished { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
