using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApi.Models;
using PracticeApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLevelController : ControllerBase
    {
        // private readonly TodoContext _context; before repositry
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AdminLevelController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Hero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminLevel>>> GetAllAdminLevels()
        {
            var adminLevels = await _repositoryWrapper.AdminLevel.FindAllAsync();
            return Ok(adminLevels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AdminLevel>>> GetSingleAdminLevel(int id)
        {
            if(_repositoryWrapper.AdminLevel == null)
            {
                return NotFound();
            }
            var adminLevel = await _repositoryWrapper.AdminLevel.FindByIDAsync(id);
            if(adminLevel == null){
                return NotFound();
            }

            return Ok(adminLevel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<AdminLevel>>> UpdateAdminLevel(int id, AdminLevel adminLevel)
        {
            if(id != adminLevel.Id)
            {
                return BadRequest();
            }
            AdminLevel? objAdminLevel;
            try{
                objAdminLevel = await _repositoryWrapper.AdminLevel.FindByIDAsync(id);
                if (objAdminLevel == null) 
                    throw new Exception("Invalid Admin Level Id");
                
                objAdminLevel.AdminLevelName = adminLevel.AdminLevelName;
                
                
                await _repositoryWrapper.AdminLevel.UpdateAsync(objAdminLevel);
                await _repositoryWrapper.EventLog.Update(objAdminLevel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminLevelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Heroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminLevel>> PostAdminLevel(AdminLevel adminLevel)
        {
            await _repositoryWrapper.AdminLevel.CreateAsync(adminLevel, true);
            await _repositoryWrapper.EventLog.Insert(adminLevel);
            return CreatedAtAction(nameof(GetSingleAdminLevel), new { id = adminLevel.Id }, adminLevel);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminLevel(int id)
        {
            var adminLevel = await _repositoryWrapper.AdminLevel.FindByIDAsync(id);
            if (adminLevel == null)
            {
                return NotFound();
            }
            await _repositoryWrapper.EventLog.Delete(adminLevel);
            await _repositoryWrapper.AdminLevel.DeleteAsync(adminLevel, true);
            
            return NoContent();
        }

        //Search
        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<AdminLevel>>> SearchAdminLevelName(string term)
        {
            var adminLevel = await _repositoryWrapper.AdminLevel.SearchAdminLevelName(term);
            return Ok(adminLevel);
        }


        private bool AdminLevelExists(long id)
        {
            return (_repositoryWrapper.AdminLevel.IsExists(id));
        }

    }
}