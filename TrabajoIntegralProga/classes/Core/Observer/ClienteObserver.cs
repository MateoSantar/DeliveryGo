using classes.Core.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes.Core.Observer
{
    class ClienteObserver
    {
        public void Suscribir(PedidoService p) => p.EstadoCambiado += OnEstadoCambiado;
        public void Desuscribir(PedidoService p) => p.EstadoCambiado -= OnEstadoCambiado;
        private void OnEstadoCambiado(object? sender, PedidoChangedEventArgs p)
        {
            Console.WriteLine($"Estimado cliente, su pedido {p.PedidoId} ha sido procesado con éxito.");
        }
    }
}
