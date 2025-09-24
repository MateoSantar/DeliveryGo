using enums;
using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes.Core.Payment
{
    class PagoConCupon : IPago
    {
        private IPago _inner;
        private decimal _porcentaje = 0.10m; 

        public PagoConCupon(IPago inner, decimal porcentaje)
        {
            this._inner = inner;
            this._porcentaje = porcentaje;
        }

        public PagoNombre Nombre => _inner.Nombre;

        public bool Procesar(decimal monto)
        {
            var total = monto * (1 - _porcentaje);
            return _inner.Procesar(total);
        }
    }
}
