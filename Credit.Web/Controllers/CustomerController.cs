using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using Credit.Data.App;
using Credit.Web.Models;



namespace Credit.Web.Controllers
{
    /// <summary>
    /// Controller for Customer view
    /// </summary>

    [Authorize]
    public class CustomerController : Controller
    {

        private readonly IAppCustomerRepository repository;

        public CustomerController (IAppCustomerRepository repo){

            repository=repo;

        }


        public IActionResult Index(CustomerIndexFilterViewModel filter, int page){

            ViewBag.SideBarItemSelected="Customers";
            //Populate dropdown for view
            ViewBag.CustomerStatusSelectList = repository.CustomerStatuses
                .Select(x=>new SelectListItem(){
                    Value=x.CustomerStatusId.ToString(),
                    Text=x.CustomerStatusValue
                }).ToList();

            //Create pagination
            int itemsPerPage = 6;
            IEnumerable<CustomerViewModel> customers = repository.Customers
                .Where(x=> filter.FilterCustomerId==null || filter.FilterCustomerId==x.CustomerId)
                .Where(x=> filter.FilterFirstName==null || filter.FilterFirstName==x.FirstName)
                .Where(x=> filter.FilterLastName==null || filter.FilterLastName==x.LastName)
                .Where(x=> filter.FilterPersonalId==null || filter.FilterPersonalId==x.PersonalId)
                .Select(x=>new CustomerViewModel {
                    CustomerId = x.CustomerId,
                    FirstName = x.FirstName,                         
                    LastName = x.LastName,                              
                    PersonalId = x.PersonalId,                          
                    BirthDate = x.BirthDate,                             
                    CustomerStatusValue = x.CustomerStatus.CustomerStatusValue
                });
            
            int totalPages = (int) Math.Ceiling((decimal)customers.Count()/itemsPerPage);
            customers=customers.Skip((page-1)*itemsPerPage).Take(itemsPerPage);

            //Create view model
            CustomerIndexViewModel model = new()
            {
                Customers=customers,
                FilterItems=filter,
                PagingInfo = new PagingInfoViewModel{
                    TotalPages=totalPages,
                    ItemsPerPage = itemsPerPage,
                    CurrentPage = page
                }
            };

            return View(model);   
        }
    
    }
}
