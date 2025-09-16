namespace interfaces
{
    public interface IEnvioStrategy
    {
        decimal Calcular(decimal subTotal);
        string Nombre { get; }
    }
}