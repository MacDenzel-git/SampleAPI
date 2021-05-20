using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.BLLResources;
using System.Linq;

namespace BusinessLogicLayer.Services.UtilsContainer
{
    public class UtilService : IUtilService
    {
        private readonly GenericRepository<DataAccessLayer.Models.Resource> _resourceRepository;
        private readonly GenericRepository<Qoute> _qouteRepository;
        private readonly GenericRepository<Branch> _branchRepository;
        private readonly GenericRepository<projectArm> _projectArmRepository;
        private readonly GenericRepository<Event> _eventRepository;
        private readonly GenericRepository<TeamMember> _teamRepository;
        private readonly GenericRepository<ResourceCategory> _resourceCategoryRepository;
        private readonly GenericRepository<ResourceType> _resourceTypeRepository;

        public UtilService(GenericRepository<DataAccessLayer.Models.Resource> resourceRepository, GenericRepository<Qoute> qouteRepository,
                           GenericRepository<projectArm> projectArmRepository, GenericRepository<Event> eventRepository,
                           GenericRepository<TeamMember> teamRepository,
                           GenericRepository<ResourceCategory> resourceCategoryRepository,
                            GenericRepository<ResourceType> resourceTypeRepository,
                            GenericRepository<Branch> branchRepository)
        {
            _branchRepository = branchRepository;
            _resourceRepository = resourceRepository;
            _qouteRepository = qouteRepository;
            _projectArmRepository = projectArmRepository;
            _eventRepository = eventRepository;
            _teamRepository = teamRepository;
            _resourceCategoryRepository = resourceCategoryRepository;
            _resourceTypeRepository = resourceTypeRepository;
        }

        public async Task<BaseViewDTO> GetMenuItems()
        {

            try
            {
                var baseViewDTO = new BaseViewDTO
                {
                    projectArms = new AutoMapper<projectArm, projectArmDTO>().MapToList(await _projectArmRepository.GetListAsync(x => x.IsPublished == true)),
                    ResourceTypes = new AutoMapper<ResourceType, ResourceTypeDTO>().MapToList(await _resourceTypeRepository.GetListAsync(x => x.IsPublished == true)),
                    Branches = new AutoMapper<DataAccessLayer.Models.Branch, BranchDTO>().MapToList(await _branchRepository.GetListAsync(x => x.IsPublished == true))
                };

                return baseViewDTO;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<HomePageDTO> SetupHomePage()
        {
            var resourceDTO = new AutoMapper<DataAccessLayer.Models.Resource, ResourceDTO>()
                            .MapToList(await _resourceRepository.GetListAsync(x => x.IsPublished == true && x.IsFeatured == true));



            var qouteDTO = new AutoMapper<Qoute, QouteDTO>()
                .MapToList(await _qouteRepository.GetListAsync(x => x.IsFeaturedOnHomePage == true && x.IsPublished == true));

            //all project arms to be filtered front end 
            var projectArms = new AutoMapper<projectArm, projectArmDTO>()
                .MapToList(await _projectArmRepository.GetListAsync(x => x.IsPublished == true));

            //timer object 

            //var timerEvent = new AutoMapper<Event, EventDTO>()
            //    .MapToObject(await _eventRepository.GetItemAsync(x => x.IsTimeActive == true && x.IsPublished == true)); 
            var timerEvent = new EventDTO();
            var events = await _eventRepository.GetListAsync(x => x.IsTimeActive == true && x.IsPublished == true && x.IsAnEvent);
            if (events.Any())
            {
                //in the case were multiple events have been marked for home page time 
                //choose the earliest date and show that one on home page making the other ones wait for this one to complete
                var currentEvent = events.OrderBy(x => x.DateOfEvent).First();
                if (currentEvent.EventEndDate < DateTime.UtcNow.AddHours(2))
                {
                    currentEvent.IsTimeActive = false;
                    await _eventRepository.UpdateAsync(currentEvent);
                    await _eventRepository.SaveChangesAsync();
                    timerEvent = null;
                }
                else
                {
                    timerEvent = new AutoMapper<Event, EventDTO>().MapToObject(currentEvent);
                }
           
            }
            else
            {
               timerEvent = null;
            }
            

            var HomePageDTO = new HomePageDTO
            {
                Resources = resourceDTO,
                Qoutes = qouteDTO,
                projectArms = projectArms,
                TimerActivatedEvent = timerEvent,

            };

            return HomePageDTO;
        }

        public async Task<PopulateDropdownDTO> DropDownItems()
        {

            var dropdown = new PopulateDropdownDTO
            {
                ResourceCategories = new AutoMapper<ResourceCategory, ResourceCategoryDTO>().MapToList(await _resourceCategoryRepository.GetListAsync(x => x.IsPublished == true)),
                ResourceTypes = new AutoMapper<ResourceType, ResourceTypeDTO>().MapToList(await _resourceTypeRepository.GetListAsync(x => x.IsPublished == true)),
            };

            return dropdown;
        }

    }
}
