using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.TestimonyServiceContainer
{
   public  interface ITestimonyService
    {
        Task<OutputHandler> GetAllTestimonies(bool isFiltered);
        Task<OutputHandler> CreateTestimony(TestimonyDTO testimonyDTO);
        Task<OutputHandler> UpdateTestimony(TestimonyDTO testimonyDTO);
        Task<OutputHandler> DeleteTestimony(int testimonyId);
        Task<OutputHandler> GetTestimony(int testimonyId);
    }
}
