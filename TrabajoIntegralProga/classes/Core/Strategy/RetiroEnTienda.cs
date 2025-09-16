using interfaces;

namespace classes.Core.Strategy
{
    public class RetiroEnTienda : IEnvioStrategy
    {
        public string Nombre => "Retiro";

        public decimal Calcular(decimal subTotal) => 0;
    }
}