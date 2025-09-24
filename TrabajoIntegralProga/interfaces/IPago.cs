using enums;

namespace interfaces
{
    public interface IPago
    {
        PagoNombre Nombre { get; }
        bool Procesar(decimal monto);
    }
}