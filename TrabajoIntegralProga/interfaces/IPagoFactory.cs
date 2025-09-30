using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enums;
namespace interfaces
{
    interface IPagoFactory
    {
        IPago Create(PagoNombre tipo);
    }
}
