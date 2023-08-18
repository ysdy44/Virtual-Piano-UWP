using System.Windows.Input;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano.Views
{
    public sealed partial class PianoView : Page
    {
        ICommand Command;

        // Key
        bool IsShift;
        MidiNote WhiteKey;
        MidiNote BlackKey;
        readonly IKeyDictionary WhiteKeys = new KeyQWERTDictionary(ToneType.White);
        readonly IKeyDictionary BlackKeys = new KeyQWERTDictionary(ToneType.Black);

        ~PianoView()
        {
            this.WhiteKeys.Dispose();
            this.BlackKeys.Dispose();
        }
        public PianoView()
        {
            this.InitializeComponent();
            base.Unloaded += (s, e) =>
            {
                Window.Current.CoreWindow.KeyUp -= this.CoreKeyUp;
                Window.Current.CoreWindow.KeyDown -= this.CoreKeyDown;
            };
            base.Loaded += (s, e) =>
            {
                Window.Current.CoreWindow.KeyUp -= this.CoreKeyUp;
                Window.Current.CoreWindow.KeyDown -= this.CoreKeyDown;
                Window.Current.CoreWindow.KeyUp += this.CoreKeyUp;
                Window.Current.CoreWindow.KeyDown += this.CoreKeyDown;
            };

            this.NoteScrollViewer.Loaded += (s, e) =>
            {
                var x = this.NoteScrollViewer.ScrollableWidth / 2;
                this.NoteScrollViewer.ChangeView(x, null, null, true);
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.Command = null;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Command = e.Parameter as ICommand;
        }

        // UI
        private void CoreKeyUp(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey is VirtualKey.Shift)
                this.IsShift = false;

            if (this.BlackKeys.TryGetValue(args.VirtualKey, out this.BlackKey))
                this.PianoTopPanel.Clear(this.BlackKey);

            if (this.WhiteKeys.TryGetValue(args.VirtualKey, out this.WhiteKey))
                this.PianoTopPanel.Clear(this.WhiteKey);
        }
        private void CoreKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey is VirtualKey.Shift)
                this.IsShift = true;

            if (this.IsShift)
            {
                if (this.BlackKeys.TryGetValue(args.VirtualKey, out this.BlackKey))
                    this.PianoTopPanel.Add(this.BlackKey);
            }
            else
            {
                if (this.WhiteKeys.TryGetValue(args.VirtualKey, out this.WhiteKey))
                    this.PianoTopPanel.Add(this.WhiteKey);
            }
        }
    }
}