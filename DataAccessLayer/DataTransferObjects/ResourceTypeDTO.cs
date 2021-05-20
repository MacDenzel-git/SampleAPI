using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class ResourceTypeDTO
    {
        public int ResourceTypeId { get; set; }
        public string ResourceTypeName { get; set; }
        public bool IsPublished { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
