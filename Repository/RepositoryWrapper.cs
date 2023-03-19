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
    }
}