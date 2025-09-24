using classes.Core.Payment;
using enums;
using interfaces;

namespace TrabajoIntegralProga;

class Program
{
    static void Main(string[] args)
    {
        PagoNombre pagoNombre = new PagoNombre();
        IPagoFactory factory = new PagoFactory();
        var app = new Aplicacion(factory, pagoNombre);
        app.Run();
        PagoMp pago = new PagoMp();

    }
}
