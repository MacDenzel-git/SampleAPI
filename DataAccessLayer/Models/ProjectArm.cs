using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class projectArm
    {
        public projectArm()
        {
            Events = new HashSet<Event>();
            TeamMembers = new HashSet<TeamMember>();
        }

        public int projectArmId { get; set; }
        public string projectArmName { get; set; }
        public string projectArmAbbreviation { get; set; }
        public string Highlight { get; set; }
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string MainObjective { get; set; }
        public string Artwork { get; set; }
        public string FooterSection { get; set; }
        public string FinalSection { get; set; }
        public bool IsPublished { get; set; }
        public bool IsFeaturedOnHomePage { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string projectFullDescription { get; set; }
        public bool IsAddedToMenu { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
