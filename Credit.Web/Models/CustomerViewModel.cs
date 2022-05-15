using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Newtonsoft.Json;

using Credit.Api.Infrastructure;

namespace Credit.Web.Models
{
    public class CustomerViewModel
    {
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Personal number")]
        public string PersonalId { get; set; }

        [DisplayName("Birth date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [JsonConverter(typeof(JsonDateConverter), "dd.MM.yyyy")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Status")]
        public string CustomerStatusValue { get; set; }

        public int CustomerStatusId { get; set; }
    }
}
