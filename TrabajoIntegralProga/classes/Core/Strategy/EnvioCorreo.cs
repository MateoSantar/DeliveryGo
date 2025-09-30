using interfaces;
using classes;
namespace classes.Core.Strategy {
    public class EnvioCorreo : IEnvioStrategy {
        public string Nombre => "Correo";

        public decimal Calcular(decimal subTotal)
        {
            return (subTotal >= ConfigManager.Instance.EnvioGratisDesde) ? 0 : 3500m;
        }
    }   
}