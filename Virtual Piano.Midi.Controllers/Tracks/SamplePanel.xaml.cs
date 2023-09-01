using System;
using System.Linq;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Virtual_Piano.Midi.Controllers
{
    [ContentProperty(Name = nameof(Pane))]
    public sealed partial class SamplePanel : Canvas, ICommand
    {
        //@Command
        public ICommand Command {get; set;} 

        // UI
        public object ItemsSource { get => this.ItemsControl.ItemsSource; set => this.ItemsControl.ItemsSource = value; }
        public UIElement Pane { get => this.PaneBorder.Child; set => this.PaneBorder.Child = value; }

        readonly SampleLayout Layout = new SampleLayout(32, 120, 40);

        public SamplePanel()
        {
            this.InitializeComponent();

            foreach (MidiNote item in System.Enum.GetValues(typeof(MidiNote)).Cast<MidiNote>())
            {
                int index = (int)item;

                Border border = new Border
                {
                    Height = this.Layout.ItemSize,
                    Style = base.Resources[$"{item.ToType()}"] as Style,
                    Child = new TextBlock
                    {
                        Text = $"{index}"
                    }
                };
                Canvas.SetTop(border, index * this.Layout.ItemSize);
                this.BackgroundCanvas.Children.Add(border);

                Button button = new Button
                {
                    Width = this.Layout.Bar,
                    Height = this.Layout.ItemSize,
                    CommandParameter = index,
                    Command = this,
                    TabIndex = index,
                    Content = new SymbolIcon
                    {
                        Symbol = Symbol.Import,
                    }
                };
                Canvas.SetTop(button, index * this.Layout.ItemSize);
                this.NoteBackgroundCanvas.Children.Add(button);
            }

            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                int w = (int)e.NewSize.Width;
                int h = (int)e.NewSize.Height;

                int w2 = w - this.Layout.PaneBar;
                foreach (FrameworkElement item in this.BackgroundCanvas.Children)
                {
                    item.Width = w2;
                }
            };
        }

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public bool CanExecute(object parameter) => parameter != default;
        public void Execute(object parameter) => this.Command?.Execute(parameter);
    }
}