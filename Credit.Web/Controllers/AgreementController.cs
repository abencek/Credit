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
    /// Controller for Agreement view
    /// </summary>

    [Authorize]
    public class AgreementController : Controller
    {
        
        private readonly IAppAgreementRepository repository;

        public AgreementController (IAppAgreementRepository repo){
            repository=repo;
        }


        public IActionResult Index(AgreementIndexFilterViewModel filter, int page){

            ViewBag.SideBarItemSelected="Agreements";
            //Populate dropdown for view
            ViewBag.ProductTypeSelectList = new SelectList(new List<SelectListItem> {
                    new SelectListItem(){Value="Monthly",Text="Monthly"}, 
                    new SelectListItem(){Value="Weekly",Text="Weekly"}
            },"Value","Text");

            //Create pagination
            int itemsPerPage = 6;
            IEnumerable<AgreementViewModel> agreements = repository.Agreements
                .Where(x=> filter.FilterAgreementId==null || filter.FilterAgreementId==x.AgreementId)
                .Where(x=> filter.FilterProductType==null || filter.FilterProductType==x.ProductType)
                .Where(x=> filter.FilterProductTerm==null || filter.FilterProductTerm==x.ProductTerm)
                .Where(x=> filter.FilterIssueValue==null || filter.FilterIssueValue==x.IssueValue)
                .Select(x=>new AgreementViewModel {
                    AgreementId = x.AgreementId,
                    ProductType = x.ProductType,
                    ProductTerm = x.ProductTerm,
                    IssueValue = x.IssueValue,
                    StartDate = x.StartDate,
                    PaidUpDate = x.PaidUpDate
                });

            int totalPages = (int) Math.Ceiling((decimal)agreements.Count()/itemsPerPage);
            agreements = agreements.Skip((page-1)*itemsPerPage).Take(itemsPerPage);

            //Create view model
            AgreementIndexViewModel model = new()
            {
                Agreements=agreements,
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
