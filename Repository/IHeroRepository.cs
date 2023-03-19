using PracticeApi.Models;
namespace PracticeApi.Repositories
{
    public interface IHeroRepository : IRepositoryBase<Hero>
    {

        Task<IEnumerable<HeroDTO>> SearchHero(string term);
        Task<IEnumerable<Hero>> SearchHeroMultiple(HeroDTO SearchObj);
        bool IsExists(long id);
    }
}