using PracticeApi.Models;

namespace PracticeApi.Repositories
{
    public class CustomerTypeRepository : RepositoryBase<CustomerType>, ICustomerTypeRepository
    {
        public CustomerTypeRepository(PracticalContext repositoryContext) : base(repositoryContext)
        {
        }

        public bool isExists(int id)
        {
            return RepositoryContext.CustomerTypes.Any(e => e.Id == id);
        }
    }
}