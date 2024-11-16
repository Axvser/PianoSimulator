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
    public partial class TxtSingleVisual : StackPanel, IExecutable, IVisualEditUnit
    {
        public int Paragraph { get; set; } = -1;
        public int AudioTrack { get; set; } = 0;
        public int Order { get; set; } = 0;
        public int NoteType { get; set; } = -1;

        public TxtSingleVisual()
        {
            InitializeComponent();
        }

        private IMusicUnit _musicUnit = new Note();
        private static TransitionBoard<Border> _selected = Transition.CreateBoardFromType<Border>()
            .SetProperty(x => x.Opacity, 0.1)
            .SetParams((x) =>
            {
                x.Duration = 0.4;
            });
        private static TransitionBoard<Border> _noselected = Transition.CreateBoardFromType<Border>()
            .SetProperty(x => x.Opacity, 0.01)
            .SetParams((x) =>
            {
                x.Duration = 0.2;
            });
     
        public IMusicUnit Value
        {
            get => _musicUnit;
            set
            {
                _musicUnit = value;
                RenderKeys();
            }
        }
        public static TxtSingleVisual? Selected { get; private set; }

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


        public void RenderAsLast()
        {
            Edge.BorderBrush = Brushes.Wheat;
            Edge.BorderThickness = new Thickness(0, 0, 3, 0);
        }
        private void RenderKeys()
        {
            Keys.Children.Clear();
            var data = BuildKeys(Value);
            foreach (var key in data.control)
            {
                Keys.Children.Add(key);
            }
            Width = 40 + data.span;
        }
        public static (List<MButton> control, int span) BuildKeys(IMusicUnit musicUnit)
        {
            List<MButton> result = [];

            for (int i = 0; i < musicUnit.Operation.Length; i++)
            {
                var mbt = new MButton()
                {
                    Text = musicUnit.Operation[i].ToChar().ToString(),
                    WiseHeight = 20,
                    WiseWidth = 40,
                    EdgeThickness = new Thickness(0),
                };
                result.Add(mbt);
            }
            var span = musicUnit.Duration?.LastOrDefault() ?? StringService.BlankSpace;

            return (result, span);
        }

        private void SpanLength_MouseEnter(object sender, MouseEventArgs e)
        {
            SpanLength.BeginTransition(_selected);
            Selected = this;
        }

        private void SpanLength_MouseLeave(object sender, MouseEventArgs e)
        {
            SpanLength.BeginTransition(_noselected);
            Selected = null;
        }
    }
}
