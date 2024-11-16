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
    /// <summary>
    /// CellVisual.xaml 的交互逻辑
    /// </summary>
    public partial class CellVisual : UserControl, IExecutable, IVisualEditUnit
    {
        public int Paragraph { get; set; } = 0;
        public int AudioTrack { get; set; } = 0;
        public int Order { get; set; } = 0;
        public int NoteType { get; set; } = 16;

        public CellVisual()
        {
            InitializeComponent();
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
    }
}
