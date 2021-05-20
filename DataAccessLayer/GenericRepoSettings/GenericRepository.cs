using DataAccessLayer.Models;
using PODL.Standard.Data.Services.RepositoryService;

namespace DataAccessLayer.GenericRepoSettings
{
    public class GenericRepository<T> : Repository<T> where T : class
    {
        private ProjectWebsiteDBContext _context;
        public GenericRepository(ProjectWebsiteDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

// Scaffold-DbContext "Server=.;Database=projectWebsiteDB; Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
