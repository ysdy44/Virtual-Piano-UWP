using System;
using System.Windows.Input;
using Virtual_Piano.Midi.Core;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Virtual_Piano.Midi.Controls
{
    public class KitFlyout : MenuFlyout, ICommand
    {
        public IKitPanel KitPanel { get; set; }

        public KitFlyout() // 0️⃣
        {
            this.Add(KitSet.Crash, "1️⃣", VirtualKey.F1);
            this.Add(KitSet.Ride, "2️⃣", VirtualKey.F2);
            this.Add(KitSet.Open, "3️⃣", VirtualKey.F3);
            this.Add(KitSet.Close, "4️⃣", VirtualKey.F4);
            this.Add(KitSet.Pedal, "5️⃣", VirtualKey.F5);
            this.Add();
            this.Add(KitSet.HiTom, "6️⃣", VirtualKey.F6);
            this.Add(KitSet.LowTom, "7️⃣", VirtualKey.F7);
            this.Add(KitSet.FloorTom, "8️⃣", VirtualKey.F8);
            this.Add();
            this.Add(KitSet.Snare, "9️⃣", VirtualKey.F9);
            this.Add(KitSet.Stick, "🔟", VirtualKey.F10);
            this.Add(KitSet.Kick, "⏸", VirtualKey.F11);
        }

        private void Add() => base.Items.Add(new MenuFlyoutSeparator());
        private void Add(KitSet set, string glyph, VirtualKey key) => base.Items.Add(new MenuFlyoutItem
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
            if (parameter is KitSet item)
            {
                this.KitPanel?.OnClick(item);
            }
        }

        public virtual string GetString(MidiPercussionNote note)
        {
            return note.ToString();
        }
    }
}