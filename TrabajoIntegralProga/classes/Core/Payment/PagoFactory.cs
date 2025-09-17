using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enums;
namespace classes.Core.Payment
{
    class PagoFactory : IPagoFactory
    {
        IPago? IPagoFactory.Create(IPago tipo)
        {
            switch (tipo.Nombre)
            {
                case PagoNombre.tarjeta:
                    return new PagoTarjeta();
                case enums.PagoNombre.transf:
                    return new PagoTransfer();
                case enums.PagoNombre.mp:
                    return new PagoMp();
                default:
                    Console.WriteLine("Hubo un error en Factory");
                    break;
            }
            return null;
        }
    }
}
