# DeliveryGo
Mini-ecommerce en consola desarrollado en C# como trabajo integral de Patrones de Diseño para la materia Programación II.

## Desarrolladores 

* [Santarsiero Mateo](https://github.com/MateoSantar)
* [Mendoza Joaquín Pedro](https://github.com/PedroMendoza19)
* [Tarricone Thiago](https://github.com/Thiago20035)

## Uso

1. Clonar el repositorio.
2. Abrir el proyecto con un IDE compatible con .NET SDK 9.0.
3. Compilar el proyecto.
4. Ejecutar: dotnet run
5. Navegar entre las opciones del menú (agregar ítems, elegir envío, pagar, confirmar pedido).

## Patrones aplicados
* **Command** para el manejo del Carrito
* **Strategy** & **Singleton** para los envios
* **Factory** & **Adapter** para las SDK de los pagos
* **Decorator** para los calculos de pagos
* **Builder** para la construccion de pedidos
* **Observer** para el manejo de eventos de pedidos
* **Facade** para la orquestacion de los pedidos

## Caso de uso

**Escenario:**  
Un cliente desea comprar productos y elegir un método de envío y pago dentro de *DeliveryGo*.

1. **Inicio de la aplicación**  
   El usuario ejecuta el programa y accede al menú principal.

2. **Agregar productos al carrito**  
   - El usuario selecciona la opción **Agregar ítems**.  
   - Cada producto agregado se gestiona mediante el patrón **Command**, lo que permite deshacer acciones si se comete un error.  
   - El **Carrito** mantiene el estado de los productos seleccionados.

3. **Elegir método de envío**  
   - El usuario selecciona entre diferentes estrategias de envío (por ejemplo: *Envío Express* o *Envío Estándar*).  
   - Se aplica el patrón **Strategy** para calcular costos y tiempos de entrega según el tipo de envío.  
   - El sistema utiliza un **Singleton** para centralizar la configuración y mantener una única instancia del gestor de envíos.

4. **Seleccionar medio de pago**  
   - El usuario elige pagar con tarjeta, PayPal u otro método disponible.  
   - El patrón **Factory** crea la instancia adecuada del procesador de pagos según la opción elegida.  
   - Si el método de pago necesita integrarse con una SDK externa, se utiliza **Adapter**.  
   - Antes de procesar el cobro, se aplican **Decorators** para añadir costos adicionales (impuestos, comisiones, descuentos, etc.).

5. **Construcción y confirmación del pedido**  
   - El pedido final se construye usando el patrón **Builder**, ensamblando carrito, envío y pago en un objeto listo para procesar.  
   - Al confirmar el pedido, se disparan eventos mediante el patrón **Observer** para notificar cambios de estado (ej.: *Pedido confirmado*, *Pedido en camino*).  
   - El patrón **Facade** coordina todas las operaciones internas para simplificar la interacción desde el menú principal.

6. **Resultado**  
   El usuario recibe un resumen completo de su compra, incluyendo detalle de productos, costos de envío, método de pago seleccionado y estado actual del pedido.

