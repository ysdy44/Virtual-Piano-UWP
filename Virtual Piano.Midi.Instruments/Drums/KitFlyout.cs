using System;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Instruments
{
    public abstract class KitFlyout : MenuFlyout, ICommand
    {
        public IKitPanel KitPanel { get; set; }

        public void Add() => base.Items.Add(new MenuFlyoutSeparator());
        public void Add(KitSet set, string glyph, VirtualKey key) => base.Items.Add(new MenuFlyoutItem
        {
            Text = this.GetString((MidiPercussionNote)set),
            CommandParameter = set,
            Command = this,
            Icon = new FontIcon
            {
                Glyph = glyph,
                FontFamily = new FontFamily("Segoe UI Emoji")
            },
            KeyboardAccelerators =
            {
                new KeyboardAccelerator
                {
                    Key = key
                }
            }
        });

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (this.KitPanel is null) return;

            if (parameter is KitSet item)
            {
                this.KitPanel.Execute(item);
            }
        }

        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }
    }
}