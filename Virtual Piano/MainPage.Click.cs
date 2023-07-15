using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Virtual_Piano.Notes;
using Virtual_Piano.Views;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano
{
    partial class MainPage
    {

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public ICommand Command => this;
        public bool CanExecute(object parameter) => parameter != default;
        public async void Execute(object parameter)
        {
            if (parameter is Message item)
            {
                this.Synthesizer.SendMessage(item);
            }
            else if (parameter is Note item0)
            {
                this.Synthesizer.NoteOn(item0);
                await Task.Delay(2000);
                this.Synthesizer.NoteOff(item0);
            }
            else if (parameter is MidiProgram item1)
            {
                this.Synthesizer.NoteOff(Note.C5);
                this.Synthesizer.ProgramChange(item1);

                this.Synthesizer.NoteOn(Note.C5);
                await Task.Delay(2000);
                this.Synthesizer.NoteOff(Note.C5);
            }
            else if (parameter is MidiPercussionNote item2)
            {
                this.Synthesizer.NoteOn(item2);
            }
            else if (parameter is OptionType item3)
            {
                this.Click(item3);
            }
        }

        public async void Click(OptionType type)
        {
            switch (type)
            {
                case OptionType.TryShowKeyboardView:
                    for (int i = 0; i < 3; i++)
                        if (this.KeyboardViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewKeyboardView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.KeyboardViewId)) return;
                    break;
                case OptionType.TryShowChordView:
                    for (int i = 0; i < 3; i++)
                        if (this.ChordViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewChordView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.ChordViewId)) return;
                    break;
                case OptionType.TryShowDrumView:
                    for (int i = 0; i < 3; i++)
                        if (this.DrumViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewDrumView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.DrumViewId)) return;
                    break;
                case OptionType.TryShowGuitarView:
                    for (int i = 0; i < 3; i++)
                        if (this.GuitarViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewGuitarView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.GuitarViewId)) return;
                    break;

                default:
                    break;
            }
        }

        int KeyboardViewId;
        int ChordViewId;
        int DrumViewId;
        int GuitarViewId;

        private void CreateNewKeyboardView()
        {
            Frame frame = new Frame();
            frame.Navigate(typeof(KeyboardView), this.Command);

            Window.Current.Content = frame;
            Window.Current.Activate();

            this.KeyboardViewId = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Id;
        }

        private void CreateNewChordView()
        {
            Frame frame = new Frame();
            frame.Navigate(typeof(ChordView), this.Command);

            Window.Current.Content = frame;
            Window.Current.Activate();

            this.ChordViewId = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Id;
        }
        private void CreateNewDrumView()
        {
            Frame frame = new Frame();
            frame.Navigate(typeof(DrumView), this.Command);

            Window.Current.Content = frame;
            Window.Current.Activate();

            this.DrumViewId = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Id;
        }
        private void CreateNewGuitarView()
        {
            Frame frame = new Frame();
            frame.Navigate(typeof(GuitarView), this.Command);

            Window.Current.Content = frame;
            Window.Current.Activate();

            this.GuitarViewId = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Id;
        }
    }
}