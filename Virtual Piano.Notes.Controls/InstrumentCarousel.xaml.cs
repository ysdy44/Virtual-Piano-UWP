using System.Linq;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Notes.Controls
{
    internal readonly struct Instrument
    {
        internal readonly byte Index;
        readonly string Text;
        public override string ToString() => this.Text;
        internal Instrument(byte index, string text)
        {
            this.Index = index;
            this.Text = text;
        }
    }

    public partial class InstrumentCarousel : UserControl
    {

        //@Command
        public ICommand Command { get; set; }

        readonly Instrument[] Instrument0;
        readonly Instrument[][] Instrument1;
        readonly Instrument[][][] Instrument2;

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
                    Instrument[][] ds = this.Instrument2[c];
                    for (int d = 0; d < ds.Length; d++)
                    {
                        Instrument[] es = ds[d];
                        for (int e = 0; e < es.Length; e++)
                        {
                            Instrument iss = es[d];
                            if (iss.Index == i)
                            {
                                this.Carousel0.Reset(c);

                                Instrument[] items1 = this.Instrument1[c];
                                this.Carousel1.Reset(items1, d);

                                Instrument[] items2 = this.Instrument2[c][d];
                                this.Carousel2.Reset(items2, e);

                                return;
                            }
                        }
                    }
                }
            }
        }

        //@Construct
        public InstrumentCarousel()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.Geometry.Rect = new Rect(default, e.NewSize);
            };

            this.Instrument0 = MidiProgramFactory.Instance.Select(c =>
            new Instrument((byte)c.Key, this.GetString(c.Key))).ToArray();
            this.Carousel0.Reset(this.Instrument0);
            this.Carousel0.ItemClick += (s, e) =>
            {
                int c = e;

                Instrument[] items1 = this.Instrument1[c];
                this.Carousel1.Reset(items1);

                Instrument[] items2 = this.Instrument2[c].First();
                this.Carousel2.Reset(items2);

                MidiProgram program = (MidiProgram)items2.First().Index;
                this.Command?.Execute(program); // Command
            };

            this.Instrument1 = MidiProgramFactory.Instance.Select(c =>
            c.Value.Select(d =>
            new Instrument((byte)d.Key, this.GetString(d.Key))).ToArray()).ToArray();
            this.Carousel1.Reset(this.Instrument1.First());
            this.Carousel1.ItemClick += (s, e) =>
            {
                int c = this.Carousel0.SelectedIndex;
                int d = e;

                Instrument[] items2 = this.Instrument2[c][d];
                this.Carousel2.Reset(items2);

                MidiProgram program = (MidiProgram)items2.First().Index;
                this.Command?.Execute(program); // Command
            };

            this.Instrument2 = MidiProgramFactory.Instance.Select(c =>
            c.Value.Select(d =>
            d.Value.Select(e =>
            new Instrument((byte)e, this.GetString(e))).ToArray()).ToArray()).ToArray();
            this.Carousel2.Reset(this.Instrument2.First().First());
            this.Carousel2.ItemClick += (s, e) =>
            {
                int c = this.Carousel0.SelectedIndex;
                int d = this.Carousel1.SelectedIndex;

                MidiProgram program = (MidiProgram)this.Instrument2[c][d][e].Index;
                this.Command?.Execute(program); // Command
            };
        }

        public virtual string GetString(MidiProgram program)
        {
            return program.ToString();
        }
        public virtual string GetString(MidiProgramGroup group)
        {
            return group.ToString();
        }
        public virtual string GetString(MidiProgramCategory category)
        {
            return category.ToString();
        }
    }
}