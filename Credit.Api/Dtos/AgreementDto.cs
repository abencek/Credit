using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Credit.Api.Infrastructure;


namespace Credit.Api.Dtos
{   
    public class AgreementDto
    {   
        [DisplayName ("Agreement ID")]
        public long AgreementId { get; set; }

        [DisplayName ("Product type")]
        public string ProductType { get; set; }

        [DisplayName ("Product term")]
        public string ProductTerm { get; set; }

        [DisplayName ("Loan amount")]
        public int IssueValue { get; set; }

        [DisplayName ("Start date")]
        [JsonConverter(typeof(JsonDateConverter), "dd.MM.yyyy")]
        public DateTime StartDate { get; set; }

        [DisplayName ("End date")]
        [JsonConverter(typeof(JsonDateConverter), "dd.MM.yyyy")]
        public DateTime? PaidUpDate { get; set; }
    }
}
