
    using classes;
    using classes.Core.Strategy;
    using classes.Core.Payment;
    using classes.Core.Observer;
    using classes.Core.Order;
    using interfaces;
    using enums;
using System;

namespace DeliveryGo
{

    public static class Program
    {
        private static CarritoPort? port;
        private static EnvioService? envioService;
        private static IPagoFactory? pagoFactory;
        private static PedidoService? pedidoService;
        private static LogisticaObserver? logisticaObserver;
        private static bool logisticaSuscrita = false;

        public static void Main()
        {
            // Inicializar servicios
            port = new CarritoPort();
            envioService = new EnvioService(new EnvioCorreo());
            pagoFactory = new PagoFactory();
            pedidoService = new PedidoService();
            logisticaObserver = new LogisticaObserver();

            // Configuración inicial
            ConfigManager.Instance.EnvioGratisDesde = 50000m;

            bool continuar = true;

            while (continuar)
            {
                MostrarMenu();

                try
                {
                    int opcion = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine();

                    switch (opcion)
                    {
                        case 1:
                            AgregarItem();
                            break;
                        case 2:
                            CambiarCantidad();
                            break;
                        case 3:
                            QuitarItem();
                            break;
                        case 4:
                            VerCarrito();
                            break;
                        case 5:
                            Deshacer();
                            break;
                        case 6:
                            Rehacer();
                            break;
                        case 7:
                            ElegirMetodoEnvio();
                            break;
                        case 8:
                            ProcesarPago();
                            break;
                        case 9:
                            ConfirmarPedido();
                            break;
                        case 10:
                            GestionarSuscripciones();
                            break;
                        case 0:
                            continuar = false;
                            Console.WriteLine("¡Gracias por usar DeliveryGo!");
                            break;
                        default:
                            Console.WriteLine(" Opción inválida. Intente nuevamente.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(" Error: Debe ingresar un número válido.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Error: {ex.Message}");
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }

        private static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("|        DELIVERYGO - CHECKOUT           |");
            Console.WriteLine("|       Sistema de Gestión de Pedidos    |");
            Console.WriteLine("==========================================");
            Console.WriteLine();
            Console.WriteLine("==========================================");
            Console.WriteLine("|            MENÚ PRINCIPAL              |");
            Console.WriteLine("==========================================");
            Console.WriteLine("|  GESTIÓN DEL CARRITO (Command)         |");
            Console.WriteLine("|  1. Agregar ítem                       |");
            Console.WriteLine("|  2. Cambiar cantidad                   |");
            Console.WriteLine("|  3. Quitar ítem                        |");
            Console.WriteLine("|  4. Ver carrito y totales              |");
            Console.WriteLine("|  5. Deshacer (Undo)                    |");
            Console.WriteLine("|  6. Rehacer (Redo)                     |");
            Console.WriteLine("==========================================");
            Console.WriteLine("|  ENVÍO Y PAGO                          |");
            Console.WriteLine("|  7. Elegir método de envío             |");
            Console.WriteLine("|  8. Procesar pago                      |");
            Console.WriteLine("|  9. Confirmar pedido                   |");
            Console.WriteLine("==========================================");
            Console.WriteLine("|  CONFIGURACIÓN                         |");
            Console.WriteLine($"|  10. Logística: {(logisticaSuscrita ? "[SUSCRITA]" : "[NO SUSCRITA]"),-19}|");
            Console.WriteLine("| 0. Salir                              |");
            Console.WriteLine("===========================================");
            Console.Write("\nSeleccione una opción: ");
        }

        private static void AgregarItem()
        {
            Console.WriteLine("─── AGREGAR ÍTEM ───");

            Console.Write("SKU del producto: ");
            string sku = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(sku))
            {
                Console.WriteLine(" El SKU no puede estar vacío.");
                return;
            }

            Console.Write("Nombre del producto: ");
            string nombre = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine(" El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Precio unitario: $");
            if (!decimal.TryParse(Console.ReadLine(), out decimal precio) || precio <= 0)
            {
                Console.WriteLine(" Precio inválido.");
                return;
            }

            Console.Write("Cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine(" Cantidad inválida.");
                return;
            }

            var item = new Item(sku, nombre, precio, cantidad);
            port!.Run(new AgregarItemCommand(port.CarritoRef, item));

            Console.WriteLine($"✓ Producto '{nombre}' agregado correctamente.");
            Console.WriteLine($"  Subtotal actual: ${port.Subtotal():N2}");
        }

        private static void CambiarCantidad()
        {
            Console.WriteLine("─── CAMBIAR CANTIDAD ───");

            Console.Write("SKU del producto: ");
            string sku = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(sku))
            {
                Console.WriteLine(" El SKU no puede estar vacío.");
                return;
            }

            Console.Write("Nueva cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine("❌ Cantidad inválida.");
                return;
            }

            port!.Run(new SetCantidadCommand(port.CarritoRef, sku, cantidad));

            Console.WriteLine($"✓ Cantidad actualizada correctamente.");
            Console.WriteLine($"  Subtotal actual: ${port.Subtotal():N2}");
        }

        private static void QuitarItem()
        {
            Console.WriteLine("─── QUITAR ÍTEM ───");

            Console.Write("SKU del producto a quitar: ");
            string sku = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(sku))
            {
                Console.WriteLine(" El SKU no puede estar vacío.");
                return;
            }

            port!.Run(new QuitarItemCommand(port.CarritoRef, sku));

            Console.WriteLine($"✓ Producto quitado del carrito.");
            Console.WriteLine($"  Subtotal actual: ${port.Subtotal():N2}");
        }

        private static void VerCarrito()
        {
            Console.WriteLine("─── CARRITO DE COMPRAS ───");
            Console.WriteLine();

            var items = port!.CarritoRef.GetItems();

            if (items.Count == 0)
            {
                Console.WriteLine(" El carrito está vacío.");
            }
            else
            {
                Console.WriteLine($"{"SKU",-10} {"Producto",-25} {"Precio",-12} {"Cant.",-6} {"Total",-12}");
                Console.WriteLine(new string('─', 67));

                foreach (var item in items)
                {
                    decimal totalItem = item.Precio * item.Cantidad;
                    Console.WriteLine($"{item.Sku,-10} {item.Nombre,-25} ${item.Precio,-11:N2} {item.Cantidad,-6} ${totalItem,-11:N2}");
                }

                Console.WriteLine(new string('─', 67));
            }

            decimal subtotal = port.Subtotal();
            decimal costoEnvio = envioService!.Calcular(subtotal);
            decimal total = subtotal + costoEnvio;

            Console.WriteLine();
            Console.WriteLine($"Subtotal:        ${subtotal,12:N2}");
            Console.WriteLine($"Envío ({envioService.NombreActual()}): ${costoEnvio,12:N2}");

            if (costoEnvio == 0 && subtotal >= ConfigManager.Instance.EnvioGratisDesde)
            {
                Console.WriteLine("                  ¡ENVÍO GRATIS!");
            }

            Console.WriteLine($"{"TOTAL:",17} ${total,12:N2}");
        }

        private static void Deshacer()
        {
            try
            {
                port!.Undo();
                Console.WriteLine(" Operación deshecha.");
                Console.WriteLine($"  Subtotal actual: ${port.Subtotal():N2}");
            }
            catch
            {
                Console.WriteLine(" No hay operaciones para deshacer.");
            }
        }

        private static void Rehacer()
        {
            try
            {
                port!.Redo();
                Console.WriteLine(" Operación rehecha.");
                Console.WriteLine($"  Subtotal actual: ${port.Subtotal():N2}");
            }
            catch
            {
                Console.WriteLine(" No hay operaciones para rehacer.");
            }
        }

        private static void ElegirMetodoEnvio()
        {
            Console.WriteLine("─── MÉTODO DE ENVÍO ───");
            Console.WriteLine("1. Envío por Correo (Gratis desde $50.000)");
            Console.WriteLine("2. Envío en Moto ($1.200)");
            Console.WriteLine("3. Retiro en Tienda (Gratis)");
            Console.Write("\nSeleccione método: ");

            if (!int.TryParse(Console.ReadLine(), out int metodo) || metodo < 1 || metodo > 3)
            {
                Console.WriteLine(" Opción inválida.");
                return;
            }

            IEnvioStrategy estrategia = metodo switch
            {
                1 => new EnvioCorreo(),
                2 => new EnvioMoto(),
                3 => new RetiroEnTienda(),
                _ => new EnvioCorreo()
            };

            envioService!.SetStrategy(estrategia);

            decimal subtotal = port!.Subtotal();
            decimal costoEnvio = envioService.Calcular(subtotal);

            Console.WriteLine($"\n✓ Método seleccionado: {envioService.NombreActual()}");
            Console.WriteLine($"  Costo de envío: ${costoEnvio:N2}");

            if (costoEnvio == 0 && subtotal >= ConfigManager.Instance.EnvioGratisDesde)
            {
                Console.WriteLine($"   ¡Envío gratis por superar ${ConfigManager.Instance.EnvioGratisDesde:N2}!");
            }
        }

        private static void ProcesarPago()
        {
            Console.WriteLine("─── PROCESAR PAGO ───");

            if (port!.CarritoRef.GetItems().Count == 0)
            {
                Console.WriteLine(" El carrito está vacío. Agregue productos antes de procesar el pago.");
                return;
            }

            Console.WriteLine("1. Tarjeta");
            Console.WriteLine("2. Mercado Pago");
            Console.WriteLine("3. Transferencia");
            Console.Write("\nSeleccione método de pago: ");

            if (!int.TryParse(Console.ReadLine(), out int metodoPago) || metodoPago < 1 || metodoPago > 3)
            {
                Console.WriteLine(" Opción inválida.");
                return;
            }

            PagoNombre tipoPago = metodoPago switch
            {
                1 => PagoNombre.tarjeta,
                2 => PagoNombre.mp,
                3 => PagoNombre.transf,
                _ => PagoNombre.tarjeta
            };

            Console.Write("\n¿Aplicar IVA (21%)? (S/N): ");
            bool aplicarIVA = Console.ReadLine()?.ToUpper() == "S";

            Console.Write("¿Tiene cupón de descuento? (S/N): ");
            bool tieneCupon = Console.ReadLine()?.ToUpper() == "S";

            decimal porcentajeDescuento = 0;
            if (tieneCupon)
            {
                Console.Write("Porcentaje de descuento (ej: 10 para 10%): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal desc) && desc > 0 && desc <= 100)
                {
                    porcentajeDescuento = desc / 100m;
                }
            }

            decimal subtotal = port.Subtotal();
            decimal costoEnvio = envioService!.Calcular(subtotal);
            decimal total = subtotal + costoEnvio;

            // Crear método de pago usando el Factory
            IPago? pago = pagoFactory!.Create(tipoPago);

            if (pago == null)
            {
                Console.WriteLine(" Error al crear el método de pago.");
                return;
            }

            // Aplicar decoradores
            if (tieneCupon && porcentajeDescuento > 0)
            {
                pago = new PagoConCupon(pago, porcentajeDescuento);
                Console.WriteLine($" Cupón de {porcentajeDescuento * 100}% aplicado.");
            }

            if (aplicarIVA)
            {
                pago = new PagoConImpuesto(pago);
                Console.WriteLine($" IVA del {ConfigManager.Instance.IVA * 100}% aplicado.");
            }

            Console.WriteLine($"\nMonto original: ${total:N2}");
            Console.Write("Confirmar pago (S/N): ");
            string confirmacion = Console.ReadLine()?.ToUpper() ?? "N";

            if (confirmacion == "S")
            {
                bool exito = pago.Procesar(total);

                if (exito)
                {
                    Console.WriteLine("\n Pago procesado exitosamente.");
                }
                else
                {
                    Console.WriteLine("\n Error al procesar el pago.");
                }
            }
            else
            {
                Console.WriteLine("\n Pago cancelado.");
            }
        }

        private static void ConfirmarPedido()
        {
            Console.WriteLine("─── CONFIRMAR PEDIDO ───");

            if (port!.CarritoRef.GetItems().Count == 0)
            {
                Console.WriteLine(" El carrito está vacío. Agregue productos antes de confirmar.");
                return;
            }

            decimal subtotal = port.Subtotal();
            decimal costoEnvio = envioService!.Calcular(subtotal);
            decimal total = subtotal + costoEnvio;

            Console.WriteLine("\n=== RESUMEN DEL PEDIDO ===");
            Console.WriteLine($"Productos:       ${subtotal:N2}");
            Console.WriteLine($"Envío:           ${costoEnvio:N2}");
            Console.WriteLine($"TOTAL A PAGAR:   ${total:N2}");
            Console.WriteLine();

            Console.Write("Dirección de envío: ");
            string direccion = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(direccion))
            {
                Console.WriteLine(" La dirección no puede estar vacía.");
                return;
            }

            Console.Write("\n¿Confirmar pedido? (S/N): ");
            string confirmacion = Console.ReadLine()?.ToUpper() ?? "N";

            if (confirmacion == "S")
            {
                // Crear pedido usando Builder
                var builder = new PedidoBuilder();
                var items = port.CarritoRef.GetItems().ToList();

                var pedido = builder
                    .ConItems(items)
                    .ConDireccion(direccion)
                    .ConMetodoPago(envioService.NombreActual())
                    .ConMonto(total)
                    .Build();

                pedido.Id = new Random().Next(1000, 9999);
                pedido.Estado = EstadoPedido.Recibido;

                Console.WriteLine($"\n✓ ¡Pedido #{pedido.Id} confirmado exitosamente!");
                Console.WriteLine($"  Estado: {pedido.Estado}");
                Console.WriteLine($"  Total: ${total:N2}");
                Console.WriteLine($"  Dirección: {direccion}");

                // Notificar cambio de estado usando Observer
                pedidoService!.CambiarEstado(pedido.Id, EstadoPedido.Recibido);

                // Simular progreso del pedido
                Console.WriteLine("\nSimulando progreso del pedido...");
                System.Threading.Thread.Sleep(1000);
                pedidoService.CambiarEstado(pedido.Id, EstadoPedido.Preparando);

                System.Threading.Thread.Sleep(1000);
                pedidoService.CambiarEstado(pedido.Id, EstadoPedido.Enviado);

                System.Threading.Thread.Sleep(1000);
                pedidoService.CambiarEstado(pedido.Id, EstadoPedido.Entregado);

                // Limpiar carrito después de confirmar
                var itemsToRemove = port.CarritoRef.GetItems().ToList();
                foreach (var item in itemsToRemove)
                {
                    port.Run(new QuitarItemCommand(port.CarritoRef, item.Sku));
                }
            }
            else
            {
                Console.WriteLine("\n Pedido cancelado.");
            }
        }

        private static void GestionarSuscripciones()
        {
            Console.WriteLine("─── GESTIÓN DE SUSCRIPCIONES (Observer) ───");
            Console.WriteLine($"\nEstado actual: Logística {(logisticaSuscrita ? "SUSCRITA" : "NO SUSCRITA")}");
            Console.WriteLine("\n1. Suscribir observador de logística");
            Console.WriteLine("2. Desuscribir observador de logística");
            Console.Write("\nSeleccione opción: ");

            if (!int.TryParse(Console.ReadLine(), out int opcion) || (opcion != 1 && opcion != 2))
            {
                Console.WriteLine(" Opción inválida.");
                return;
            }

            if (opcion == 1)
            {
                if (logisticaSuscrita)
                {
                    Console.WriteLine(" El observador de logística ya está suscrito.");
                }
                else
                {
                    logisticaObserver!.Suscribir(pedidoService!);
                    logisticaSuscrita = true;
                    Console.WriteLine(" Observador de logística suscrito exitosamente.");
                    Console.WriteLine("  Ahora recibirá notificaciones de cambios de estado de pedidos.");
                }
            }
            else if (opcion == 2)
            {
                if (!logisticaSuscrita)
                {
                    Console.WriteLine(" El observador de logística no está suscrito.");
                }
                else
                {
                    logisticaObserver!.Desuscribir(pedidoService!);
                    logisticaSuscrita = false;
                    Console.WriteLine(" Observador de logística desuscrito exitosamente.");
                    Console.WriteLine("  Ya no recibirá notificaciones de cambios de estado.");
                }
            }
        }
    }
}
