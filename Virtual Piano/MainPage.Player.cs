﻿using System.Threading.Tasks;
using Virtual_Piano.Midi;
using Virtual_Piano.Midi.Controllers;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano
{
    partial class MainPage
    {
        private void Play()
        {
            if (this.TrackSelectedChannel >= 0 && this.TrackSelectedChannel < this.TrackCollection.Count)
            {
                ContentControl item = this.TrackCollection[this.TrackSelectedChannel];

                this.Play(item);
            }
            else if (this.TrackSoloChannel >= 0 && this.TrackSoloChannel < this.TrackCollection.Count)
            {
                ContentControl item = this.TrackCollection[this.TrackSoloChannel];

                this.Play(item); // Solo
            }
            else
            {
                foreach (ContentControl item in this.TrackCollection)
                {
                    if (item.IsEnabled) // Mute
                    {
                        this.Play(item);
                    }
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
            long positionMilliseconds = this.Player.PositionMilliseconds;
            foreach (ContentControl item in track.Notes)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                if (item.Content is MidiMessage message)
                {
                    long delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                    if (delayMilliseconds < 0)
                    {
                        continue;
                    }
                    else if (delayMilliseconds == 0)
                    {
                        positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delayMilliseconds > 0)
                    {
                        positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                        await Task.Delay((int)delayMilliseconds);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delayMilliseconds > 20) // Async Position
                    {
                        positionMilliseconds = this.Player.PositionMilliseconds;
                        delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                        await Task.Delay((int)delayMilliseconds);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                }
            }
        }

        private async void Programs(Track track)
        {
            long positionMilliseconds = this.Player.PositionMilliseconds;
            foreach (ContentControl item in track.Programs)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                if (item.Content is MidiMessage message)
                {
                    long delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                    if (delayMilliseconds < 0)
                    {
                        continue;
                    }
                    else if (delayMilliseconds == 0)
                    {
                        positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delayMilliseconds > 0)
                    {
                        positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                        await Task.Delay((int)delayMilliseconds);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                    else if (delayMilliseconds > 20) // Async Position
                    {
                        positionMilliseconds = this.Player.PositionMilliseconds;
                        delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                        await Task.Delay((int)delayMilliseconds);
                        this.MidiSynthesizer.SendMessage(message);
                    }
                }
            }
        }

        private async void Controllers(ControllerCollection messages)
        {
            long positionMilliseconds = this.Player.PositionMilliseconds;
            foreach (MidiMessage message in messages)
            {
                if (this.Player.IsPlaying is false)
                {
                    return;
                }

                long delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                if (delayMilliseconds < 0)
                {
                    continue;
                }
                else if (delayMilliseconds == 0)
                {
                    positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                    this.MidiSynthesizer.SendMessage(message);
                }
                else if (delayMilliseconds > 0)
                {
                    positionMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime);

                    await Task.Delay((int)delayMilliseconds);
                    this.MidiSynthesizer.SendMessage(message);
                }
                else if (delayMilliseconds > 20) // Async Position
                {
                    positionMilliseconds = this.Player.PositionMilliseconds;
                    delayMilliseconds = this.TrackTempo.ToMilliseconds(message.AbsoluteTime) - positionMilliseconds;

                    await Task.Delay((int)delayMilliseconds);
                    this.MidiSynthesizer.SendMessage(message);
                }
            }
        }
    }
}