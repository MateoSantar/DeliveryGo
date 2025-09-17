using classes.Core.Payment;
using interfaces;

namespace TrabajoIntegralProga;

class Program
{
    static void Main(string[] args)
    {
        IPagoFactory factory = new PagoFactory();
        var app = new Aplicacion(factory);
        app.Run();

    }
}
