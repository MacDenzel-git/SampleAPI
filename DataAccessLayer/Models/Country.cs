using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Country
    {
        public Country()
        {
            Districts = new HashSet<District>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
