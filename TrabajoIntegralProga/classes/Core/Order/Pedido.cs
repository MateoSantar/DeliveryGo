using classes;
using enums;

namespace classes.Core.Order
{
    public class Pedido
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; }
        public string Direccion { get; set; }
        public string TipoPago { get; set; }
        public decimal Monto { get; set; }
        public EstadoPedido Estado { get; set; }

        public Pedido() { }

        public Pedido(int id, List<Item> items, string direccion, decimal monto, EstadoPedido estado, string tipoPago)
        {
            Id = id;
            Items = items;
            Direccion = direccion;
            Monto = monto;
            Estado = estado;
            TipoPago = tipoPago;
        }

        public override string ToString()
        {
            return $"Pedido ID: {Id}, Items: {string.Join(", ", Items)}, Dirección: {Direccion}, Monto: {Monto}, Estado: {Estado}, Tipo de Pago: {TipoPago}";
        }
    }
}
