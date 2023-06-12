using System.Windows.Input;
using System.Xml.Linq;

namespace Virtual_Piano.Notes.Controls
{
    public interface INotePanel
    {
        ICommand Command { get; set; }
        double WhiteHeight { get; set; }
        double BlackHeight { get; set; }

        int ItemWidth { get; set; }
        KeyLabel Label { get; set; }

        INoteButton this[Note item] { get; }

        void OnClick(Note note);
    }
}