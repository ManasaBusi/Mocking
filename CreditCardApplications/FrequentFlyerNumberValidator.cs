using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardApplications
{
    public class FrequentFlyerNumberValidatorService : IFrequentFlyerNumberValidator
    {
        IServiceInformation IFrequentFlyerNumberValidator.ServiceInformation => throw new NotImplementedException("For demo purposes");

        ValidationMode IFrequentFlyerNumberValidator.ValidationMode
        {
            get => throw new NotImplementedException("For demo purposes");
            set => throw new NotImplementedException("For demo purposes");
        }

        public bool IsValid(string frequentFlyerNumber)
        {
            throw new NotImplementedException("Simulate this real dependency being hard to use");
        }

        public void IsValid(string frequentFlyerNumber, out bool isValid)
        {
            throw new NotImplementedException("Simulate this real dependency being hard to use");
        }
    }
}
