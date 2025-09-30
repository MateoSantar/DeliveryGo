using System;
using System.Collections.Generic;
using classes;

public class Carrito
{
    private readonly Dictionary<string, Item> _items = new();

    public void Agregar(Item i)
    {
        if (_items.ContainsKey(i.Sku))
            _items[i.Sku].Cantidad += i.Cantidad;
        else
            _items[i.Sku] = i;
    }

    public Item? Quitar(string sku)
    {
        if (_items.TryGetValue(sku, out var item))
        {
            _items.Remove(sku);
            return item;
        }
        return null;
    }

    public bool SetCantidad(string sku, int nueva)
    {
        if (_items.ContainsKey(sku) && nueva > 0)
        {
            _items[sku].Cantidad = nueva;
            return true;
        }
        return false;
    }

    public bool TryGetCantidad(string sku, out int cantidad)
    {
        if (_items.TryGetValue(sku, out var item))
        {
            cantidad = item.Cantidad;
            return true;
        }
        cantidad = 0;
        return false;
    }

    public decimal Subtotal()
    {
        decimal total = 0;
        foreach (var item in _items.Values)
            total += (decimal)item.Precio * item.Cantidad;
        return total;
    }
}