using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Services.SermonCategoryContainer;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.SermonCategoryContainer
{
    public class ResourceCategoryService : IResourceCategoryService
    {
        private readonly GenericRepository<ResourceCategory> _sermonCategoryRepository;
        public ResourceCategoryService(GenericRepository<ResourceCategory> sermonCategoryRepository)
        {
            _sermonCategoryRepository = sermonCategoryRepository;
        }
        public async Task<OutputHandler> CreateResourceCategory(ResourceCategoryDTO sermonCategory)
        {
            try
            {
                var series = new ResourceCategory { CategoryName = sermonCategory.CategoryName };
                await _sermonCategoryRepository.CreateAsync(series);
                await _sermonCategoryRepository.SaveChangesAsync();

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Resource Series Created Successfully"
                };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);


            }

        }

        public async Task<OutputHandler> DeleteResourceCategory(int sermonId)
        {
            try
            {
                var sermon = await _sermonCategoryRepository.GetItemAsync(x => x.ResourceCategoryId == sermonId);
                await _sermonCategoryRepository.DeleteAsync(sermon);
                await _sermonCategoryRepository.SaveChangesAsync(); ;
                return new OutputHandler { IsErrorOccured = false, Message = "Resource Series Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<OutputHandler> GetAllResourceCategory()
        {
            var output = await _sermonCategoryRepository.GetUnfilteredListAsync();
            return new OutputHandler { Result = output, IsErrorOccured = false };
        }

        public async Task<OutputHandler> UpdateResourceCategory(ResourceCategoryDTO resourceCategory)
        {
            try
            {
                var series = new ResourceCategory { CategoryName = resourceCategory.CategoryName, ResourceCategoryId = resourceCategory.ResourceCategoryId };
                await _sermonCategoryRepository.UpdateAsync(series);

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Resource Series Updated Successfully"
                };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);


            }

        }

        public async Task<OutputHandler> GetResourceCategory(int CategoryId)
        {
            var output = await _sermonCategoryRepository.GetItemAsync(x => x.ResourceCategoryId == CategoryId);
            return new OutputHandler { Result = output };
        }
    }
}
