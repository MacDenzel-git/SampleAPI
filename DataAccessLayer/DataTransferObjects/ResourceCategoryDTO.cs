using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class ResourceCategoryDTO
    {
        public int ResourceCategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsPublished { get; set; }
        public bool IsFeatured { get; set; }
    }
}
