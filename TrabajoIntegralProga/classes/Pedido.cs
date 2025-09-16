using enums;

namespace classes
{
    public class Pedido
    {
        private int Id { get; set; }
        private List<Item> Items { get; set; }
        private String Direccion { get; set; }
        private double Monto { get; set; }
        private EstadoPedido Estado { get; set; }

        public Pedido() { }

        public Pedido(int id, List<Item> items, String direccion, double monto, EstadoPedido estado)
        {
            Id = id;
            Items = items;
            Direccion = direccion;
            Monto = monto;
            Estado = estado;
        }
    }
}
