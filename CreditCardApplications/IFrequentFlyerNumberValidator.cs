using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardApplications
{
    public interface ILicenseData
    {
        string LicenseKey { get; }
    }

    public interface IServiceInformation
    {
        ILicenseData License { get; }
    }

    public interface IFrequentFlyerNumberValidator
    {
        bool IsValid(string frequentFlyerNumber);

        void IsValid(string frequentFlyerNumber, out bool isValid);
        //string LicenseKey { get; }

        IServiceInformation ServiceInformation { get; }

        ValidationMode ValidationMode { get; set; }


    }
}
