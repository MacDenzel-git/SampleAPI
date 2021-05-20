using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.PositionsContainer
{
   public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAllPosition();
        Task<OutputHandler> CreatePosition(PositionDTO sermonCategory);
        Task<OutputHandler> UpdatePosition(PositionDTO sermonCategory);
        Task<OutputHandler> DeletePosition(int positionId);
        Task<OutputHandler> GetPosition(int PositionId);
    }
}
