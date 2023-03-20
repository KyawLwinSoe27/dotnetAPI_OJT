using PracticeApi.Models;

namespace PracticeApi.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<IEnumerable<Customer>> SearchCustomer(string searchTerm);
        Task<IEnumerable<CustomerResult>> ListCustomer();
        Task<List<CustomerResult>> SearchCustomerCombo(string filter);
        bool isExists(int id);
    }
}