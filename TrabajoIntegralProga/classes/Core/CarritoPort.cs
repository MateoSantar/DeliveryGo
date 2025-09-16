using System;
using Interfaces; 

public class CarritoPort
{
    private readonly Carrito _carrito = new();
    private readonly EditorCarrito _editor = new();

    public decimal Subtotal() => _carrito.Subtotal();
    public void Run(ICommand cmd) => _editor.Run(cmd);
    public void Undo() => _editor.Undo();
    public void Redo() => _editor.Redo();
}

