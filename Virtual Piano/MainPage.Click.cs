using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Virtual_Piano.Midi;
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
            if (parameter is MidiMessage item)
            {
                this.Synthesizer.SendMessage(item);
            }
            else if (parameter is MidiNote item0)
            {
                this.Synthesizer.NoteOn(item0);
                await Task.Delay(2000);
                this.Synthesizer.NoteOff(item0);
            }
            else if (parameter is MidiProgram item1)
            {
                this.Favorites.Instrument = item1;
                this.Synthesizer.ProgramChange(item1);
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
                case OptionType.Roulette:
                    this.ClickRoulette();
                    break;

                case OptionType.FullScreen:
                    this.ClickFullScreen();
                    break;
                case OptionType.UnFullScreen:
                    this.ClickUnFullScreen();
                    break;

                case OptionType.TryShowPianoView:
                    for (int i = 0; i < 3; i++)
                        if (this.PianoViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewPianoView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.PianoViewId)) return;
                    break;
                case OptionType.TryShowChordView:
                    for (int i = 0; i < 3; i++)
                        if (this.ChordViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewChordView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.ChordViewId)) return;
                    break;
                case OptionType.TryShowGuitarView:
                    for (int i = 0; i < 3; i++)
                        if (this.GuitarViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewGuitarView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.GuitarViewId)) return;
                    break;
                case OptionType.TryShowBassView:
                    for (int i = 0; i < 3; i++)
                        if (this.BassViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewBassView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.BassViewId)) return;
                    break;
                case OptionType.TryShowHarpView:
                    for (int i = 0; i < 3; i++)
                        if (this.HarpViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewHarpView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.HarpViewId)) return;
                    break;
                case OptionType.TryShowKitView:
                    for (int i = 0; i < 3; i++)
                        if (this.KitViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewKitView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.KitViewId)) return;
                    break;
                case OptionType.TryShowPadView:
                    for (int i = 0; i < 3; i++)
                        if (this.PadViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewPadView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.PadViewId)) return;
                    break;
                case OptionType.TryShowDrumView:
                    for (int i = 0; i < 3; i++)
                        if (this.DrumViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewDrumView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.DrumViewId)) return;
                    break;
                case OptionType.TryShowMachineView:
                    for (int i = 0; i < 3; i++)
                        if (this.MachineViewId == default) await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, this.CreateNewMachineView);
                        else if (await ApplicationViewSwitcher.TryShowAsStandaloneAsync(this.MachineViewId)) return;
                    break;
                default:
                    break;
            }
        }

        int PianoViewId;
        int ChordViewId;
        int GuitarViewId;
        int BassViewId;
        int HarpViewId;
        int KitViewId;
        int PadViewId;
        int DrumViewId;
        int MachineViewId;

        private void CreateNewPianoView() => this.PianoViewId = this.CreateNew(typeof(PianoView));
        private void CreateNewChordView() => this.ChordViewId = this.CreateNew(typeof(ChordView));
        private void CreateNewGuitarView() => this.GuitarViewId = this.CreateNew(typeof(GuitarView));
        private void CreateNewBassView() => this.BassViewId = this.CreateNew(typeof(BassView));
        private void CreateNewHarpView() => this.HarpViewId = this.CreateNew(typeof(HarpView));
        private void CreateNewKitView() => this.KitViewId = this.CreateNew(typeof(KitView));
        private void CreateNewPadView() => this.PadViewId = this.CreateNew(typeof(PadView));
        private void CreateNewDrumView() => this.DrumViewId = this.CreateNew(typeof(DrumView));
        private void CreateNewMachineView() => this.MachineViewId = this.CreateNew(typeof(MachineView));

        private int CreateNew(Type sourcePageType)
        {
            Frame frame = new Frame();
            frame.Navigate(sourcePageType, this.Command);

            Window.Current.Content = frame;
            Window.Current.Activate();

            return Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Id;
        }
    }
}