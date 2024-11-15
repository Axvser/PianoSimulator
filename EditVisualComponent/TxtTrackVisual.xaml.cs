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
    public partial class TxtTrackVisual : StackPanel, IExecutable
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

        public void Play()
        {

        }
        public void Preview()
        {

        }
        public void Pause()
        {

        }
        public void Stop()
        {

        }

        private void RenderTrack()
        {
            Children.Clear();
            for (int i = 0; i < Value.Count; i++)
            {
                var tst = new TxtSingleVisual();
                tst.Value = Value[i];
                if (i == Value.Count - 1)
                {
                    tst.RenderAsLast();
                }
                Children.Add(tst);
            }
        }
    }
}
