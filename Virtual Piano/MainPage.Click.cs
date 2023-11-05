using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Virtual_Piano.Elements;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Virtual_Piano.Midi.Core;
using Virtual_Piano.Views;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
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
            if (parameter is MidiMessage item1)
            {
                this.MidiSynthesizer.SendMessage(item1);
            }
            else if (parameter is MidiNote item2)
            {
                this.AllowRing = true;

                this.MidiSynthesizer.NoteOn(item2);
                await Task.Delay(2000);
                this.MidiSynthesizer.NoteOff(item2);
            }
            else if (parameter is MidiProgram item3)
            {
                this.AllowRing = true;

                this.Favorites.Instrument = item3;
                this.MidiSynthesizer.ProgramChange(item3);
            }
            else if (parameter is MidiPercussionNote item4)
            {
                this.AllowRing = true;

                this.MidiSynthesizer.NoteOn(item4);
                await Task.Delay(2000);
                this.MidiSynthesizer.NoteOff(item4);
            }
            else if (parameter is OptionType item5)
            {
                this.Click(item5);
            }
            else if (parameter is MidiPercussionProgram item6)
            {
                this.AllowRing = true;
                this.DrumItem.Key = item6;
                this.MidiSynthesizer.ProgramChange(item6);
            }
            else if (parameter is CultureInfo item7)
            {
                if (string.IsNullOrEmpty(item7.Name))
                    CultureInfoCollection.SetLanguageEmpty();
                else
                    CultureInfoCollection.SetLanguage(item7.Name);
            }
            else if (parameter is Uri item8)
            {
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(item8);

                using (IRandomAccessStream stream = await file.OpenAsync(default))
                {
                    this.Initialize(new TrackCollection(stream));
                }
            }
            else if (parameter is MidiChannelMessage item0)
            {
                switch (item0.Type)
                {
                    case MidiChannelMessageType.ProgramChange:
                        break;
                    case MidiChannelMessageType.Edit:
                        if (this.TrackCollection is null) break;
                        if (item0.Channel < this.TrackCollection.Count)
                        {
                            if (this.TrackCollection[item0.Channel] is ContentControl contentControl)
                            {
                                if (contentControl.Content is Track track)
                                {
                                    // UI
                                    this.ClickTrackNote(item0.Channel);

                                    // Track
                                    this.TrackNotePanel.Load(track);
                                    break;
                                }
                            }
                        }

                        // UI
                        this.ClickTrack();

                        // Track
                        this.TrackNotePanel.Load(null);
                        break;
                    case MidiChannelMessageType.UnMute:
                        if (this.TrackCollection is null) break;
                        if (this.TrackCollection.Count <= item0.Channel) break;

                        if (this.TrackCollection[item0.Channel] is ContentControl item02)
                        {
                            if (item02.IsEnabled == true) break; // Mute

                            this.Click(OptionType.RePause);
                            item02.IsEnabled = true; // Mute
                            await Task.Delay(500); // UI-Thread
                            this.Click(OptionType.RePlay);
                        }
                        break;
                    case MidiChannelMessageType.Mute:
                        if (this.TrackCollection is null) break;
                        if (this.TrackCollection.Count <= item0.Channel) break;

                        if (this.TrackCollection[item0.Channel] is ContentControl item01)
                        {
                            if (item01.IsEnabled == false) break; // Mute

                            this.Click(OptionType.RePause);
                            item01.IsEnabled = false; // Mute
                            await Task.Delay(500); // UI-Thread
                            this.Click(OptionType.RePlay);
                        }
                        break;
                    case MidiChannelMessageType.UnSolo:
                        if (this.TrackCollection is null) { this.ChannelPanel.Desolo(); break; }
                        if (this.TrackCollection.Count <= item0.Channel) { this.ChannelPanel.Desolo(); break; }
                        if (this.TrackSoloChannel == item0.Channel)
                        {
                            this.Click(OptionType.RePause);
                            this.TrackSoloChannel = -1;
                            this.ChannelPanel.Desolo();
                            await Task.Delay(500); // UI-Thread
                            this.Click(OptionType.RePlay);
                        }
                        break;
                    case MidiChannelMessageType.Solo:
                        if (this.TrackCollection is null) { this.ChannelPanel.Desolo(item0.Channel); break; }
                        if (this.TrackCollection.Count <= item0.Channel) { this.ChannelPanel.Desolo(item0.Channel); break; }
                        if (this.TrackSoloChannel == item0.Channel) { this.ChannelPanel.Desolo(); break; }

                        this.Click(OptionType.RePause);
                        this.TrackSoloChannel = item0.Channel;
                        this.ChannelPanel.Desolo(item0.Channel);
                        await Task.Delay(500); // UI-Thread
                        this.Click(OptionType.RePlay);
                        break;
                    case MidiChannelMessageType.UnRecord:
                        break;
                    case MidiChannelMessageType.Record:
                        break;
                    default:
                        break;
                }
            }
        }

        public async void Click(OptionType type)
        {
            switch (type)
            {
                case OptionType.Roulette:
                    this.ClickRoulette();
                    break;

                case OptionType.Loop:
                    this.IsLoop = true;
                    break;
                case OptionType.UnLoop:
                    this.IsLoop = false;
                    break;

                case OptionType.FullScreen:
                    this.ClickFullScreen();
                    break;
                case OptionType.UnFullScreen:
                    this.ClickUnFullScreen();
                    break;

                case OptionType.MetronomeStart:
                    this.ClickMetronomeStart();
                    break;
                case OptionType.MetronomeStop:
                    this.ClickMetronomeStop();
                    break;
                case OptionType.LanguageTip:
                    await Windows.ApplicationModel.Core.CoreApplication.RequestRestartAsync(string.Empty);
                    break;
                case OptionType.LocalFolder:
                    await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
                    break;

                case OptionType.Mute:
                    this.ClickMute();
                    this.MidiSynthesizer.ControlChange(MidiControlController.MainVolume, 0);
                    this.MidiSynthesizer.ControlChange(MidiControlController.MainVolume, 0, 9);
                    break;
                case OptionType.Volume:
                    this.ClickVolume();
                    this.MidiSynthesizer.ControlChange(MidiControlController.MainVolume, 127);
                    this.MidiSynthesizer.ControlChange(MidiControlController.MainVolume, 127, 9);
                    break;

                case OptionType.New:
                    break;
                case OptionType.Open:
                    // Picker
                    FileOpenPicker openPicker = new FileOpenPicker
                    {
                        ViewMode = PickerViewMode.Thumbnail,
                        SuggestedStartLocation = default,
                        FileTypeFilter =
                        {
                            ".mid", ".midi"
                        }
                    };

                    // File
                    StorageFile file = await openPicker.PickSingleFileAsync();
                    if (file is null) break;

                    using (IRandomAccessStream stream = await file.OpenAsync(default))
                    {
                        this.Initialize(new TrackCollection(stream));
                    }
                    break;
                case OptionType.Save:
                    break;

                case OptionType.RePlay:
                    if (this.TrackCollection is null) break;

                    if (this.ReIsPlaying is false) break;
                    if (this.Player.IsPlaying is false)
                    {
                        this.Player.Play();
                    }
                    break;
                case OptionType.RePause:
                    if (this.TrackCollection is null) break;

                    this.ReIsPlaying = this.Player.IsPlaying;
                    if (this.TrackCollection is null) break;

                    if (this.Player.IsPlaying)
                    {
                        this.Player.Pause();
                    }
                    break;

                case OptionType.Play:
                    if (this.TrackCollection is null) break;

                    if (this.Player.IsPlaying is false)
                    {
                        this.Player.Play();
                    }
                    break;
                case OptionType.Pause:
                    if (this.TrackCollection is null) break;

                    if (this.Player.IsPlaying)
                    {
                        this.Player.Pause();
                    }
                    break;
                case OptionType.Stop:
                    if (this.TrackCollection is null) break;

                    //if (this.Player.IsPlaying)
                    {
                        this.Player.Reset();
                    }
                    break;

                case OptionType.Previous:
                    break;
                case OptionType.Next:
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
    }
}