namespace interfaces
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}