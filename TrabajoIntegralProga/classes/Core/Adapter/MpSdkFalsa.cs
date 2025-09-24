using classes.Core.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes.Core.Adapter
{
    class MpSdkFalsa
    {
        public bool Cobrar(decimal monto)
        {
            Console.WriteLine($"[SDK] Cobrando {monto} con exito.");
            return true;
        }
        
        public PagoMp SimularPagoMp()
        {
            return new PagoMp();
        }
    }
}
