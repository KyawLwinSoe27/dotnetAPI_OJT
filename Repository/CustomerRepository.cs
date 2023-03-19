using System.Data;
using PracticeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Dynamic;
using Serilog;

namespace PracticeApi.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(PracticalContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Customer>> SearchCustomer(string searchTerm)
        {
            return await RepositoryContext.Customers
                        .Where(s => s.CustomerName.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<CustomerResult>> ListCustomer(){
            // return await RepositoryContext.Customers
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
            return await RepositoryContext.Customers
                        .Select(e => new CustomerResult{
                            Id = e.Id,
                            CustomerName = e.CustomerName,
                            CustomerRegisterDate = e.CustomerRegisterDate,
                            CustomerAddress = e.CustomerAddress,
                            CustomerTypeId = e.CustomerTypeId,
                            CustomerTypeName = e.customerType!.CustomerTypeName
                        })
                        .OrderBy(s => s.Id).ToListAsync();
        }   

        public bool isExists(int id)
        {
            return RepositoryContext.Customers.Any(s => s.Id == id);
        }
    }
}