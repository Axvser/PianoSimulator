using PianoSimulator.Generalization;
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
    public partial class TxtTrackVisual : StackPanel
    {
        public TxtTrackVisual()
        {
            InitializeComponent();
        }

        private List<IMusicUnit> _value = [];
        public List<IMusicUnit> Value
        {
            get => _value;
            set
            {
                _value = value;
                RenderTrack();
            }
        }

        private void RenderTrack()
        {
            Children.Clear();
            foreach (var group in Value)
            {
                var tst = new TxtSingleVisual();
                tst.Value = group;
                Children.Add(tst);
            }
        }
    }
}
