using System.Data;
using PracticeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PracticeApi.Repositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(PracticalContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Admin>> SearchAdmin(string searchTerm)
        {
            return await RepositoryContext.admins
                        .Where(s => s.AdminName.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<AdminResult>> ListAdmins()
        {
            // return await RepositoryContext.Admins
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Customers
            //             .Include(e => e.EmpDepartment)
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Customers
            //             .Select(e => new CustomerResult{
            //                 Id = e.Id,
            //                 CustomerName = e.CustomerName,
            //                 CustomerAddress = e.CustomerAddress,
            //                 CustomerTypeId = e.CustomerTypeId
            //             })
            //             .OrderBy(s => s.Id).ToListAsync();
            return await RepositoryContext.admins
                        .Select(e => new AdminResult{
                            Id = e.Id,
                            AdminName = e.AdminName,
                            Email = e.Email,
                            LoginName = e.LoginName,
                            Inactive = e.Inactive,
                            AdminLevelName = e.adminLevel!.AdminLevelName,
                        })
                        .OrderBy(s => s.Id).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.admins.Any(e => e.Id == id);
        }
    }

}