using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Credit.Data.App;
using Credit.Web.Models;



namespace Credit.Web.Controllers
{
    /// <summary>
    /// Detailed information about selected customer
    /// </summary>

    [Authorize]
    public class DetailController : Controller
    {

        private readonly IAppCustomerRepository repoCustomer;
        private readonly IAppAddressRepository repoAddress;
        private readonly IAppAgreementRepository repoAgreement;
        private readonly IAppCommentRepository repoComment;

        public DetailController (IAppCustomerRepository repoCust, IAppAgreementRepository repoAgr, 
                IAppAddressRepository repoAddr, IAppCommentRepository repoCom){
            repoCustomer = repoCust;
            repoAddress = repoAddr;
            repoAgreement = repoAgr;
            repoComment = repoCom;
        }


        public IActionResult Index(int id, string tab = "Agreements"){

            //Hide sidebar menu
            ViewBag.SideBarItemSelected=null;

            //Get data for model
            CustomerViewModel customer = repoCustomer.Customers
                    .Where(x=>x.CustomerId==id)
                    .Select(x=>new CustomerViewModel {
                        CustomerId = x.CustomerId,
                        FirstName = x.FirstName,                         
                        LastName = x.LastName,                              
                        PersonalId = x.PersonalId,                          
                        BirthDate = x.BirthDate,                             
                        CustomerStatusValue = x.CustomerStatus.CustomerStatusValue
                    }).FirstOrDefault();
            AddressViewModel address1=repoAddress.Addresses
                    .Where(x=>x.Customer.CustomerId==id && x.AddressType.AddressTypeId==1)
                    .Select(x=> new AddressViewModel {
                        AddressId=x.AddressId,	
                        AddressTypeValue=x.AddressType.AddressTypeValue, 
                        City=x.City,	
                        Street=x.Street,	
                        Zip=x.Zip
                    }).FirstOrDefault();
             AddressViewModel address2=repoAddress.Addresses
                    .Where(x=>x.Customer.CustomerId==id && x.AddressType.AddressTypeId==2)
                    .Select(x=> new AddressViewModel {
                        AddressId=x.AddressId,	
                        AddressTypeValue=x.AddressType.AddressTypeValue, 
                        City=x.City,	
                        Street=x.Street,	
                        Zip=x.Zip
                    }) .FirstOrDefault();
            IEnumerable<AgreementViewModel> agreements = repoAgreement.Agreements
                    .Where(x=>x.Customer.CustomerId==id)
                    .Select(x=>new AgreementViewModel {
                        AgreementId = x.AgreementId,
                        ProductType = x.ProductType,
                        ProductTerm = x.ProductTerm,
                        IssueValue = x.IssueValue,
                        StartDate = x.StartDate,
                        PaidUpDate = x.PaidUpDate
                    }).ToList();
            IEnumerable<CommentViewModel> comments = repoComment.Comments
                    .Where(x=>x.Customer.CustomerId==id)
                    .Select(x=>new CommentViewModel {
                        CommentId = x.CommentId,
                        CommentType = x.CommentType.CommentTypeValue,
                        CommentDate = x.CommentDate,
                        CommentDescription = x.CommentDescription
                    }).ToList();

            DetailIndexViewModel model = new()
            {
                CustomerInfo=customer,
                Address1=address1,
                Address2=address2,
                ActiveTab=tab,
                TabAgreementsData= agreements,
                TabCommentsData= comments
            };

            
            return View(model);   
        }

    }
}
