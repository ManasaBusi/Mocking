using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardApplications
{
    public interface IGateway
    {
        int Execute(ref Person person);
    }
}
