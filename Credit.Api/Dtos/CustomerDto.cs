using System;
using System.ComponentModel;
using Newtonsoft.Json;

using Credit.Api.Infrastructure;


namespace Credit.Api.Dtos
{   
    public class CustomerDto
    {
        [DisplayName ("Customer ID")]
        public	int	CustomerId { get; set; }

        [DisplayName ("First name")]
        public	string	FirstName { get; set; }

        [DisplayName ("Last name")]
        public	string	LastName { get; set; }

        [DisplayName ("Personal number")]
        public	string	PersonalId { get; set; }

        [DisplayName ("Birth date")]
        [JsonConverter(typeof(JsonDateConverter),"dd.MM.yyyy")]
        public	DateTime BirthDate { get; set; }

        [DisplayName ("Status")]
        public int CustomerStatusId { get; set; }
    }
}
