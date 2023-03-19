using PracticeApi.Models;

namespace PracticeApi.Repositories
{
    public interface ICustomerTypeRepository : IRepositoryBase<CustomerType>
    {
        bool isExists(int id);
    }
}