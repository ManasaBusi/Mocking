using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardApplications
{
    
    public class Processor
    {
        private readonly IGateway _gateway;
        public Processor(IGateway gateway) 
        {
            _gateway = gateway;
        }

        public bool Process(Person person)
        {
            int returnCode = _gateway.Execute(ref person);
            
            return returnCode == 0;
        }
    }
}
