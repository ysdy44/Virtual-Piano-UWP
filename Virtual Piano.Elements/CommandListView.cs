using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Virtual_Piano.Elements
{
    public sealed class CommandListView : ListView
    {
        //@Command
        public ICommand Command { get; set; }

        //@Construct
        public CommandListView()
        {
            base.ItemClick += (s, e) =>
            {
                this.Command?.Execute(e.ClickedItem); // Command
            };
        }
    }
}