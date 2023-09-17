using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Elements;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;
using Windows.Foundation;
using Windows.Storage;
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
        readonly Uri GithubLink = new Uri(App.Resource.GetString(UIType.GithubLink.ToString()));
        readonly Uri FeedbackLink = new Uri($"mailto:{App.Resource.GetString(UIType.FeedbackLink.ToString())}");
        FlowDirection Direction => CultureInfoCollection.FlowDirection;

        //@Converter
        private string DoubleToStringConverter(double value) => $"{System.Math.Clamp((int)value, 0, 127)}";
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;
        private Visibility ReverseBooleanToVisibilityConverter(bool value) => value ? Visibility.Collapsed : Visibility.Visible;
        private Visibility BooleanNullableToVisibilityConverter(bool? value) => value is true ? Visibility.Visible : Visibility.Collapsed;
        private Visibility ReverseBooleanNullableToVisibilityConverter(bool? value) => value is true ? Visibility.Collapsed : Visibility.Visible;
        private bool ReverseBooleanToBooleanConverter(bool value) => value is false;
        private bool ReverseBooleanNullableToBooleanConverter(bool? value) => value is false;
        private SplitViewPanePlacement BooleanToPlacementConverter(bool value) => value is true ? SplitViewPanePlacement.Right : SplitViewPanePlacement.Left;
        private Thickness BooleanToThickness163Converter(bool value) => value ? new Thickness(0, 163, 0, -163) : default;
        private Thickness BooleanToThickness203Converter(bool value) => value ? new Thickness(0, 203, 0, -203) : default;
        private double MarbleToRouletteConverter(double value) => value - 100 + 32;
        private Symbol FullScreenSymbolConverter(bool value) => value ? Symbol.BackToWindow : Symbol.FullScreen;
        private Symbol MuteSymbolConverter(bool value) => value ? Symbol.Mute : Symbol.Volume;

        // Synthesizer
        MidiSynthesizer MidiSynthesizer;
        // Track
        int TrackIndex = -1;
        TrackCollection TrackCollection;
        // Player
        readonly ITickPlayer Player = new TickPlayer();

        // Key
        bool IsShift;
        MidiNote WhiteKey;
        MidiNote BlackKey;
        readonly IKeyDictionary WhiteKeys = new KeyQWERTDictionary(ToneType.White);
        readonly IKeyDictionary BlackKeys = new KeyQWERTDictionary(ToneType.Black);
        /*
        readonly ObservableCollection<ContentControl> ItemsSource = new ObservableCollection<ContentControl>();
         */

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
            Interval = new Tempo(60).Delay * 4
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

            this.Label0Item.Click += (s, e) => this.PianoTopPanel.Label = KeyLabel.Off;
            this.Label1Item.Click += (s, e) => this.PianoTopPanel.Label = KeyLabel.Conly;
            this.Label2Item.Click += (s, e) => this.PianoTopPanel.Label = KeyLabel.CDE;
            this.Label3Item.Click += (s, e) => this.PianoTopPanel.Label = KeyLabel.DoReMi;

            this.Label0Item.Click += (s, e) => this.Label1Item.IsChecked = this.Label2Item.IsChecked = this.Label3Item.IsChecked = false;
            this.Label1Item.Click += (s, e) => this.Label0Item.IsChecked = this.Label2Item.IsChecked = this.Label3Item.IsChecked = false;
            this.Label2Item.Click += (s, e) => this.Label0Item.IsChecked = this.Label1Item.IsChecked = this.Label3Item.IsChecked = false;
            this.Label3Item.Click += (s, e) => this.Label0Item.IsChecked = this.Label1Item.IsChecked = this.Label2Item.IsChecked = false;

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
                switch (this.MetronomeIndex % 4)
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
            this.Player.TickProgress += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                if (this.Player.PositionMilliseconds >= this.TrackCollection.Duration)
                {
                    this.Player.Stop();
                }

                this.Progress();
            };

            // Track
            this.TrackNotePanel.BackClick += (s, e) =>
            {
                // UI
                this.TrackIndex = -1;
                this.TrackNotePanel.Visibility = Visibility.Collapsed;
                this.TrackPanel.Visibility = Visibility.Visible;
            };
            this.TrackPanel.ItemClick += (s, e) =>
            {
                if (this.TrackCollection is null) return;

                int index = e;
                if (index < this.TrackCollection.Count)
                {
                    if (this.TrackCollection[index] is ContentControl contentControl)
                    {
                        if (contentControl.Tag is TrackInfo trackInfo)
                        {
                            // UI
                            this.TrackIndex = index;
                            this.TrackPanel.Visibility = Visibility.Collapsed;
                            this.TrackNotePanel.Visibility = Visibility.Visible;

                            // Track
                            this.TrackNotePanel.LoadInfo(trackInfo);
                            return;
                        }
                    }
                }

                // UI
                this.TrackIndex = -1;
                this.TrackNotePanel.Visibility = Visibility.Collapsed;
                this.TrackPanel.Visibility = Visibility.Visible;
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
            this.MidiSynthesizer?.Dispose();
            this.MidiSynthesizer = await MidiSynthesizer.CreateAsync();

            this.RingTimer.Start();
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