namespace MinoriEditorShell.Services
{
    public interface IUndoableAction
    {
        string Name { get; }

        void Execute();
        void Undo();
    }
}