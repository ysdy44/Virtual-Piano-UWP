using System.Threading.Tasks;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano
{
    partial class MainPage
    {
        private void Play()
        {
            if (this.TrackIndex >= 0 && this.TrackIndex < this.TrackCollection.Count)
            {
                ContentControl item = this.TrackCollection[this.TrackIndex];

                this.Play(item);
            }
            else
            {
                foreach (ContentControl item in this.TrackCollection)
                {
                    this.Play(item);
                }
            }
        }

        private void Play(ContentControl item)
        {
            if (item.Content is Track track)
            {
                this.Programs(track);
                this.Play(track);
                foreach (var item2 in track.Controllers)
                {
                    this.Controllers(item2.Value);
                }
            }
        }

        private async void Play(Track track)
        {
            long position = this.Player.PositionMilliseconds;
            foreach (ContentControl item in track.Notes)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                if (item.Content is MidiMessage message)
                {
                    long delay = this.TrackTempo.Scale(message.AbsoluteTime) - position;

                    if (delay < 0)
                    {
                        continue;
                    }
                    else if (delay == 0)
                    {
                        position = this.TrackTempo.Scale(message.AbsoluteTime);

                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delay > 0)
                    {
                        position = this.TrackTempo.Scale(message.AbsoluteTime);

                        await Task.Delay((int)delay);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delay > 20) // Async Position
                    {
                        position = this.Player.PositionMilliseconds;
                        delay = this.TrackTempo.Scale(message.AbsoluteTime) - position;

                        await Task.Delay((int)delay);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                }
            }
        }

        private async void Programs(Track track)
        {
            long position = this.Player.PositionMilliseconds;
            foreach (ContentControl item in track.Programs)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                if (item.Content is MidiMessage message)
                {
                    long delay = this.TrackTempo.Scale(message.AbsoluteTime) - position;

                    if (delay < 0)
                    {
                        continue;
                    }
                    else if (delay == 0)
                    {
                        position = this.TrackTempo.Scale(message.AbsoluteTime);

                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delay > 0)
                    {
                        position = this.TrackTempo.Scale(message.AbsoluteTime);

                        await Task.Delay((int)delay);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delay > 20) // Async Position
                    {
                        position = this.Player.PositionMilliseconds;
                        delay = this.TrackTempo.Scale(message.AbsoluteTime) - position;

                        await Task.Delay((int)delay);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                }
            }
        }

        private async void Controllers(ControllerCollection messages)
        {
            long position = this.Player.PositionMilliseconds;
            foreach (MidiMessage message in messages)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                long delay = this.TrackTempo.Scale(message.AbsoluteTime) - position;

                if (delay < 0)
                {
                    continue;
                }
                else if (delay == 0)
                {
                    position = this.TrackTempo.Scale(message.AbsoluteTime);

                    this.MidiSynthesizer.SendMessage(message);
                }
                else if (delay > 0)
                {
                    position = this.TrackTempo.Scale(message.AbsoluteTime);

                    await Task.Delay((int)delay);
                    this.MidiSynthesizer.SendMessage(message);
                }
                else if (delay > 20) // Async Position
                {
                    position = this.Player.PositionMilliseconds;
                    delay = this.TrackTempo.Scale(message.AbsoluteTime) - position;

                    await Task.Delay((int)delay);
                    this.MidiSynthesizer.SendMessage(message);
                }
            }
        }
    }
}