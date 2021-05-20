using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class ResourcePageDTO
    {
        public IEnumerable<ResourceDTO> Resources { get; set; }
        public IEnumerable<ResourceCategoryDTO> ResourceCategories { get; set; }
     
    }
}
