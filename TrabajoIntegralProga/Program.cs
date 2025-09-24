
namespace TrabajoIntegralProga;
using classes;
using global::classes.Core.Strategy;
using interfaces;
using classes.Core.Payment;
using enums;
using interfaces;

namespace TrabajoIntegralProga;

class Program
{
    static void Main(string[] args)
    {

        ConfigManager.Instance.EnvioGratisDesde = 50000m;
        EnvioService envioService = new EnvioService(new EnvioCorreo());
        Console.WriteLine($"{envioService.NombreActual()}: {envioService.Calcular(60000m)}");
        envioService.SetStrategy(new EnvioMoto());
        Console.WriteLine($"{envioService.NombreActual()}: {envioService.Calcular(60000m)}");
        envioService.SetStrategy(new RetiroEnTienda());
        Console.WriteLine($"{envioService.NombreActual()}: {envioService.Calcular(60000m)}");
        PagoNombre pagoNombre = new PagoNombre();
        IPagoFactory factory = new PagoFactory();
        var app = new Aplicacion(factory, pagoNombre);
        app.Run();
        PagoMp pago = new PagoMp();

    }
}
