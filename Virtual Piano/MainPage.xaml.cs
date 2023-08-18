using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Core;
using Windows.Devices.Midi;
using Windows.Foundation;
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
        //@Converter
        private string DoubleToStringConverter(double value) => $"{System.Math.Clamp((int)value, 0, 127)}";
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;
        private Visibility ReverseBooleanToVisibilityConverter(bool value) => value ? Visibility.Collapsed : Visibility.Visible;
        private Visibility BooleanNullableToVisibilityConverter(bool? value) => value is true ? Visibility.Collapsed : Visibility.Visible;
        private bool ReverseBooleanNullableToBooleanConverter(bool? value) => value is true is false;
        private SplitViewPanePlacement BooleanToPlacementConverter(bool value) => value is true ? SplitViewPanePlacement.Right : SplitViewPanePlacement.Left;
        private double MarbleToRouletteConverter(double value) => value - 100 + 32;
        private Symbol FullScreenSymbolConverter(bool value) => value ? Symbol.BackToWindow : Symbol.FullScreen;

        // Synthesizer
        MidiSynthesizer Synthesizer;

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

        //@Construct
        ~MainPage()
        {
            this.Synthesizer?.Dispose();

            this.WhiteKeys.Dispose();
            this.BlackKeys.Dispose();
        }
        public MainPage()
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

            this.SplitButton.Click += (s, e) =>
            {
                this.SplitView.IsPaneOpen = !this.SplitView.IsPaneOpen;
            };
            this.ListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is InstrumentItem item)
                {
                    this.Favorites.Instrument = item.Key;
                }
            };

            this.DrumScrollViewer.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
                if (e.NewSize.Width == e.PreviousSize.Width) return;

                this.DrumPanel.UpdateWidthCount(e.NewSize.Width);
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.Synthesizer?.Dispose();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Synthesizer?.Dispose();
            this.Synthesizer = await MidiSynthesizer.CreateAsync();
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