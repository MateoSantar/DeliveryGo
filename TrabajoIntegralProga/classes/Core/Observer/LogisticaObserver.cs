using classes.Core.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes.Core.Observer
{
    class LogisticaObserver
    {
        public void Suscribir(PedidoService p) => p.EstadoCambiado += OnEstadoCambiado;
        public void Desuscribir(PedidoService p) => p.EstadoCambiado -= OnEstadoCambiado;
        private void OnEstadoCambiado(object? sender, PedidoChangedEventArgs p)
        {
            if (p.NuevoEstado == enums.EstadoPedido.Enviado)
            {
                Console.WriteLine($"[LOGISTICA] Pedido {p.PedidoId} ha sido enviado. Preparando para la entrega.");
            }
            else
            {
                Console.WriteLine($"[LOGISTICA] Pedido {p.PedidoId} => Estado: {p.NuevoEstado}");
            }
        }
    }
}
