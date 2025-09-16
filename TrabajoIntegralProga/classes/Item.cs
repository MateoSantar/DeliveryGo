namespace classes
{
    public class Item
    {
        private string Sku { get; set; }
        private string Name { get; set; }
        private double Price { get; set; }
        private int Cantidad { get; set; }
        public Item(string sku, string name, double price, int cantidad)
        {
            Sku = sku;
            Name = name;
            Price = price;
            Cantidad = cantidad;
        }
    }
}