using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classes;
using interfaces;

namespace TrabajoIntegralProga.classes
{
    public class CheckoutFacade
    {
        public CheckoutFacade(ICarritoPort carrito, IEnvioStrategy envio, PedidoService pedidos)
        {

        }

        public void AgregarItem(string sku, string nombre, decimal precio, int cantidad)
        {
        }
        public void CambiarCantidad(string sku, int cantidad)
        {
        }
        public void QuitarItem(string sku)
        {
        }
        public void ElegirEnvio(IEnvioStrategy envio)
        {
        }
        public decimal CalcularTotal()
        {
            return 0;
        }
        public bool Pagar(string tipoPago, bool aplicarIVA, decimal ? cupon = null)
        {
            return false;
        }
        public Pedido ConfirmarPedido(string direccion, string tipoPago)
        {
            return new Pedido();
        }
    }
}
