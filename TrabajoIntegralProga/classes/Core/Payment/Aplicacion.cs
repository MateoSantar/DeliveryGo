using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes.Core.Payment
{
    class Aplicacion
    {
        private readonly IPago pago;

        public Aplicacion (IPagoFactory factory)
        {
            pago = factory.Create(pago);
        }

        public void Run()
        {
            pago.Procesar(1000);
            Console.WriteLine("Pago realizado con exito");
        }
    }
}
