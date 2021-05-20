using BusinessLogicLayer.Resources;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.ResourceTypeContainer
{
    public class ResourceTypeService : IResourceTypeService
    {
        private readonly GenericRepository<ResourceType> _resourcTypeRepository;
        public ResourceTypeService(GenericRepository<ResourceType> resourceTypeRepository)
        {
            _resourcTypeRepository = resourceTypeRepository;
        }
        public async Task<OutputHandler> CreateResourceType(ResourceTypeDTO resourceType)
        {
            try
            {
                var resource = new ResourceType { ResourceTypeName = resourceType.ResourceTypeName };
                await _resourcTypeRepository.CreateAsync(resource);
                await _resourcTypeRepository.SaveChangesAsync();

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Resource Type Created Successfully"
                };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);


            }

        }

        public async Task<OutputHandler> DeleteResourceType(int resourceTypeId)
        {
            try
            {
                var resource = await _resourcTypeRepository.GetItemAsync(x => x.ResourceTypeId == resourceTypeId);
                await _resourcTypeRepository.DeleteAsync(resource);
                await _resourcTypeRepository.SaveChangesAsync(); ;
                return new OutputHandler { IsErrorOccured = false, Message = "Resource Type Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<OutputHandler> GetAllResourceTypes()
        {
            var output = await _resourcTypeRepository.GetUnfilteredListAsync();
            return new OutputHandler { Result = output, IsErrorOccured = false };
        }

        public async Task<OutputHandler> UpdateResourceType(ResourceTypeDTO resourceType)
        {
            try
            {
                var resource = new ResourceType { ResourceTypeName = resourceType.ResourceTypeName, ResourceTypeId = resourceType.ResourceTypeId };
                await _resourcTypeRepository.UpdateAsync(resource);

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Resource Type Updated Successfully"
                };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);


            }

        }

        public async Task<OutputHandler> GetResourceType(int CategoryId)
        {
            var output = await _resourcTypeRepository.GetItemAsync(x => x.ResourceTypeId == CategoryId);
            return new OutputHandler { Result = output };
        }
    }
}
