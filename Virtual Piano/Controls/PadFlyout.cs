using Virtual_Piano.Midi;
using Windows.System;

namespace Virtual_Piano.Controls
{
    public sealed class PadFlyout : Midi.Instruments.KitFlyout
    {
        public PadFlyout()
        {
            this.Add(KitSet.Kick, "0️⃣", VirtualKey.F1);
            this.Add(KitSet.Snare, "1️⃣", VirtualKey.F2);
            this.Add(KitSet.Clap, "2️⃣", VirtualKey.F3);
            this.Add(KitSet.Close, "3️⃣", VirtualKey.F4);
            this.Add();
            this.Add(KitSet.LowTom, "4️⃣", VirtualKey.F5);
            this.Add(KitSet.Open, "5️⃣", VirtualKey.F6);
            this.Add(KitSet.MidTom, "6️⃣", VirtualKey.F7);
            this.Add(KitSet.Crash, "7️⃣", VirtualKey.F8);
            this.Add();
            this.Add(KitSet.FloorTom, "8️⃣", VirtualKey.F9);
            this.Add(KitSet.Ride, "9️⃣", VirtualKey.F10);
            this.Add(KitSet.Ring, "🔟", VirtualKey.F11);
            this.Add(KitSet.Sandham, "⏸", VirtualKey.F12);
        }

        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}