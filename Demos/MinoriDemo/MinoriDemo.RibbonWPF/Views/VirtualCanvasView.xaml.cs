﻿
using MinoriEditorShell.VirtualCanvas.Services;

namespace MinoriDemo.RibbonWPF.Views
{
    /// <summary>
    /// Interaction logic for VirtualCanvasView.xaml
    /// </summary>
    public partial class VirtualCanvasView
    {
        public VirtualCanvasView()
        {
            InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                IMesVirtualCanvas dc = (IMesVirtualCanvas)DataContext;
                Graph.UseDefaultControls(dc);
            };
        }
    }
}
