using classes;
using enums;

namespace classes.Core.Order
{
    public class Pedido
    {
        private int Id { get; set; }
        private List<Item> Items { get; set; }
        private string Direccion { get; set; }
        private double Monto { get; set; }
        private EstadoPedido Estado { get; set; }

        public Pedido() { }

        public Pedido(int id, List<Item> items, string direccion, double monto, EstadoPedido estado)
        {
            Id = id;
            Items = items;
            Direccion = direccion;
            Monto = monto;
            Estado = estado;
        }
    }
}
