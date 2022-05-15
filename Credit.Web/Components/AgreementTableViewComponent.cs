using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Credit.Web.Models;


namespace Credit.Web.Components
{
    /// <summary>
    /// Agreement table for Agreement view
    /// </summary>
    public class AgreementTableViewComponent : ViewComponent {

        public  IViewComponentResult Invoke (IEnumerable<AgreementViewModel> agreements) {

            return View(agreements);
        }

    }
}
