using Newtonsoft.Json;


namespace Credit.Data.App.Models
{   
    public class Address
    {
        public	int	AddressId { get; set; }

        public	AddressType AddressType	{ get; set; }

        public	string City	{ get; set; }

        public	string Street { get; set; }

        public	string Zip	{ get; set; }

        [JsonIgnore]
        public	virtual Customer Customer { get; set; }
    }

}
