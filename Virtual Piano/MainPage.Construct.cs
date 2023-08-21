using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtual_Piano.Views;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Virtual_Piano
{
    partial class MainPage
    {
        int PianoViewId;
        int ChordViewId;
        int GuitarViewId;
        int BassViewId;
        int HarpViewId;
        int KitViewId;
        int PadViewId;
        int DrumViewId;
        int MachineViewId;

        private void CreateNewPianoView() => this.PianoViewId = this.CreateNew(typeof(PianoView));
        private void CreateNewChordView() => this.ChordViewId = this.CreateNew(typeof(ChordView));
        private void CreateNewGuitarView() => this.GuitarViewId = this.CreateNew(typeof(GuitarView));
        private void CreateNewBassView() => this.BassViewId = this.CreateNew(typeof(BassView));
        private void CreateNewHarpView() => this.HarpViewId = this.CreateNew(typeof(HarpView));
        private void CreateNewKitView() => this.KitViewId = this.CreateNew(typeof(KitView));
        private void CreateNewPadView() => this.PadViewId = this.CreateNew(typeof(PadView));
        private void CreateNewDrumView() => this.DrumViewId = this.CreateNew(typeof(DrumView));
        private void CreateNewMachineView() => this.MachineViewId = this.CreateNew(typeof(MachineView));

        private int CreateNew(Type sourcePageType)
        {
            Frame frame = new Frame();
            frame.Navigate(sourcePageType, this.Command);

            Window.Current.Content = frame;
            Window.Current.Activate();

            return Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Id;
        }
    }
}