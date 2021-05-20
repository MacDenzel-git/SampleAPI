using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.ResourceTypeContainer
{
    public interface IResourceTypeService
    {
        Task<OutputHandler> GetAllResourceTypes();
        Task<OutputHandler> CreateResourceType(ResourceTypeDTO resourceType);
        Task<OutputHandler> UpdateResourceType(ResourceTypeDTO resourceType);
        Task<OutputHandler> DeleteResourceType(int resourceTypeId);
        Task<OutputHandler> GetResourceType(int resourceType);
    }
}
