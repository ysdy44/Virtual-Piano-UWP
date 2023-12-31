using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Elements;
using Virtual_Piano.FileUtils;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Virtual_Piano.Midi.Core;
using Virtual_Piano.Strings;
using Windows.Devices.Midi;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Virtual_Piano
{
    public sealed partial class MainPage : Page, ICommand
    {
        //@String
        readonly Uri GithubLink = new Uri(UIType.GithubLink.GetString());
        readonly Uri FeedbackLink = new Uri(UIType.FeedbackLink.GetString());
        FlowDirection Direction => CultureInfoCollection.FlowDirection;

        //@Converter
        private string ObjectToStringConverter(object value) => $"{value}";
        private string DoubleToStringConverter(double value) => $"{value}";
        private string ReverseDoubleToStringConverter(double value) => $"{System.Math.Clamp(127 - (int)value, 0, 127)}";
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;
        private Visibility ReverseBooleanToVisibilityConverter(bool value) => value ? Visibility.Collapsed : Visibility.Visible;
        private Visibility BooleanNullableToVisibilityConverter(bool? value) => value is true ? Visibility.Visible : Visibility.Collapsed;
        private Visibility ReverseBooleanNullableToVisibilityConverter(bool? value) => value is true ? Visibility.Collapsed : Visibility.Visible;
        private bool NullableBooleanToBooleanConverter(bool? value) => value is true;
        private bool ReverseBooleanToBooleanConverter(bool value) => value is false;
        private bool ReverseBooleanNullableToBooleanConverter(bool? value) => value is false;
        private SplitViewPanePlacement BooleanToPlacementConverter(bool value) => value is true ? SplitViewPanePlacement.Right : SplitViewPanePlacement.Left;
        private Thickness BooleanToThickness163Converter(bool? value) => value is true ? new Thickness(0, 163, 0, -163) : default;
        private Thickness BooleanToThickness203Converter(bool? value) => value is true ? new Thickness(0, 203, 0, -203) : default;
        private int DoubleToInt32Converter(double value) => System.Math.Clamp((int)value, 0, 127);
        private double MarbleToRouletteConverter(double value) => value - 100 + 32;
        private Symbol FullScreenSymbolConverter(bool value) => value ? Symbol.BackToWindow : Symbol.FullScreen;
        private Symbol MuteSymbolConverter(bool value) => value ? Symbol.Mute : Symbol.Volume;

        // Synthesizer
        MidiSynthesizer MidiSynthesizer;
        // Track
        int TrackSelectedChannel = -1;
        int TrackSoloChannel = -1;
        TrackCollection TrackCollection;
        KeySignature TrackKeySignature = new KeySignature(4, 4);
        TimeSignature TrackTimeSignature = new TimeSignature(4, 4);
        Ticks TrackTicks;
        Tempo TrackTempo;
        TempoDuration TrackDuration;

        // Player
        bool IsLoop = true;
        bool ReIsPlaying;
        double Offset;
        readonly IPlayer Player = new Player();

        // Key
        bool IsShift;
        MidiNote WhiteKey;
        MidiNote BlackKey;
        readonly IKeyDictionary WhiteKeys = new KeyQWERTDictionary(ToneType.White);
        readonly IKeyDictionary BlackKeys = new KeyQWERTDictionary(ToneType.Black);
        /*
        readonly ObservableCollection<ContentControl> ItemsSource = new ObservableCollection<ContentControl>();
         */

        // Songs
        readonly IList<Uri> Songs = new List<Uri>(Enum.GetValues(typeof(SongType)).Cast<SongType>().Select(SongUri.GetUri));

        // Drum
        readonly MidiPercussionNote DemoPercussionNote = (MidiPercussionNote)KitSet.HiTom;
        readonly MidiPercussionProgram[] Drums = System.Enum.GetValues(typeof(MidiPercussionProgram)).Cast<MidiPercussionProgram>().ToArray();

        // Favorites
        readonly MidiNote DemoNote = MidiNote.C5;
        readonly Controls.InstrumentObservableCollection Favorites = new Controls.InstrumentObservableCollection
        {
            MidiProgram.AcousticGrand,
            MidiProgram.ElectricPiano1,
            MidiProgram.Harpsichord,
            MidiProgram.Celesta,
            MidiProgram.Glockenspiel,
            MidiProgram.Vibraphone,
            MidiProgram.Marimba,
            MidiProgram.Xylophone,
            MidiProgram.ChurchOrgan,
            MidiProgram.AcousticGuitarNylon,
            MidiProgram.AcousticGuitarSteel,
            MidiProgram.TremoloStrings,
            MidiProgram.PizzicatoStrings,
            MidiProgram.OrchestralStrings,
            MidiProgram.Timpani,
            MidiProgram.StringEnsemble1,
            MidiProgram.StringEnsemble2,
            MidiProgram.ChoirAahs,
            MidiProgram.VoiceOohs,
            MidiProgram.OrchestraHit,
            MidiProgram.BrassSection,
            MidiProgram.SynthBrass1,
            MidiProgram.Whistle,
            MidiProgram.Pad2Warm,
            MidiProgram.Pad7Halo,
            MidiProgram.Pad8Sweep,
            MidiProgram.FX2Soundtrack,
            MidiProgram.Shamisen,
            MidiProgram.Kalimba,
            MidiProgram.Agogo,
            MidiProgram.SteelDrums,
            MidiProgram.Woodblock,
            MidiProgram.TaikoDrum,
            MidiProgram.MelodicTom,
            MidiProgram.SynthDrum,
            MidiProgram.ReverseCymbal,
            MidiProgram.GuitarFretNoise,
            MidiProgram.Seashore,
            MidiProgram.Applause,
            MidiProgram.Gunshot,
        };

        // Icon
        bool IsRoulette;
        readonly AnimationRouletterDictionary RouletterDictionary = new AnimationRouletterDictionary();
        readonly MidiInstrumentDictionary InstrumentDictionary = new MidiInstrumentDictionary();
        readonly MidiInstrumentDictionary InstrumentDictionary2 = new MidiInstrumentDictionary();
        Geometry RouletterData(int index) => this.RouletterDictionary[index];
        Geometry InstrumentData(int index) => this.InstrumentDictionary[(MidiInstrument)index];
        Geometry InstrumentData2(int index) => this.InstrumentDictionary2[(MidiInstrument)index];

        // Timer
        bool AllowRing;
        readonly DispatcherTimer RingTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(250)
        };

        int MetronomeIndex;
        readonly DispatcherTimer MetronomeTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };

        // Setting
        readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
        readonly CultureInfoCollection Cultures = new CultureInfoCollection();
        readonly ListViewItem[] Languages;
        private ListViewItem LanguageSelect(CultureInfo item) => new ListViewItem
        {
            ContentTemplate = string.IsNullOrEmpty(item.Name) ? this.LanguageUseSystemSettingTemplate : this.LanguageTemplate,
            Content = item
        };

        //@Construct
        ~MainPage()
        {
            this.MidiSynthesizer?.Dispose();
            this.MidiSynthesizer = null;

            this.WhiteKeys.Dispose();
            this.BlackKeys.Dispose();

            this.MetronomeTimer.Stop();
            this.RingTimer.Stop();
        }
        public MainPage()
        {
            this.InitializeComponent();
            this.TrackTicks = new Ticks(this.TrackTimeSignature, 480);
            this.TrackTempo = new Tempo(this.TrackTicks, 120);
            this.TrackDuration = new TempoDuration(this.TrackTempo, 120);
            this.Languages = this.Cultures.Select(this.LanguageSelect).ToArray();
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

            this.SplitButton.Click += (s, e) =>
            {
                this.SplitView.IsPaneOpen = !this.SplitView.IsPaneOpen;
            };

            this.TagListView.SelectionChanged += (s, e) =>
            {
                this.PianoTopPanel.Label = (KeyLabel)this.TagListView.SelectedIndex; ;
            };

            this.NoteScrollViewer.Loaded += (s, e) =>
            {
                var x = this.NoteScrollViewer.ScrollableWidth / 2;
                this.NoteScrollViewer.ChangeView(x, null, null, true);
            };
            this.DrumScrollViewer.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                this.DrumPanel.UpdateWidthCount(e.NewSize.Width);
            };

            // Timer
            this.RingTimer.Tick += (s, e) =>
            {
                if (this.AllowRing)
                {
                    this.AllowRing = false;
                    this.MarbleButton.ShowRing();
                }
            };

            this.MetronomeTimer.Tick += async (s, e) =>
            {
                this.MetronomeIndex++;
                switch (this.MetronomeIndex % this.TrackTicks.BeatsPerBar)
                {
                    case 0:
                        this.MidiSynthesizer.NoteOn(MidiPercussionNote.MetronomeBell);
                        await System.Threading.Tasks.Task.Delay(1000);
                        this.MidiSynthesizer.NoteOff(MidiPercussionNote.MetronomeBell);
                        break;
                    default:
                        this.MidiSynthesizer.NoteOn(MidiPercussionNote.MetronomeClick);
                        await System.Threading.Tasks.Task.Delay(1000);
                        this.MidiSynthesizer.NoteOff(MidiPercussionNote.MetronomeClick);
                        break;
                }
            };

            // Setting
            ElementTheme theme = this.GetTheme();
            this.SetTheme(theme);

            this.LightRadioButton.IsChecked = (theme == ElementTheme.Light);
            this.DarkRadioButton.IsChecked = (theme == ElementTheme.Dark);
            this.DefaultRadioButton.IsChecked = (theme == ElementTheme.Default);

            this.LightRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Light);
            this.DarkRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Dark);
            this.DefaultRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Default);


            // Player
            this.Player.Tick += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (this.Player.PositionMilliseconds < this.TrackDuration.DurationMilliseconds)
                {
                    this.Progress();
                    return;
                }

                if (this.IsLoop)
                {
                    this.Player.Reset();
                    this.Player.Play();
                }
                else
                {
                    this.Player.Pause();
                }
            };
            this.Player.CurrentStateChanged += (s, e) =>
            {
                switch (e)
                {
                    case Windows.Media.Playback.MediaPlaybackState.None:
                        this.Stop();
                        this.ClickPlay();
                        break;
                    case Windows.Media.Playback.MediaPlaybackState.Playing:
                        this.Start();
                        this.Play();

                        this.ClickPause();
                        break;
                    default:
                        this.ClickPlay();
                        break;
                }
            };

            // Track
            this.TrackNotePanel.BackClick += (s, e) => this.ClickTrack(); // UI
            this.TrackNotePanel.FootItemClick += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (e.ClickedItem is MidiControlController item)
                {
                    int i = this.TrackSelectedChannel;
                    TrackCollection tracks = this.TrackCollection;
                    if (tracks[i].Content is Track track)
                    {
                        if (track.Controllers.ContainsKey(item))
                        {
                            this.TrackNotePanel.LoadCC(track.Controllers[item]);
                        }
                    }
                }
            };

            this.TrackPanel.DragStarted += (s, e) =>
            {
                this.Offset = e.HorizontalOffset;

                this.TrackPanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackPanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.Click(OptionType.RePause);

                this.DigitalTimer.Time = timespan;
                this.Player.Seek(timespan, milliseconds);
            };
            this.TrackPanel.DragDelta += (s, e) =>
            {
                this.Offset += e.HorizontalChange;

                this.TrackPanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackPanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.DigitalTimer.Time = timespan;
                this.Player.Seek(timespan, milliseconds);
            };
            this.TrackPanel.DragCompleted += (s, e) =>
            {
                this.TrackPanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackPanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.DigitalTimer.Time = timespan;
                this.Player.Seek(timespan, milliseconds);

                this.Click(OptionType.RePlay);
            };

            this.TrackNotePanel.DragStarted += (s, e) =>
            {
                this.Offset = e.HorizontalOffset;

                this.TrackNotePanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackNotePanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.Click(OptionType.RePause);

                this.DigitalTimer.Time = timespan;
                this.Player.Seek(timespan, milliseconds);
            };
            this.TrackNotePanel.DragDelta += (s, e) =>
            {
                this.Offset += e.HorizontalChange;

                this.TrackNotePanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackNotePanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.DigitalTimer.Time = timespan;
                this.Player.Seek(timespan, milliseconds);
            };
            this.TrackNotePanel.DragCompleted += (s, e) =>
            {
                this.TrackNotePanel.ChangePositionUI((int)this.Offset);
                long milliseconds = System.Math.Max(0, this.TrackTempo.ToMilliseconds(this.TrackNotePanel.Position));
                TimeSpan timespan = TimeSpan.FromMilliseconds(milliseconds);

                this.DigitalTimer.Time = timespan;
                this.Player.Seek(timespan, milliseconds);

                this.Click(OptionType.RePlay);
            };


            // Track
            // Tempo
            this.TempoFlyout.Closed += (s, e) => this.TempoSlider.Value = this.TrackTempo.Bpm;
            this.TempoButton.Click += (s, e) =>
            {
                if (this.TrackCollection == null)
                {
                    this.TrackTempo = new Tempo(this.TrackTicks);
                    this.TrackDuration = new TempoDuration(this.TrackTempo);
                }
                else
                {
                    this.TrackTempo = new Tempo(this.TrackTicks, this.TempoSlider.Value);
                    this.TrackDuration = new TempoDuration(this.TrackTempo, this.TrackCollection.Duration);
                }

                this.Click(OptionType.Stop);
            };

            // TimeSignature
            this.NumeratorComboBox.ItemsSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            this.DenominatorComboBox.ItemsSource = new int[] { 2, 4, 8 };
            this.NumeratorComboBox.SelectedItem = this.TrackTimeSignature.Numerator;
            this.DenominatorComboBox.SelectedItem = this.TrackTimeSignature.Denominator;
            this.NumeratorComboBox.SelectionChanged += (s, e) =>
            {
                if (this.TimeSignatureFlyout.IsOpen is false) return;
                TimeSignature timeSignature = new TimeSignature((int)this.NumeratorComboBox.SelectedItem, this.TrackTimeSignature.Denominator);
                this.TimeSignaturesPanel.Update(timeSignature);
            };
            this.DenominatorComboBox.SelectionChanged += (s, e) =>
            {
                if (this.TimeSignatureFlyout.IsOpen is false) return;
                TimeSignature timeSignature = new TimeSignature(this.TrackTimeSignature.Numerator, (int)this.DenominatorComboBox.SelectedItem);
                this.TimeSignaturesPanel.UpdateDenominator(timeSignature);
            };
            this.TimeSignatureButton.Click += (s, e) =>
            {
                TimeSignature timeSignature = new TimeSignature((int)this.NumeratorComboBox.SelectedItem, (int)this.DenominatorComboBox.SelectedItem);

                this.TrackTimeSignature = timeSignature;
                this.TrackTicks = new Ticks(this.TrackTicks, this.TrackTimeSignature, timeSignature);

                this.TrackTempo = new Tempo(this.TrackTicks, this.TrackTempo.Bpm);
                this.TrackDuration = new TempoDuration(this.TrackTempo, this.TrackDuration.Duration);

                this.TrackPanel.Init(this.TrackTimeSignature, this.TrackTicks);
                this.TrackNotePanel.Init(this.TrackTimeSignature, this.TrackTicks);
            };

            this.TimeSignatureFlyout.Closed += (s, e) =>
            {
                this.NumeratorComboBox.SelectedItem = this.TrackTimeSignature.Numerator;
                this.DenominatorComboBox.SelectedItem = this.TrackTimeSignature.Denominator;
            };
            this.TimeSignatureFlyout.Opened += (s, e) =>
            {
                this.TimeSignaturesPanel.Update(this.TrackTimeSignature);
            };
            this.TimeSignaturesPanel.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.TimeSignaturesPanel.Update(this.TrackTimeSignature, e.NewSize.Width);
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.MidiSynthesizer?.Dispose();
            this.MidiSynthesizer = null;

            this.MetronomeTimer.Stop();
            this.RingTimer.Stop();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // this.MetronomeTimer.Stop();
            this.RingTimer.Start();

            this.MidiSynthesizer?.Dispose();
            this.MidiSynthesizer = await MidiSynthesizer.CreateAsync();
        }

        private void Initialize(TrackCollection tracks)
        {
            // UI
            this.ClickTrack();
            this.Click(OptionType.Stop);

            // Track
            this.TrackCollection = tracks;
            this.TrackKeySignature = new KeySignature(tracks.SharpsFlats, tracks.MajorMinor);
            this.TrackTimeSignature = new TimeSignature(tracks.Numerator, tracks.Denominator);
            this.TrackTicks = new Ticks(this.TrackTimeSignature, tracks.DeltaTicksPerQuarterNote);
            this.TrackTempo = new Tempo(this.TrackTicks, tracks.Tempo);
            this.TrackDuration = new TempoDuration(this.TrackTempo, tracks.Duration);

            // Timer
            this.ClickMetronomeStop();
            this.UpdateMetronome(this.TrackTicks, this.TrackTempo);

            // UI
            this.UpdateTrackPanel(this.TrackCollection);
            this.UpdateTrackTimeSignature(this.TrackTimeSignature);
            this.UpdateTrackTicks(this.TrackTimeSignature, this.TrackTicks);
            this.UpdateTrackTempo(this.TrackTicks, this.TrackTempo);
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