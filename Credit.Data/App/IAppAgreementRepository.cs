using System.Collections.Generic;
using Credit.Data.App.Models;

namespace Credit.Data.App
{
    public interface IAppAgreementRepository
    {
        IEnumerable <Agreement> Agreements {get;}
        bool SaveAgreement(Agreement agreement);
    }
}
