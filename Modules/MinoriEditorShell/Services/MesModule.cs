using System.ComponentModel.Composition;

namespace MinoriEditorShell.Services
{
#warning ModuleBase
    [Export(typeof(IMesModule))]
    public class MesModule /*: ModuleBase*/
    {
#if false
        public override IEnumerable<ResourceDictionary> GlobalResourceDictionaries
        {
            get
            {
                yield return new ResourceDictionary
                {
                    Source = new Uri("/MinoriEditorStudio;component/Modules/ToolBars/Resources/Styles.xaml", UriKind.Relative)
                };
            }
        }
#endif
    }
}
