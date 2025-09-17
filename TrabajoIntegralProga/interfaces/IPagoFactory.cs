using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfaces
{
    interface IPagoFactory
    {
        IPago Create(IPago tipo);
    }
}
