using PracticeApi.Models;
namespace PracticeApi.Repositories
{
    public interface IAdminRepository : IRepositoryBase<Admin>
    {
        Task<IEnumerable<Admin>> SearchAdmin(string searchTerm);
        Task<IEnumerable<AdminResult>> ListAdmins();

        bool IsExists(long id);
    }
}