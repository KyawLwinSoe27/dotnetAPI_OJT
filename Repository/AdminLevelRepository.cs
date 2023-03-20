using System.Data;
using PracticeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PracticeApi.Repositories
{
    public class AdminLevelRepository : RepositoryBase<AdminLevel>, IAdminLevelRepository
    {
        public AdminLevelRepository(PracticalContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<AdminLevel>> SearchAdminLevelName(string searchTerm)
        {
            return await RepositoryContext.adminLevels
                        .Where(s => s.AdminLevelName.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.adminLevels.Any(e => e.Id == id);
        }
    }

}