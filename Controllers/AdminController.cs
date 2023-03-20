using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApi.Models;
using PracticeApi.Repositories;
using PracticeApi.Util;

namespace PracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AdminController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Hero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminResult>>> GetAllAdmins()
        {
            var admin = await _repositoryWrapper.Admin.FindAllAsync();
            return Ok(admin);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AdminResult>>> GetSingleAdmin(int id)
        {
            if(_repositoryWrapper.Admin == null)
            {
                return NotFound();
            }
            var admin = await _repositoryWrapper.Admin.FindByIDAsync(id);
            if(admin == null){
                return NotFound();
            }

            return Ok(admin);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Admin>>> UpdateCustomer(int id, AdminRequest adminRequest)
        {
            if(id != adminRequest.Id)
            {
                return BadRequest();
            }
            Admin? objAdmin;
            try{
                objAdmin = await _repositoryWrapper.Admin.FindByIDAsync(id);
                if (objAdmin == null) 
                    throw new Exception("Invalid Admin ID");
                
    
                objAdmin.AdminName = adminRequest.AdminName;
                objAdmin.Email = adminRequest.Email;
                objAdmin.LoginName = adminRequest.LoginName;
                objAdmin.Password = adminRequest.Password;
                objAdmin.Inactive = adminRequest.Inactive;
                objAdmin.AdminLevelId = adminRequest.AdminLevelId;
                
                await _repositoryWrapper.Admin.UpdateAsync(objAdmin);
                await _repositoryWrapper.EventLog.Update(objAdmin);

                if(adminRequest.AdminPhoto != null || adminRequest.AdminPhoto != ""){
                    FileService.DeleteFileNameOnly("AdminPhoto", objAdmin.Id.ToString());
                    FileService.MoveTempFile("AdminPhoto", objAdmin.Id.ToString(), adminRequest.AdminPhoto);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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
        public async Task<ActionResult<Admin>> PostCustomer(AdminRequest adminRequest)
        {
            var admin = new Admin 
            {
                Id = adminRequest.Id,
                AdminName = adminRequest.AdminName,
                Email = adminRequest.Email,
                LoginName = adminRequest.LoginName,
                Inactive = adminRequest.Inactive,
                AdminLevelId = adminRequest.AdminLevelId
            };
            var password = adminRequest.Password;
            if(password.ToString().Length < 8)
            {
                throw new ValidationException("Invalid Password");
            }
            else
            {
                string salt = Util.SaltedHash.GenerateSalt();
                password = Util.SaltedHash.ComputeHash(salt, password.ToString());
                admin.Password = password;
                admin.Salt = salt;
            }
            await _repositoryWrapper.Admin.CreateAsync(admin, true);
            await _repositoryWrapper.EventLog.Insert(admin);
            if( adminRequest.AdminPhoto != null && adminRequest.AdminPhoto != "")
            {
                FileService.MoveTempFile("AdminPhoto" , admin.Id.ToString(), adminRequest.AdminPhoto);
                // FileService.MoveTempFileDir("AdminPhoto", admin.Id.ToString(), adminRequest.AdminPhoto);
            }
            
            return CreatedAtAction(nameof(GetSingleAdmin), new { id = admin.Id }, adminRequest);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _repositoryWrapper.Admin.FindByIDAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            FileService.DeleteFileNameOnly("AdminPhoto", admin.Id.ToString());
            await _repositoryWrapper.EventLog.Delete(admin);
            await _repositoryWrapper.Admin.DeleteAsync(admin, true);
            
            return NoContent();
        }

        //Search
        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<Admin>>> SearchAdmin(string term)
        {
            var adminList = await _repositoryWrapper.Admin.SearchAdmin(term);
            return Ok(adminList);
        }


        [HttpPost("searchadmin")] 
        public async Task<ActionResult<IEnumerable<Admin>>> ListCustomer()
        {
            var adminList = await _repositoryWrapper.Admin.ListAdmins();
            return Ok(adminList);
        }

        private static AdminResult adminResult(Admin admin) => 
            new AdminResult {
                Id = admin.Id,
                AdminName = admin.AdminName,
                Email = admin.Email,
                LoginName = admin.LoginName,
                Inactive = admin.Inactive,
                AdminLevelId = admin.AdminLevelId
            };

    
        private bool AdminExists(long id)
        {
            return (_repositoryWrapper.Admin.IsExists(id));
        }

    }
}