using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.ResourceContainer
{
    public interface IResourceService
    {
        Task<OutputHandler> AddResourceAsync(ResourceDTO resource);
        Task<OutputHandler> GetResourceListAsync(string resourceType);
        Task<OutputHandler> GetFeaturedResourcesAsync(string resourceType);
        Task<IEnumerable<ResourceDTO>> GetFilteredResourceListAsync(int categoryId);
        Task<IEnumerable<ResourceDTO>> GetResourceListForAdminAsync();
        Task<IEnumerable<ResourceCategory>> GetResourceCategories();
        Task<OutputHandler> DeleteResource(long resourceId);
        Task<ResourceDTO> GetResource(long resourceId);
        Task<OutputHandler> EditResource(ResourceDTO resource);
    }
}
