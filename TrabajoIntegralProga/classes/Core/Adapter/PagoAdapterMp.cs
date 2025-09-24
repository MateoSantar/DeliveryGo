using enums;
using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classes.Core.Payment;

namespace classes.Core.Adapter
{
    class PagoAdapterMp : IPago
    {
        private readonly MpSdkFalsa _legacy;

        public PagoAdapterMp()   
        {
            _legacy = new MpSdkFalsa();
        }

        public PagoNombre Nombre => PagoNombre.mp;

        public bool Procesar(decimal monto)
        {
            _legacy.SimularPagoMp().Procesar(monto);
            _legacy.Cobrar(monto);
            return true;
        }
    }
}
