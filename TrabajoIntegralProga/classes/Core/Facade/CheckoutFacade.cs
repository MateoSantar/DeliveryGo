using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classes.Core.Observer;
using interfaces;
using classes.Core.Order;
using classes.Core.Adapter;
using classes.Core.Payment;
using enums;

namespace classes.Core.Facade
{
    public class CheckoutFacade
    {
        private IEnvioStrategy _envioActual;
        private ICarritoPort _carrito;
        private PedidoService _pedidos;
        public CheckoutFacade(ICarritoPort carrito, IEnvioStrategy envioInicial, PedidoService pedidos)
        {
            this._envioActual = envioInicial;
            this._carrito = carrito;
            this._pedidos = pedidos;

        }
        public void AgregarItem(string sku, string nombre, decimal precio, int cantidad)
        {
            Item item = new Item(sku, nombre, precio, cantidad);
            CarritoPort carrito = (CarritoPort)_carrito;
            AgregarItemCommand cmd = new AgregarItemCommand(carrito.CarritoRef, item);
            carrito.Run(cmd);

        }
        public void CambiarCantidad(string sku, int cantidad)
        {
            CarritoPort carrito = (CarritoPort)_carrito;
            SetCantidadCommand cmd = new SetCantidadCommand(carrito.CarritoRef, sku, cantidad);
            carrito.Run(cmd);

        }
        public void QuitarItem(string sku)
        {
            CarritoPort carrito = (CarritoPort)_carrito;
            QuitarItemCommand cmd = new QuitarItemCommand(carrito.CarritoRef, sku);
            carrito.Run(cmd);
        }
        public void ElegirEnvio(IEnvioStrategy envio)
        {
            _envioActual = envio;
        }
        public decimal CalcularTotal()
        {
            return _carrito.Subtotal() + _envioActual.Calcular(_carrito.Subtotal());
        }
        public bool Pagar(string tipoPago, bool aplicarIVA, decimal? cupon = null)
        {
            IPago pago;
            if (tipoPago == "mp-adapter")
            {
                pago = new PagoAdapterMp(new MpSdkFalsa());
            }
            else
            {
                IPagoFactory factory = new PagoFactory();
                pago = factory.Create(Enum.Parse<enums.PagoNombre>(tipoPago));
                
            }
            if (aplicarIVA)
            {
                pago = new PagoConImpuesto(pago);
            }
            if (cupon.HasValue)
            {
                pago = new PagoConCupon(pago, cupon.Value);
            }
            return pago.Procesar(CalcularTotal());
        }
        public Pedido ConfirmarPedido(string direccion, string tipoPago)
        {
            PedidoBuilder builder = new PedidoBuilder();
            CarritoPort carrito = (CarritoPort)_carrito;
            builder.ConItems(carrito.CarritoRef.GetItems())
                     .ConDireccion(direccion)
                     .ConMetodoPago(tipoPago)
                     .ConMonto(CalcularTotal());
            Pedido pedido = builder.Build();
            pedido.Id = new Random().Next(1000, 9999);
            _pedidos.CambiarEstado(pedido.Id, EstadoPedido.Recibido);
            Thread.Sleep(1000);
            _pedidos.CambiarEstado(pedido.Id, EstadoPedido.Preparando);
            Thread.Sleep(1000);
            _pedidos.CambiarEstado(pedido.Id, EstadoPedido.Enviado);
            Thread.Sleep(1000);
            _pedidos.CambiarEstado(pedido.Id, EstadoPedido.Entregado);
            return pedido;

        }
    }
}
