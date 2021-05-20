using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.SermonCategoryContainer
{
    public interface IResourceCategoryService
    {
        Task<OutputHandler> GetAllResourceCategory();
        Task<OutputHandler> CreateResourceCategory(ResourceCategoryDTO sermonCategory);
        Task<OutputHandler> UpdateResourceCategory(ResourceCategoryDTO sermonCategory);
        Task<OutputHandler> DeleteResourceCategory(int sermonId);
        Task<OutputHandler> GetResourceCategory(int seriesId);
    }
}
