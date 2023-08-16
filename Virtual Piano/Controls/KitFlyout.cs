using Virtual_Piano.Midi;
using Windows.System;

namespace Virtual_Piano.Controls
{
    public sealed class KitFlyout : Midi.Instruments.KitFlyout
    {
        public KitFlyout()
        {
            base.Add(KitSet.Crash, "1️⃣", VirtualKey.F1);
            base.Add(KitSet.Ride, "2️⃣", VirtualKey.F2);
            base.Add(KitSet.Open, "3️⃣", VirtualKey.F3);
            base.Add(KitSet.Close, "4️⃣", VirtualKey.F4);
            base.Add(KitSet.Pedal, "5️⃣", VirtualKey.F5);
            base.Add();
            base.Add(KitSet.HiTom, "6️⃣", VirtualKey.F6);
            base.Add(KitSet.LowTom, "7️⃣", VirtualKey.F7);
            base.Add(KitSet.FloorTom, "8️⃣", VirtualKey.F8);
            base.Add();
            base.Add(KitSet.Snare, "9️⃣", VirtualKey.F9);
            base.Add(KitSet.Stick, "🔟", VirtualKey.F10);
            base.Add(KitSet.Kick, "⏸", VirtualKey.F11);
        }

        public override string GetString(MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}