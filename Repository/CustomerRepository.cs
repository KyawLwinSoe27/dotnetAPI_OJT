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

        public async Task<List<CustomerResult>> SearchCustomerCombo(string filter)
        {
            try {
                ExpandoObject queryFilter = new();
                queryFilter.TryAdd("@filter", "%" + filter + "%");
                queryFilter.TryAdd("@filterid", filter);

                var SelectQuery = @"SELECT c.customer_id AS ID, c.customer_name AS CustomerName, c.customer_register_date AS RegisterDate, c.customer_address AS CustomerAddress, c.customer_type_id AS CustomerTypeId, tbl_customer_type.customer_type_name AS CustomerTypeName
                                    FROM tbl_customers c
                                    INNER JOIN tbl_customer_type ON c.customer_type_id = tbl_customer_type.customer_type_id
                                    WHERE c.customer_name LIKE @filter OR c.customer_id = @filterid
                                    ORDER BY c.customer_name LIMIT 0, 5";

                List<CustomerResult> custResult = await RepositoryContext.RunExecuteSelectQuery<CustomerResult>(SelectQuery, queryFilter);
                return custResult;
            }
            catch (Exception ex) {
                Log.Error("GetCustomerCombo fail "+ ex.Message);
                return new List<CustomerResult>();
            }
        }   

        public bool isExists(int id)
        {
            return RepositoryContext.Customers.Any(s => s.Id == id);
        }
    }
}