using interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes.Core.Order
{
    class PedidoBuilder : IPedidoBuilder
    {
        private readonly Pedido _pedido = new();


        public IPedidoBuilder ConDireccion(string direccion)
        {
            _pedido.Direccion = direccion;
            return this;
        }

        public IPedidoBuilder ConItems(List<Item> items)
        {
            _pedido.Items = items;
            return this;
        }

        public IPedidoBuilder ConMetodoPago(string tipoPago)
        {
            _pedido.TipoPago = tipoPago;
            return this;
        }

        public IPedidoBuilder ConMonto(decimal monto)
        {
            _pedido.Monto = monto;
            return this;
        }
        public Pedido Build()
        {
            if(string.IsNullOrEmpty(_pedido.Items.ToString()))
            {
                throw new InvalidOperationException("El carrito no puede estar vacio.");
            }
            if (string.IsNullOrEmpty(_pedido.Direccion))
            {
                throw new InvalidOperationException("La direccion no puede estar vacia.");
            }
            return _pedido;
        }
    }
}
