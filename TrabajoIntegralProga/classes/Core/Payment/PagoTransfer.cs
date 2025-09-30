using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enums;

namespace classes.Core.Payment
{
    class PagoTransfer : IPago
    {
        public PagoNombre Nombre => PagoNombre.transf;

        public bool Procesar(decimal monto)
        {
            Console.WriteLine($"[Transferencia] Monto a pagar: {monto}");
            return true;
        }
    }
}
