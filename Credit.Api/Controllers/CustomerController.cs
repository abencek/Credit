using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using Credit.Data.App;
using Credit.Data.App.Models;
using Credit.Api.Dtos;
using Credit.Api.Infrastructure;
using Credit.Api.Mapping;

namespace Credit.Web.Controllers
{
    /// <summary>
    /// API for Customer table
    /// This can retrieve or update Customer data
    /// </summary>

    [Route("api/customer")]
    public class CustomerController : Controller
    {

        private readonly IAppCustomerRepository repository;

        public CustomerController (IAppCustomerRepository repo){
            repository=repo;
        }

        
        [HttpGet("{id}",Name = "apicustomerget")]
        public CustomerDto GET (int id){

            var customer = repository.Customers
                .Where(x => x.CustomerId == id)
                .FirstOrDefault();

            var customerDto = Mapper.Instance.Map<CustomerDto>(customer);

            return customerDto;
        }
    

        [HttpPost("{id}")]
        public JsonResult UPDATE ([FromForm] CustomerDto customerDto){             

            var customer = Mapper.Instance.Map<Customer>(customerDto);

            bool result = repository.SaveCustomer(customer);

            //Create response
            if (result) {
                customer = repository.Customers
                       .Where(x => x.CustomerId == customerDto.CustomerId)
                       .FirstOrDefault();

                return Json(new string[] {
                    customer.CustomerId.ToString(),
                    customer.FirstName,
                    customer.LastName,
                    customer.PersonalId,
                    customer.BirthDate.ToString(JsonDateConverter.DefaultDateTimeFormat),
                    customer.CustomerStatus.CustomerStatusValue
                });
            }
             else
            {
                return Json(Array.Empty<string>());
            }

        }
    }
}
