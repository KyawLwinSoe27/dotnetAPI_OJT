using System.Data;
using PracticeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PracticeApi.Repositories
{
    public class HeroRepository : RepositoryBase<Hero>, IHeroRepository
    {
        public HeroRepository(PracticalContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<HeroDTO>> SearchHero(string term)
        {
            return await RepositoryContext.Heroes
                        .Where(s => s.HeroName!.Contains(term))
                        .Select(e => new HeroDTO{
                            HeroId = e.HeroId,
                            HeroName = e.HeroName,
                            HeroAddress = e.HeroAddress
                        }).OrderBy(s => s.HeroId).ToListAsync();
        }

        public async Task<IEnumerable<Hero>> SearchHeroMultiple(HeroDTO SearchObj)
        {
            return await RepositoryContext.Heroes
                        .Where(s => s.HeroName!.Contains(SearchObj.HeroName ?? "") || s.HeroAddress!.Contains(SearchObj.HeroAddress ?? ""))
                        .OrderBy(s => s.HeroId).ToListAsync();
        }

        public bool IsExists(long id)
        {
            return RepositoryContext.Heroes.Any(e => e.HeroId == id);
        }
    }

}