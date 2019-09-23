using System.ComponentModel.Composition;
using System.Threading.Tasks;
using MinoriEditorStudio.Services;
using MinoriEditorStudio.Threading;

namespace MinoriEditorStudio.Commands
{
    [CommandHandler]
    public class CloseFileCommandHandler : CommandHandlerBase<CloseFileCommandDefinition>
    {
        private readonly IManager _shell;

        [ImportingConstructor]
        public CloseFileCommandHandler(IManager shell) => _shell = shell;

        public override void Update(Command command)
        {
            command.Enabled = _shell.ActiveItem != null;
            base.Update(command);
        }

        public override Task Run(Command command)
        {
            _shell.CloseDocument(_shell.SelectedDocument);
            return TaskUtility.Completed;
        }
    }
}