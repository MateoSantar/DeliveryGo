using System;
using classes;

namespace DeliveryGo
{
    public static class Program
    {
        public static void Main()
        {
            CarritoPort port = new CarritoPort();
            var item1 = new Item("A001", "Mouse", 1000, 1);

            port.Run(new AgregarItemCommand(port.CarritoRef, item1));
            Console.WriteLine($"Subtotal: {port.Subtotal()}"); // 1000

            port.Run(new SetCantidadCommand(port.CarritoRef, "A001", 3));
            Console.WriteLine($"Subtotal: {port.Subtotal()}"); // 3000

            port.Undo();
            Console.WriteLine($"Subtotal tras Undo: {port.Subtotal()}"); // 1000

            port.Redo();
            Console.WriteLine($"Subtotal tras Redo: {port.Subtotal()}"); // 3000

            port.Run(new QuitarItemCommand(port.CarritoRef, "A001"));
            Console.WriteLine($"Subtotal tras quitar: {port.Subtotal()}"); // 0

            port.Undo();
            Console.WriteLine($"Subtotal tras Undo quitar: {port.Subtotal()}"); // 3000
        }
    }
}