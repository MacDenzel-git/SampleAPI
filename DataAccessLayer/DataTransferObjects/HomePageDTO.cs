using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class HomePageDTO
    {
        public IEnumerable<QouteDTO> Qoutes { get; set; }
        public IEnumerable<projectArmDTO> projectArms { get; set; }
        public IEnumerable<TeamMembersDTO> TeamMembers { get; set; }
        public EventDTO TimerActivatedEvent { get; set; }
        public IEnumerable<BranchDTO> Branches { get; set; }
        public IEnumerable<ResourceDTO> Resources { get; set; }
    }
}
