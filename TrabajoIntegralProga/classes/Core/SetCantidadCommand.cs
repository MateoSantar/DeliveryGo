using System;
using Interfaces;

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
        if (_carrito.SetCantidad(_sku, _nueva))
            _anterior = _nueva; 
    }

    public void Undo() => _carrito.SetCantidad(_sku, _anterior);
}
