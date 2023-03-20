using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApi.Models;
using PracticeApi.Repositories;

namespace PracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        public CustomerTypeController(IRepositoryWrapper RW){
            _repositoryWrapper = RW;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerType>>> getCustomerType(){
            var customerType = await _repositoryWrapper.CustomerType.FindAllAsync();
            return Ok(customerType);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerType>> getCustomerTypeById(int id){
            var customerType = await _repositoryWrapper.CustomerType.FindByIDAsync(id);
            if(customerType == null)
            {
                return BadRequest();
            }
            return Ok(customerType);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerType>> postCustomerType(CustomerType customerType)
        {
            await _repositoryWrapper.CustomerType.CreateAsync(customerType,true);
            await _repositoryWrapper.EventLog.Insert(customerType);
            return CreatedAtAction("getCustomerTypeById",new{id = customerType.Id}, customerType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerType>> putCustomerType(int id, CustomerType customerType)
        {
            if(id != customerType.Id)
            {
                return BadRequest();
            }
            try{
                var objCustomerType =  await _repositoryWrapper.CustomerType.FindByIDAsync(id);
                if(objCustomerType == null){
                    throw new Exception("Invalid ID");
                }
                objCustomerType.CustomerTypeName = customerType.CustomerTypeName;
                objCustomerType.CustomerTypeDescription = customerType.CustomerTypeDescription;
                await _repositoryWrapper.CustomerType.UpdateAsync(objCustomerType);
                await _repositoryWrapper.EventLog.Update(objCustomerType);

                return Ok(objCustomerType);
            }catch(DbUpdateConcurrencyException){
                if(!isCustomerTypeExists(id))
                {
                    return NotFound();
                }else{
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerType>> deleteCustomerType(int id)
        {
            var customerType = await _repositoryWrapper.CustomerType.FindByIDAsync(id);
            if(customerType == null)
            {
                return BadRequest();
            }
            await _repositoryWrapper.EventLog.Delete(customerType);
            await _repositoryWrapper.CustomerType.DeleteAsync(customerType,true);
            return NoContent();
        }
        private bool isCustomerTypeExists(int id)
        {
            return _repositoryWrapper.CustomerType.isExists(id);
        }
    }
}