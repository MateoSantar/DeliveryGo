namespace interfaces
{
    public interface ICarritoPort 
{ 
    decimal Subtotal();                      // suma de (precio * cantidad) 
    void Run(ICommand cmd);                  // ejecuta comando y guarda en historial 
    void Undo(); 
    void Redo(); 
}
}