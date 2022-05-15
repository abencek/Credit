using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Credit.Data.App;
using Credit.Data.App.Models;
using Credit.Api.Dtos;
using Credit.Api.Infrastructure;
using Credit.Api.Mapping;

namespace Credit.Web.Controllers
{
    /// <summary>
    /// API for Agreement table
    /// This can retrieve or update Agreement data
    /// </summary>

    [Authorize]
    [Route("api/agreement")]
    public class AgreementController : Controller
    {
        private readonly IAppAgreementRepository repository;

        public AgreementController (IAppAgreementRepository repo){
            repository=repo;
        }

        
        [HttpGet("{id}",Name = "apiagreementget")]
        public AgreementDto GET (int id){

            Agreement agreement = repository.Agreements
                .Where(x => x.AgreementId == id)
                .FirstOrDefault();

            var agreementDto = Mapper.Instance.Map<AgreementDto>(agreement);
                
            return agreementDto; 
            
        }

      

        [HttpPost("{id}")]
        public JsonResult UPDATE ([FromForm] AgreementDto agreementDto){             

            var agreement = Mapper.Instance.Map<Agreement>(agreementDto);

            bool result = repository.SaveAgreement(agreement);

            if (result) {
                return Json(new string[] {
                    agreement.AgreementId.ToString(),
                    agreement.ProductType,
                    agreement.ProductTerm,
                    agreement.IssueValue.ToString(),
                    agreement.StartDate.ToString(JsonDateConverter.DefaultDateTimeFormat),
                    agreement.PaidUpDate.ValueOrNullToString(JsonDateConverter.DefaultDateTimeFormat),
                });
            }
            else
            {
                return Json(Array.Empty<string>());
            }

        }

    }
}
