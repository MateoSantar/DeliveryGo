using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enums;
using interfaces;

namespace classes.Core.Payment
{
    class PagoMp : IPago
    {
        public PagoNombre Nombre => PagoNombre.mp;

        public bool Procesar(decimal monto)
        {
            Console.WriteLine($"Monto a pagar con MercadoPago: {monto}");
            return true;
        }
    }
}
