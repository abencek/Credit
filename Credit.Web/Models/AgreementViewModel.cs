using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Newtonsoft.Json;
using Credit.Api.Infrastructure;

namespace Credit.Web.Models
{
    public class AgreementViewModel
    {
        [DisplayName("Agreement ID")]
        public long AgreementId { get; set; }

        [DisplayName("Product type")]
        public string ProductType { get; set; }

        [DisplayName("Product term")]
        public string ProductTerm { get; set; }

        [DisplayName("Loan amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D0}")]
        public int IssueValue { get; set; }

        [DisplayName("Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [JsonConverter(typeof(JsonDateConverter), "dd.MM.yyyy")]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [JsonConverter(typeof(JsonDateConverter), "dd.MM.yyyy")]
        public DateTime? PaidUpDate { get; set; }
    }
}
