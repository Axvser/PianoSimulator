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
    /// SectionVisual.xaml 的交互逻辑
    /// </summary>
    public partial class SectionVisual : UserControl, IExecutable, IVisualEditUnit
    {
        public int Paragraph { get; set; } = -1;
        public int AudioTrack { get; set; } = -1;
        public int Order { get; set; } = 0;
        public int NoteType { get; set; } = -1;

        public SectionVisual()
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
