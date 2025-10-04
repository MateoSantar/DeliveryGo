using System;
using interfaces;
using classes;

public class CarritoPort : ICarritoPort
{
    public Carrito CarritoRef { get; } = new();
    private readonly EditorCarrito _editor = new();

    public decimal Subtotal() => CarritoRef.Subtotal();
    public void Run(ICommand cmd) => _editor.Run(cmd);
    public void Undo() => _editor.Undo();
    public void Redo() => _editor.Redo();
}