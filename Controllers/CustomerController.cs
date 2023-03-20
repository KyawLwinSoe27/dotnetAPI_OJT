using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApi.Models;
using PracticeApi.Repositories;
using PracticeApi.Util;

namespace PracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        public CustomerController(IRepositoryWrapper RW) {
            _repositoryWrapper = RW;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> getCustomers(){
            var customers = await _repositoryWrapper.Customer.FindAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> getCustomerById(int id){
            var customer = await _repositoryWrapper.Customer.FindByIDAsync(id);
            if(customer == null)
            {
                return BadRequest();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> postCustomerType(CustomerRequest customerRequest)
        {
            var customer = new Customer
            {
                CustomerName = customerRequest.CustomerName,
                CustomerRegisterDate = customerRequest.CustomerRegisterDate,
                CustomerAddress = customerRequest.CustomerAddress,
                CustomerTypeId = customerRequest.CustomerTypeId
            };
            await _repositoryWrapper.Customer.CreateAsync(customer,true);
            if(customerRequest.Photo != null && customerRequest.Photo != "")
            {
                FileService.MoveTempFileDir("CustomerPhoto",customer.Id.ToString(),customerRequest.Photo);
                // FileService.MoveTempFile("CustomerPhoto",customer.Id.ToString(),customerRequest.Photo);
            }
            return CreatedAtAction(nameof(getCustomerById),new {id = customerRequest.Id},customerRequest );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> putCustomerType(int id, CustomerRequest customerRequest)
        {

            if(id != customerRequest.Id){
                return BadRequest();
            }

            Customer? customer;
            try{
                customer = await _repositoryWrapper.Customer.FindByIDAsync(id);
                if(customer == null)
                {
                    return NotFound();
                }
                customer.CustomerName = customerRequest.CustomerName;
                customer.CustomerRegisterDate = customerRequest.CustomerRegisterDate;
                customer.CustomerAddress = customerRequest.CustomerAddress;
                customer.CustomerTypeId = customerRequest.CustomerTypeId;

                await _repositoryWrapper.Customer.UpdateAsync(customer);
                if(customerRequest.Photo != null && customerRequest.Photo != "")
                {
                    // FileService.MoveTempFileDir("CustomerPhoto",customer.Id.ToString(),customerRequest.Photo);
                    FileService.DeleteFileNameOnly("CustomerPhoto",customer.Id.ToString());
                    FileService.MoveTempFile("CustomerPhoto",customer.Id.ToString(),customerRequest.Photo);
                }
                return Ok(customer);
            }catch(DbUpdateConcurrencyException){
                if(!isCustomerExists(id))
                {
                    return NotFound();
                }else{
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> deleteCustomer(int id)
        {
            var customer = await _repositoryWrapper.Customer.FindByIDAsync(id);
            if(customer == null)
            {
                return BadRequest();
            }
            FileService.DeleteFileNameOnly("CustomerPhoto",customer.Id.ToString());
            await _repositoryWrapper.Customer.DeleteAsync(customer,true);
            return NoContent();
        }

        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomer(string term){
            var customerList = await _repositoryWrapper.Customer.SearchCustomer(term);
            return Ok(customerList);
        }

        [HttpPost("SearchCustomer")]
        public async Task<ActionResult<IEnumerable<CustomerResult>>> ListCustomer(){
            var customerList = await _repositoryWrapper.Customer.ListCustomer();
            return Ok(customerList);
        }
        private bool isCustomerExists(int id)
        {
            return _repositoryWrapper.Customer.isExists(id);
        }
    }
}