using MinoriDemo.Core.ViewModels;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoriDemo.Core
{
    public class App : MvxApplication
    {
        public override void Initialize() => RegisterAppStart<MainViewModel>();
    }
}
