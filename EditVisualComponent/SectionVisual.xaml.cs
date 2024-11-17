using PianoSimulator.BasicService;
using PianoSimulator.Generalization;
using PianoSimulator.Visualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PianoSimulator.EditVisualComponent
{
    public partial class SectionVisual : VisualUnitControlBase
    {
        public SectionVisual()
        {
            InitializeComponent();
        }

        private static SectionVisual? _selectedInstance = null;
        public static SectionVisual? Selected
        {
            get => _selectedInstance;
            set
            {
                _selectedInstance = value;
            }
        }

        public List<IMusicUnit> Value
        {
            get
            {
                List<IMusicUnit> result = [];
                foreach (TrackVisual track in Tracks.Children)
                {
                    result = [.. result, .. track.Value];
                }
                return result;
            }
        }

        public SectionVisual(MusicTheory musicTheory)
        {
            InitializeComponent();
            RenderTracks(musicTheory);
        }

        public void RenderTracks(MusicTheory theory)
        {
            Track0.RenderCells(theory);
            Track1.RenderCells(theory);
            Track2.RenderCells(theory);
            Track3.RenderCells(theory);
            Track4.RenderCells(theory);
            Track5.RenderCells(theory);
        }
        public void Scroll(Thickness value)
        {
            var vartical = SV.HorizontalOffset;
            var newHorizontal = vartical - value.Top + value.Bottom;
            SV.ScrollToVerticalOffset(newHorizontal);
        }

        private void VisualUnitControlBase_MouseEnter(object sender, MouseEventArgs e)
        {
            Selected = this;
        }
        private void VisualUnitControlBase_MouseLeave(object sender, MouseEventArgs e)
        {
            Selected = null;
        }
    }
}
