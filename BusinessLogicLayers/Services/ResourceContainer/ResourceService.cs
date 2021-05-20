using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.BLLResources;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;
using System.IO;
using BusinessLogicLayer.Resources;

namespace BusinessLogicLayer.Services.ResourceContainer
{
    public class ResourceService : IResourceService
    {
        private readonly GenericRepository<DataAccessLayer.Models.Resource> _resourceRepository;
        private readonly GenericRepository<ResourceType> _resourceTypeRepository;
        private readonly GenericRepository<ResourceCategory> _ResourceCategoryRepository;
        private const string FolderName = "ResourceArtworks";
        public ResourceService(GenericRepository<DataAccessLayer.Models.Resource> sermonRepositoty,
            GenericRepository<ResourceType> resourceTypeRepository,
            GenericRepository<ResourceCategory> ResourceCategoryRepository)
        {
            _ResourceCategoryRepository = ResourceCategoryRepository;
            _resourceTypeRepository = resourceTypeRepository;
            _resourceRepository = sermonRepositoty;
        }
        public async Task<OutputHandler> AddResourceAsync(ResourceDTO resource)
        {


            var outputhandler = await FileHandler.SaveFileFromByte(resource.Artwork, resource.Filename, FolderName);
            if (outputhandler.IsErrorOccured)
            {
                if (outputhandler.IsErrorKnown)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = outputhandler.Message
                    };
                }
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Something went wrong while the system tried to upload the file"
                };
            }
            resource.ImageUrl = outputhandler.ImageUrl;

            try
            {
                var MappedSermon = new AutoMapper<ResourceDTO, DataAccessLayer.Models.Resource>().MapToObject(resource);
                _resourceRepository.CreateEntity(MappedSermon);
                await _resourceRepository.SaveChangesAsync();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Sermon has been created Successsdfully "

                };
            }
            catch (Exception ex)
            {
                var outputHandler = await FileHandler.DeleteFileFromFolder(resource.ImageUrl, FolderName);
                return StandardMessages.getExceptionMessage(ex);

            }

        }

        public async Task<OutputHandler> DeleteResource(long resourceId)
        {
            try
            {
                var resource = await _resourceRepository.GetItemAsync(x => x.ResourceId == resourceId);
                await _resourceRepository.DeleteAsync(resource);
                var deletionresult = await FileHandler.DeleteFileFromFolder(resource.ImageUrl, FolderName);
                if (deletionresult.IsErrorOccured)
                {
                    return deletionresult;
                }
                await _resourceRepository.SaveChangesAsync();

                return new OutputHandler { IsErrorOccured = false, Message = "Qoute Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<OutputHandler> EditResource(ResourceDTO resource)
        {

            if (resource.Artwork == null)
            { resource.ImageUrl = resource.ImageUrl; }
            else
            {
                var outputhandler = await FileHandler.SaveFileFromByte(resource.Artwork, resource.Filename, FolderName);

                if (outputhandler.IsErrorOccured)
                {
                    if (outputhandler.IsErrorKnown)
                    {
                        return new OutputHandler
                        {
                            IsErrorOccured = true,
                            Message = outputhandler.Message
                        };
                    }
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "Something went wrong while the system tried to upload the file"
                    };
                }
                resource.ImageUrl = outputhandler.ImageUrl;
            }
            try
            {
                var MappedSermon = new AutoMapper<ResourceDTO, DataAccessLayer.Models.Resource>().MapToObject(resource);
                await _resourceRepository.UpdateAsync(MappedSermon);
                await _resourceRepository.SaveChangesAsync();

                if (resource.OldImageUrl == null)
                { 

                }
                else
                {
                    if (resource.Artwork == null)
                    {

                    }
                    else
                    {
                        var outputHandler = await FileHandler.DeleteFileFromFolder(resource.OldImageUrl, FolderName);
                        if (outputHandler.IsErrorOccured) //Deletion failed but updated saved
                        {
                            return new OutputHandler
                            {
                                IsErrorKnown = true,
                                IsErrorOccured = true,
                                Message = "resource Details updated successfully, but deleting of old file failed, please alert Techarch Team"
                            };
                        }
                    }
                }
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "resource has been edited Successsdfully "

                };
            }
            catch (Exception ex)
            {

                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<IEnumerable<ResourceDTO>> GetFilteredResourceListAsync(int categoryId)
        {
            var sermons = await _resourceRepository.GetListAsync(x => x.ResourceCategoryId == categoryId);
            var sermonDTO = new AutoMapper<DataAccessLayer.Models.Resource, ResourceDTO>().MapToList(sermons);
            foreach (var item in sermonDTO)
            {
                item.Artwork = await FileHandler.ConvertFileToByte(item.ImageUrl);
            }

            return sermonDTO;

        }

        public async Task<ResourceDTO> GetResource(long resourceId)
        {
            var sermons = await _resourceRepository.GetItemAsync(x => x.ResourceId == resourceId);
            var sermonDTO = new AutoMapper<DataAccessLayer.Models.Resource, ResourceDTO>().MapToObject(sermons);
            sermonDTO.Artwork = await FileHandler.ConvertFileToByte(sermons.ImageUrl);
            return sermonDTO;

        }

        public async Task<IEnumerable<ResourceCategory>> GetResourceCategories()
        {
            var output = await _ResourceCategoryRepository.GetUnfilteredListAsync();
            return output;
        }

        public async Task<OutputHandler> GetResourceListAsync(string resourceType)
        {
            var type = await _resourceTypeRepository.GetItemAsync(x => x.ResourceTypeName == resourceType);
            if (type == null)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "We could not find what you are looking for, choose from the menu Resource center."
                };
            }
            else
            {

                ResourcePageDTO resource = new ResourcePageDTO();
                    
                    var resourceList = await _resourceRepository.GetListAsync(x => x.IsPublished && x.ResourceTypeId == type.ResourceTypeId, "ResourceCategory");
                var resourceCategoriesList = await _ResourceCategoryRepository.GetListAsync(x => x.IsPublished);
                resource.Resources = new AutoMapper<DataAccessLayer.Models.Resource, ResourceDTO>().MapToList(resourceList);
                resource.ResourceCategories = new AutoMapper<DataAccessLayer.Models.ResourceCategory, ResourceCategoryDTO>().MapToList(resourceCategoriesList); //for categories list on View
                foreach (var item in resource.Resources)
                {
                    item.Artwork = await FileHandler.ConvertFileToByte(item.ImageUrl);
                }

                return new OutputHandler { Result = resource, IsErrorOccured = false };
            }
           
        }
        public async Task<OutputHandler> GetFeaturedResourcesAsync(string resourceType)
        {
            if (string.IsNullOrEmpty(resourceType))
            {
                ResourcePageDTO resource = new ResourcePageDTO();

                var resourceList = await _resourceRepository.GetListAsync(x => x.IsPublished == true && x.IsFeatured == true, "ResourceCategory");
                var resourceCategoriesList = await _ResourceCategoryRepository.GetListAsync(x => x.IsPublished);
                resource.Resources = new AutoMapper<DataAccessLayer.Models.Resource, ResourceDTO>().MapToList(resourceList);
                resource.ResourceCategories = new AutoMapper<DataAccessLayer.Models.ResourceCategory, ResourceCategoryDTO>().MapToList(resourceCategoriesList); //for categories list on View
                foreach (var item in resource.Resources)
                {
                    item.Artwork = await FileHandler.ConvertFileToByte(item.ImageUrl);
                }

                return new OutputHandler { Result = resource.Resources, IsErrorOccured = false };
            }
            else
            {

        
            var type = await _resourceTypeRepository.GetItemAsync(x => x.ResourceTypeName == resourceType);
            if (type == null)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "We could not find what you are looking for, choose from the menu Resource center."
                };
            }
            else
            {

                ResourcePageDTO resource = new ResourcePageDTO();

                var resourceList = await _resourceRepository.GetListAsync(x => x.IsPublished && x.ResourceTypeId == type.ResourceTypeId, "ResourceCategory");
                var resourceCategoriesList = await _ResourceCategoryRepository.GetListAsync(x => x.IsPublished);
                resource.Resources = new AutoMapper<DataAccessLayer.Models.Resource, ResourceDTO>().MapToList(resourceList);
                resource.ResourceCategories = new AutoMapper<DataAccessLayer.Models.ResourceCategory, ResourceCategoryDTO>().MapToList(resourceCategoriesList); //for categories list on View
                foreach (var item in resource.Resources)
                {
                    item.Artwork = await FileHandler.ConvertFileToByte(item.ImageUrl);
                }

                return new OutputHandler { Result = resource.Resources, IsErrorOccured = false };
            }  
            }

        }
        public async Task<IEnumerable<ResourceDTO>> GetResourceListForAdminAsync()
        {
            var resources = new AutoMapper<Resource,ResourceDTO>().MapToList( await _resourceRepository.GetUnfilteredListAsync());
            foreach (var item in resources)
            {
                var output = await FileHandler.GetFileSize(item.ImageUrl);
                if (output.IsErrorOccured)
                {
                    item.StorageSize = "Could not retrieve size";
                }
                else
                {
                    item.StorageSize = output.Result.ToString();
                }
            }
            return resources;
        }
    }
}
