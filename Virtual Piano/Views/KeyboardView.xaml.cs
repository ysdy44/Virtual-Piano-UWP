﻿using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.Views
{
    public sealed partial class PianoView : Page
    {
        ICommand Command;
        
        public PianoView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.Command = null;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Command = e.Parameter as ICommand;
        }
    }
}