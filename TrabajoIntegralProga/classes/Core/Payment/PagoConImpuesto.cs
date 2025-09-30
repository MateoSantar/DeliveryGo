using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enums;
using interfaces;

namespace classes.Core.Payment
{
    class PagoConImpuesto : IPago
    {
        private IPago inner;

        public PagoConImpuesto(IPago inner)
        {
            this.inner = inner;
        }

        public PagoNombre Nombre => inner.Nombre;

        public bool Procesar(decimal monto)
        {
            var total = monto * (1 + ConfigManager.Instance.IVA);
            return inner.Procesar(total);
        }
    }
}
