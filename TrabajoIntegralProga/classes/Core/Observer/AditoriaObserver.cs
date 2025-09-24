using classes.Core.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes.Core.Observer
{
    class AditoriaObserver
    {
        public void Suscribir(PedidoService p) => p.EstadoCambiado += OnEstadoCambiado;
        public void Desuscribir(PedidoService p) => p.EstadoCambiado -= OnEstadoCambiado;
        private void OnEstadoCambiado(object? sender, PedidoChangedEventArgs p)
        {
            Console.WriteLine($"[AUDITORIA] Pedido {p.PedidoId} cambiado a {p.NuevoEstado} en {DateTime.Now}");
        }
    }
}
