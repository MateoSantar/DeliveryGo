using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enums;

namespace classes.Core.Observer
{
    public class PedidoService
    {
        public event EventHandler<PedidoChangedEventArgs> EstadoCambiado;
        public void CambiarEstado(int pedidoId, EstadoPedido nuevoEstado) => EstadoCambiado?.Invoke(this, new PedidoChangedEventArgs(pedidoId, nuevoEstado, DateTime.Now));

    }
}
