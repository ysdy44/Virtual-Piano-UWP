using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;

namespace Virtual_Piano.Midi.Controls
{
    public class InstrumentObservableCollection : ObservableCollection<MidiProgram>, ICommand
    {
        private MidiProgram instrument;
        public MidiProgram Instrument
        {
            get => this.instrument;
            set
            {
                this.instrument = value;
                this.OnPropertyChanged(nameof(Instrument));
                this.OnPropertyChanged(nameof(IsContains));
                this.OnPropertyChanged(nameof(String));
                this.OnPropertyChanged(nameof(Emoji));
            }
        }

        public bool IsContains => base.Contains(this.Instrument);
        public string String => this.GetString(this.Instrument);
        public string Emoji
        {
            get
            {
                foreach (var item in MidiProgramFactory.Instance)
                {
                    foreach (var item2 in item.Value)
                    {
                        foreach (var item3 in item2.Value)
                        {
                            if (item3 == this.Instrument)
                            {
                                return MidiProgramFactory.Emoji[item2.Key];
                            }
                        }
                    }
                }

                return string.Empty;
            }
        }

        public virtual string GetString(MidiProgram program)
        {
            return program.ToString();
        }

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public ICommand Command => this;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (parameter is bool item)
            {
                if (item)
                {
                    base.Remove(this.Instrument);
                    this.OnPropertyChanged(nameof(IsContains));
                }
                else
                {
                    base.Add(this.Instrument);
                    this.OnPropertyChanged(nameof(IsContains));
                }
            }
        }

        //@Notify 
        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName"> Name of the property used to notify listeners. </param>
        private void OnPropertyChanged(string propertyName) => base.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }
}