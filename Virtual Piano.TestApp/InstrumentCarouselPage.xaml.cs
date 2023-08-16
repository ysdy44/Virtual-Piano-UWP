using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Virtual_Piano.Midi;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.TestApp
{
    internal readonly struct InstrumentCarouselItem
    {
        internal readonly byte Index;
        readonly string Text;
        public override string ToString() => this.Text;
        internal InstrumentCarouselItem(byte index, string text)
        {
            this.Index = index;
            this.Text = text;
        }
    }

    public sealed partial class InstrumentCarouselPage : Page
    {

        //@Command
        public ICommand Command { get; set; }

        readonly InstrumentCarouselItem[] Instrument0;
        readonly InstrumentCarouselItem[][] Instrument1;
        readonly InstrumentCarouselItem[][][] Instrument2;

        public MidiProgram SelectedItem
        {
            get
            {
                if (base.IsLoaded is false) return default;

                int c = this.Carousel0.SelectedIndex;
                int d = this.Carousel1.SelectedIndex;
                int e = this.Carousel2.SelectedIndex;

                MidiProgram program = (MidiProgram)this.Instrument2[c][d][e].Index;
                return program;
            }
            set
            {
                byte i = (byte)value;
                for (int c = 0; c < this.Instrument2.Length; c++)
                {
                    InstrumentCarouselItem[][] ds = this.Instrument2[c];
                    for (int d = 0; d < ds.Length; d++)
                    {
                        InstrumentCarouselItem[] es = ds[d];
                        for (int e = 0; e < es.Length; e++)
                        {
                            InstrumentCarouselItem iss = es[d];
                            if (iss.Index == i)
                            {
                                this.Carousel0.Reset(c);

                                InstrumentCarouselItem[] items1 = this.Instrument1[c];
                                this.Carousel1.Reset(items1, d);

                                InstrumentCarouselItem[] items2 = this.Instrument2[c][d];
                                this.Carousel2.Reset(items2, e);

                                return;
                            }
                        }
                    }
                }
            }
        }

        public InstrumentCarouselPage()
        {
            this.InitializeComponent();
            this.Instrument0 = MidiProgramFactory.Instance.Select(c =>
            new InstrumentCarouselItem((byte)c.Key, this.GetString(c.Key))).ToArray();
            this.Carousel0.Reset(this.Instrument0);
            this.Carousel0.ItemClick += (s, e) =>
            {
                int c = e;

                InstrumentCarouselItem[] items1 = this.Instrument1[c];
                this.Carousel1.Reset(items1);

                InstrumentCarouselItem[] items2 = this.Instrument2[c].First();
                this.Carousel2.Reset(items2);

                MidiProgram program = (MidiProgram)items2.First().Index;
                this.Command?.Execute(program); // Command
            };

            this.Instrument1 = MidiProgramFactory.Instance.Select(c =>
            c.Value.Select(d =>
            new InstrumentCarouselItem((byte)d.Key, this.GetString(d.Key))).ToArray()).ToArray();
            this.Carousel1.Reset(this.Instrument1.First());
            this.Carousel1.ItemClick += (s, e) =>
            {
                int c = this.Carousel0.SelectedIndex;
                int d = e;

                InstrumentCarouselItem[] items2 = this.Instrument2[c][d];
                this.Carousel2.Reset(items2);

                MidiProgram program = (MidiProgram)items2.First().Index;
                this.Command?.Execute(program); // Command
            };

            this.Instrument2 = MidiProgramFactory.Instance.Select(c =>
            c.Value.Select(d =>
            d.Value.Select(e =>
            new InstrumentCarouselItem((byte)e, this.GetString(e))).ToArray()).ToArray()).ToArray();
            this.Carousel2.Reset(this.Instrument2.First().First());
            this.Carousel2.ItemClick += (s, e) =>
            {
                int c = this.Carousel0.SelectedIndex;
                int d = this.Carousel1.SelectedIndex;

                MidiProgram program = (MidiProgram)this.Instrument2[c][d][e].Index;
                this.Command?.Execute(program); // Command
            };
        }

        public string GetString(MidiProgram program)
        {
            return program.ToString();
        }
        public string GetString(MidiProgramGroup group)
        {
            return group.ToString();
        }
        public string GetString(MidiProgramCategory category)
        {
            return category.ToString();
        }
    }
}