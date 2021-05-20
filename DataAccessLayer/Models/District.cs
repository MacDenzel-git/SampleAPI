using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class District
    {
        public District()
        {
            Branches = new HashSet<Branch>();
        }

        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int CountryId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
