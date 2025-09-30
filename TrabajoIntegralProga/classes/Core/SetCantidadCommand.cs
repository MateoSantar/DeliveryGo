using System;
using interfaces;

public class SetCantidadCommand : ICommand
{
    private readonly Carrito _carrito;
    private readonly string _sku;
    private readonly int _nueva;
    private int _anterior;

    public SetCantidadCommand(Carrito carrito, string sku, int nueva)
    {
        _carrito = carrito;
        _sku = sku;
        _nueva = nueva;
    }

    public void Execute()
    {
        if (_carrito.TryGetCantidad(_sku, out var prev))
        {
            _anterior = prev;
            _carrito.SetCantidad(_sku, _nueva);
        }
    }

    public void Undo()
    {
        if (_anterior > 0)
            _carrito.SetCantidad(_sku, _anterior);
    }
}