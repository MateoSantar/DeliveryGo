using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enums;

namespace classes.Core.Payment
{
    class PagoTarjeta : IPago
    {
        public PagoNombre Nombre => PagoNombre.tarjeta;

        public bool Procesar(decimal monto)
        {
            Console.WriteLine($"Monto a pagar con tarjeta: {monto}");
            return true;
        }
    }
}
