using System;
using System.Collections.ObjectModel;
using Windows.Devices.Midi;
using Windows.Media.Audio;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Midi.Core
{
    public sealed class AudioObservableCollection : ObservableCollection<ContentControl>, IAudioSynthesizer
    {
        public bool this[MidiNote note]
        {
            get
            {
                int index = (int)note;
                foreach (ContentControl item in this)
                {
                    if (item.TabIndex == index)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public string Select(MidiNote note)
        {
            int index = (int)note;
            foreach (ContentControl item in this)
            {
                if (item.TabIndex == index)
                {
                    return item.Content.ToString();
                }
            }
            return string.Empty;
        }

        public ContentControl Select(int index)
        {
            foreach (ContentControl item in this)
            {
                if (item.TabIndex == index)
                {
                    return item;
                }
            }
            return null;
        }

        public void NoteOn(MidiNote note)
        {
            int index = (int)note;
            foreach (ContentControl item in this)
            {
                if (item.TabIndex == index)
                {
                    if (item.Tag is AudioFileInputNode node)
                    {
                        node.Seek(TimeSpan.Zero);
                        node.Start();
                    }
                }
            }
        }
        public void NoteOff(MidiNote note)
        {
            int index = (int)note;
            foreach (ContentControl item in this)
            {
                if (item.TabIndex == index)
                {
                    if (item.Tag is AudioFileInputNode node)
                    {
                        node.Stop();
                    }
                }
            }
        }
        public void SendMessage(MidiMessage message)
        {
            switch (message.Type)
            {
                case MidiMessageType.NoteOff:
                    this.NoteOff(message.Note);
                    break;
                case MidiMessageType.NoteOn:
                    this.NoteOn(message.Note);
                    break;
                default:
                    break;
            }
        }

        public void Reset() => this.Dispose();

        public void Dispose()
        {
            foreach (ContentControl item in this)
            {
                item.Tag = null;
            }
            base.Clear();
        }
    }
}