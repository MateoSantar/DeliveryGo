using interfaces;
namespace classes.Core.Strategy
{
    public class EnvioMoto : IEnvioStrategy
    {
        public string Nombre => "Moto";

        public decimal Calcular(decimal subTotal) => 1200m;
    }
}