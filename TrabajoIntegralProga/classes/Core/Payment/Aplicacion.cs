using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enums;
namespace classes.Core.Payment
{
    class Aplicacion
    {
        private readonly IPago pago;

        public Aplicacion (IPagoFactory factory, PagoNombre tipo)
        {
            this.pago = factory.Create(tipo);
        }

        public void Run()
        {
            pago.Procesar(1000);
            Console.WriteLine("Pago realizado con exito");
        }
    }
}
