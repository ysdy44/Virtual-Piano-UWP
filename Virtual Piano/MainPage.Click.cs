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
        public ICommand Command => null;
        public bool CanExecute(object parameter) => parameter != default;
        public void Execute(object parameter)
        {
        }
    }
}