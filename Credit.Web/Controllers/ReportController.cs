using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Credit.Data.App;
using Credit.Web.Models;
using Credit.Web.Infrastructure;


namespace Credit.Web.Controllers
{
    /// <summary>
    /// Tool for exporting application data into xlsx format
    /// This contains predefined reports that can be changed in the application anytime
    /// </summary>

    [Authorize]
    public class ReportController : Controller
    {
 

        private readonly IAppCustomerRepository repoCustomer;
        private readonly IAppAgreementRepository repoAgreement;

        public ReportController(IAppCustomerRepository repoCust, IAppAgreementRepository repoAgr)
        {
            repoCustomer = repoCust;
            repoAgreement = repoAgr;
        }


        public ActionResult Index()
        {
            ViewBag.SideBarItemSelected = "Reports";

            //Create list of sample reports
            IEnumerable<ReportIndexViewModel> mockReports = new List<ReportIndexViewModel> {
                new ReportIndexViewModel {ReportId=1, ReportGroup='A', ReportGroupId=1, ReportName="Agreements Example 1"},
                new ReportIndexViewModel {ReportId=2, ReportGroup='A', ReportGroupId=2, ReportName="Agreements Example 2"},
                new ReportIndexViewModel {ReportId=3, ReportGroup='A', ReportGroupId=3, ReportName="Agreements Example 3"},
                new ReportIndexViewModel {ReportId=1, ReportGroup='A', ReportGroupId=4, ReportName="Agreements Example 4"},
                new ReportIndexViewModel {ReportId=2, ReportGroup='A', ReportGroupId=5, ReportName="Agreements Example 5"},
                new ReportIndexViewModel {ReportId=4, ReportGroup='C', ReportGroupId=1, ReportName="Customers"},
            };

            return View(mockReports);
        }


        public ActionResult CreateReport(int reportId)
        {
            //Export data to xlsx format
            string fileName = "Report.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            MemoryStream stream = new();

            //Select report by ID
            switch (reportId)
            {
                case 1:
                    stream = OpenXMLSpreadsheet.CreateExcelDocumentStream(
                        repoAgreement.Agreements.Select(x=> new {
                            x.AgreementId,
                            x.ProductType,
                            x.ProductTerm,
                            x.IssueValue,
                            x.StartDate,
                            x.PaidUpDate,
                            x.Customer.CustomerId
                        }).ToList()
                    );
                    break;
                case 2:
                    stream = OpenXMLSpreadsheet.CreateExcelDocumentStream(
                        repoAgreement.Agreements.Where(x => x.PaidUpDate != null).Select(x=> new {
                            x.AgreementId,
                            x.ProductType,
                            x.ProductTerm,
                            x.IssueValue,
                            x.StartDate,
                            x.PaidUpDate,
                            x.Customer.CustomerId
                        }).ToList()
                    );
                    break;
                case 3:
                    stream = OpenXMLSpreadsheet.CreateExcelDocumentStream(
                        repoAgreement.Agreements.Where(x => x.PaidUpDate == null).Select(x=> new {
                            x.AgreementId,
                            x.ProductType,
                            x.ProductTerm,
                            x.IssueValue,
                            x.StartDate,
                            x.PaidUpDate,
                            x.Customer.CustomerId
                        }).ToList()
                    );
                    break;
                case 4:
                    stream = OpenXMLSpreadsheet.CreateExcelDocumentStream(
                        repoCustomer.Customers
                        .Select(x=> new {
                            x.CustomerId,
                            x.FirstName,
                            x.LastName,
                            x.PersonalId,
                            x.BirthDate,
                            x.CustomerStatus.CustomerStatusValue
                        }).ToList()
                    );
                    break;
            }

            if (stream.Length > 0)
            {
                return File(stream, fileType, fileName);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
