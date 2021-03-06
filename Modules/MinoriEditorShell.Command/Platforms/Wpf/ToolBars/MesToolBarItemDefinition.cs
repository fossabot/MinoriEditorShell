using MinoriEditorShell.Commands;
using System;
using System.Windows.Input;

namespace MinoriEditorShell.Platforms.Wpf.ToolBars
{
    public abstract class MesToolBarItemDefinition
    {
        public MesToolBarItemGroupDefinition Group { get; }

        public Int32 SortOrder { get; }

        public ToolBarItemDisplay Display { get; }

        public abstract string Text { get; }
        public abstract Uri IconSource { get; }
        public abstract KeyGesture KeyGesture { get; }
        public abstract MesCommandDefinitionBase CommandDefinition { get; }

        protected MesToolBarItemDefinition(MesToolBarItemGroupDefinition group, int sortOrder, ToolBarItemDisplay display)
        {
            Group = group;
            SortOrder = sortOrder;
            Display = display;
        }
    }

    public enum ToolBarItemDisplay
    {
        IconOnly,
        IconAndText
    }
}
