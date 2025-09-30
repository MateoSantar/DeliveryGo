namespace classes
{
    public class Item
    {
        public string Sku { get; init; } = "";
        public string Nombre { get; init; } = "";
        public decimal Precio { get; init; }
        public int Cantidad { get; set; }

        public Item(string sku, string nombre, decimal precio, int cantidad)
        {
            Sku = sku;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
        }
    }
}
