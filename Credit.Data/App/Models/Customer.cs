using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace Credit.Data.App.Models
{   
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public	int	CustomerId { get; set; }

        public	string	FirstName { get; set; }

        public	string	LastName { get; set; }

        public	string	PersonalId { get; set; }

        [DataType(DataType.Date)]
        public	DateTime BirthDate { get; set; }

        public int CustomerStatusID { get; set; }

        [JsonIgnore]
        public	virtual CustomerStatus	CustomerStatus { get; set; }
    }
}
