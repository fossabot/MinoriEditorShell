﻿using System.Windows;

namespace MinoriEditorStudio.Platforms.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;

            Closing += (s, e) => Owner.Focus();
        }
    }
}