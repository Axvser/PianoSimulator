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
    }
}
