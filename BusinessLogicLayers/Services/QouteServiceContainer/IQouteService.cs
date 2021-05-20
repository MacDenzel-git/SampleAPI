using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.QouteServiceContainer
{
    public interface IQouteService
    {
        Task<OutputHandler> GetAllQoutes(bool isFiltered);
        Task<OutputHandler> GetAllQoutesForAdmin( );
        Task<OutputHandler> CreateQoute(QouteDTO qouteDTO);
        Task<OutputHandler> UpdateQoute(QouteDTO qouteDTO);
        Task<OutputHandler> DeleteQoute(int qouteId);
        Task<OutputHandler> GetQoute(int qouteId);
    }
}
