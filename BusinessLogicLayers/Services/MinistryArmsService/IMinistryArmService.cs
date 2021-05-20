using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.projectArmsService
{
    public interface IprojectArmService
    {
        Task<OutputHandler> GetAllprojectArms(bool isAdminRequest = false);
        Task<OutputHandler> CreateprojectArm(projectArmDTO projectArmDTO);
        Task<OutputHandler> UpdateprojectArm(projectArmDTO projectArmDTO);
        Task<OutputHandler> DeleteprojectArm(int projectArmId);
        Task<OutputHandler> GetprojectArm(int projectArmId);
    }
}
