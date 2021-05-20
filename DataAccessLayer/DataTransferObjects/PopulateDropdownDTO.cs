using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class PopulateDropdownDTO
    {
        public IEnumerable<ResourceCategoryDTO> ResourceCategories { get; set; }
        public IEnumerable<ResourceTypeDTO> ResourceTypes { get; set; }
    }
}
