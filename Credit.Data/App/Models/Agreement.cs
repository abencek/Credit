using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace Credit.Data.App.Models
{   
    public class Agreement
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AgreementId { get; set; }

        public string ProductType { get; set; }

        public string ProductTerm { get; set; }

        public int IssueValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PaidUpDate { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
