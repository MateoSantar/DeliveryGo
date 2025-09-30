using interfaces;

namespace classes.Core.Strategy
{
    public class EnvioService
    {
        private IEnvioStrategy _actual;

        public EnvioService(IEnvioStrategy strategy)
        {
            _actual = strategy;
        }

        public void SetStrategy(IEnvioStrategy strategy)
        {
            _actual = strategy;
        }

        public decimal Calcular(decimal subTotal)
        {
            return _actual.Calcular(subTotal);
        }

        public string NombreActual()
        {
            return _actual.Nombre;
        }
    }
}