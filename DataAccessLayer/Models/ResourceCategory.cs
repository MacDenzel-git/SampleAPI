using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class ResourceCategory
    {
        public ResourceCategory()
        {
            Resources = new HashSet<Resource>();
        }

        public int ResourceCategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsPublished { get; set; }
        public bool IsFeatured { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
