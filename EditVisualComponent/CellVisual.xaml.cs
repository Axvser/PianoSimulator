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
    public partial class CellVisual : VisualUnitControlBase
    {
        public CellVisual()
        {
            InitializeComponent();
        }

        private static TransitionBoard<CellVisual> _selected = Transition.CreateBoardFromType<CellVisual>()
            .SetProperty(x=>x.BorderBrush,Brushes.Cyan)
            .SetProperty(x=>x.BorderThickness,new Thickness(1))
            .SetParams((x) =>
            {
                x.Duration = 0.4;
            });
        private static TransitionBoard<CellVisual> _noselected = Transition.CreateBoardFromType<CellVisual>()
            .SetProperty(x => x.BorderBrush, Brushes.White)
            .SetProperty(x => x.BorderThickness, new Thickness(0))
            .SetParams((x) =>
            {
                x.Duration = 0.2;
            });

        private void CellBox_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BeginTransition(_selected);
        }
        private void CellBox_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BeginTransition(_noselected);
        }
    }
}
