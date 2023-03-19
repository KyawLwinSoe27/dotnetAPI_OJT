using PracticeApi.Repositories;
using PracticeApi.Models;
namespace PracticeApi.Repositories
{
    public interface IRepositoryWrapper
    {
        IHeroRepository Hero { get; }
        
    }
}
