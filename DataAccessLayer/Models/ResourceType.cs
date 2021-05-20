using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class ResourceType
    {
        public ResourceType()
        {
            Resources = new HashSet<Resource>();
        }

        public int ResourceTypeId { get; set; }
        public string ResourceTypeName { get; set; }
        public bool IsPublished { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
