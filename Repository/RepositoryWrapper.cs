using PracticeApi.Models;
namespace PracticeApi.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly PracticalContext _repoContext;

        public RepositoryWrapper(PracticalContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        private IHeroRepository? oHero;
        public IHeroRepository Hero
        {
            get
            {
                if (oHero == null)
                {
                    oHero = new HeroRepository(_repoContext);
                }

                return oHero;
            }
        }

        private ICustomerTypeRepository? oCustomerType;
        public ICustomerTypeRepository CustomerType
        {
            get
            {
                if(oCustomerType == null)
                {
                    oCustomerType = new CustomerTypeRepository(_repoContext);
                }
                return oCustomerType;
            }
        }

        private ICustomerRepository? oCustomer;
        public ICustomerRepository Customer
        {
            get
            {
                if(oCustomer == null)
                {
                    oCustomer = new CustomerRepository(_repoContext);
                }
                return oCustomer;
            }
        }
    }
}