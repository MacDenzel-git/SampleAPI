using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class ProjectPartnershipPlatformsDTO
    {
        public int GivingPlatformId { get; set; }
        public string Platform { get; set; }
        public string Number { get; set; }
        public int BranchId { get; set; }
        public int? Contact { get; set; }

    }
}
