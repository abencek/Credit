using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Credit.Web.Models;


namespace Credit.Web.Components
{
    /// <summary>
    /// Customer table for Customer view
    /// </summary>
    public class CustomerTableViewComponent : ViewComponent {

        public  IViewComponentResult Invoke (IEnumerable<CustomerViewModel> customers) {

            return View(customers);

        }

    }
}
