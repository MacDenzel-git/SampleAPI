using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class BaseViewDTO
    {
        public IEnumerable<projectArmDTO> projectArms { get; set; }
        public IEnumerable <ResourceTypeDTO> ResourceTypes { get; set; }
        public IEnumerable<BranchDTO> Branches { get; set; }
    }
}
