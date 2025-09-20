using classes;
using classes.Core.Order;

namespace interfaces
{
    public interface IPedidoBuilder
    {
        IPedidoBuilder ConItems(List<Item> items);
        IPedidoBuilder ConDireccion(string direccion);
        IPedidoBuilder ConMetodoPago(string tipoPago);
        Pedido Build();
    }
}