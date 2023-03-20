using PracticeApi.Models;

namespace PracticeApi.Repositories
{
    public interface IAdminLevelRepository : IRepositoryBase<AdminLevel>
    {
        Task<IEnumerable<AdminLevel>> SearchAdminLevelName(string searchTerm);

        bool IsExists(long id);
    }
}