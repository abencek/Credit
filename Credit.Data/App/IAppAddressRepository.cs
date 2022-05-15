using System.Collections.Generic;
using Credit.Data.App.Models;

namespace Credit.Data.App
{
    public interface IAppAddressRepository
    {
        IEnumerable <Address> Addresses {get;}
        
    }
}
