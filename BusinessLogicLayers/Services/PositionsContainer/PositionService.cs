using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.PositionsContainer
{
    public class PositionService:IPositionService
    {
        readonly GenericRepository<Position> _positionRepository;
       

        public PositionService( GenericRepository<Position> positionRepository)
        {
           
            _positionRepository = positionRepository;
            
        }



        public async Task<IEnumerable<Position>> GetAllPosition()
        {
            return await _positionRepository.GetUnfilteredListAsync();
        }
        public async Task<OutputHandler> CreatePosition(PositionDTO positionDTO)
        {
            try
            {
                var position = new AutoMapper<PositionDTO, Position>().MapToObject(positionDTO);
                 _positionRepository.CreateEntity(position);
                await _positionRepository.SaveChangesAsync();


                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Position Created Successfully"
                };

            }
            catch (Exception)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Something went wrong, Contact Admin"
                };

            }

        }
        public async Task<OutputHandler> DeletePosition(int positionId)
        {
            try
            {
                var position = await _positionRepository.GetItemAsync(x => x.PositionId == positionId);
               await _positionRepository.DeleteAsync(position);
                await _positionRepository.SaveChangesAsync(); ;
                return new OutputHandler { IsErrorOccured = false, Message = "Position Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return new OutputHandler { IsErrorOccured = true, Message = "Something went wrong, Please Contact Administrator" };
            }
        }
        public async Task<OutputHandler> UpdatePosition(PositionDTO position)
        {
            try
            {
                var series = new Position { Abbreviation = position.Abbreviation, PositionName = position.PositionName, PositionId = position.PositionId };
                await _positionRepository.UpdateAsync(series);

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Position  Updated Successfully"
                };

            }
            catch (Exception)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Something went wrong, Contact Admin"
                };

            }

        }

        public async Task<OutputHandler> GetPosition(int PositionId)
        {
            var output = await _positionRepository.GetItemAsync(x => x.PositionId == PositionId);
            return new OutputHandler { Result = output };
        }
 

    }
}
