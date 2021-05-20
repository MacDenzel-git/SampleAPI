using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.UtilsContainer
{
    public interface IUtilService
    {
        public Task<HomePageDTO> SetupHomePage();
        public Task<BaseViewDTO> GetMenuItems();
        public Task<PopulateDropdownDTO> DropDownItems();
    }
}
