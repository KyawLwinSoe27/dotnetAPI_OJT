using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApi.Models;
using PracticeApi.Repositories;

namespace PracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        // private readonly PracticalContext _context;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public HeroController(IRepositoryWrapper RW){
            _repositoryWrapper = RW;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroDTO>>> getHeroes()
        {
            var hero =  await _repositoryWrapper.Hero.FindAllAsync();
            return Ok(hero);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HeroDTO>> getHero(int id)
        {
            var hero = await _repositoryWrapper.Hero.FindByIDAsync(id);
            if(hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<Hero>> postHero(Hero hero)
        {
            
            await _repositoryWrapper.Hero.CreateAsync(hero,true);
            await _repositoryWrapper.EventLog.Insert(hero);
            return CreatedAtAction("getHero",new {id = hero.HeroId}, hero);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Hero>> putHero(int id, Hero hero)
        {
            if(id != hero.HeroId)
            {
                return BadRequest();
            }
            try
            {
                var objHero = await _repositoryWrapper.Hero.FindByIDAsync(id);
                if(objHero == null)
                {
                    throw new Exception("Invalid ID");
                }
                objHero.HeroName = hero.HeroName;
                objHero.HeroAddress = hero.HeroAddress;
                objHero.HeroSecret = hero.HeroSecret;
                await _repositoryWrapper.Hero.UpdateAsync(objHero);
                await _repositoryWrapper.EventLog.Update(objHero);
                return Ok(objHero);
            }catch(DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Hero>> deleteHero(int id)
        {
            if(_repositoryWrapper.Hero == null){
                return NotFound();
            }
            var hero = await _repositoryWrapper.Hero.FindByIDAsync(id);
            if(hero == null )
            {
                return BadRequest();
            }
            await _repositoryWrapper.EventLog.Delete(hero);
            await _repositoryWrapper.Hero.DeleteAsync(hero,true);
            return NoContent();
        }

        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<HeroDTO>>> SearchHero(string term)
        {
            var heroList = await _repositoryWrapper.Hero.SearchHero(term);
            return Ok(heroList);
        }

        [HttpPost("searchobj")]
        public async Task<ActionResult<IEnumerable<Hero>>> SearchHero(HeroDTO SearchObj)
        {
            var heroList = await _repositoryWrapper.Hero.SearchHeroMultiple(SearchObj);
            return Ok(heroList);
        }
        

        private bool HeroExists(int id)
        {
            return (_repositoryWrapper.Hero.IsExists(id));
        }

        private static HeroDTO heroDTO(Hero hero) => 
            new HeroDTO {
                HeroId = hero.HeroId,
                HeroName = hero.HeroName,
                HeroAddress = hero.HeroAddress
            };

    }
}