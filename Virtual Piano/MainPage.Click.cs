using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Virtual_Piano.Notes;

namespace Virtual_Piano
{
    partial class MainPage
    {

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public ICommand Command => this;
        public bool CanExecute(object parameter) => parameter != default;
        public async void Execute(object parameter)
        {
            if (parameter is Note item0)
            {
                this.Synthesizer.NoteOn(item0);
                await Task.Delay(2000);
                this.Synthesizer.NoteOff(item0);
            }
            else if (parameter is MidiProgram item1)
            {
                this.Synthesizer.NoteOff(Note.Do3);
                this.Synthesizer.ProgramChange(item1);

                this.Synthesizer.NoteOn(Note.Do3);
                await Task.Delay(2000);
                this.Synthesizer.NoteOff(Note.Do3);
            }
            else if (parameter is MidiPercussionNote item2)
            {
                this.Synthesizer.NoteOn(item2);
            }
        }
    }
}