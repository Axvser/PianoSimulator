using PianoSimulator.BasicService;
using PianoSimulator.EditVisualComponent;
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

namespace PianoSimulator.EditPage
{
    [Navigable]
    public partial class VisualEdit : UserControl, IExecutable, IMusicUnitAggregation, IVisualEditUnitAggregation
    {
        public VisualEdit()
        {
            InitializeComponent();
            MusicTheory = MusicConfiguration.MusicTheory;
            Render();
        }

        private static TransitionBoard<TextBox> _selected = Transition.CreateBoardFromType<TextBox>()
            .SetProperty(x => x.BorderBrush, Brushes.Cyan)
            .SetProperty(x => x.Foreground, Brushes.Cyan)
            .SetParams((x) =>
            {
                x.Duration = 0.2;
            });
        private static TransitionBoard<TextBox> _noselected = Transition.CreateBoardFromType<TextBox>()
            .SetProperty(x => x.BorderBrush, Brushes.White)
            .SetProperty(x => x.Foreground, Brushes.White)
            .SetParams((x) =>
            {
                x.Duration = 0.2;
            });

        public List<IMusicUnit> Operation
        {
            get
            {
                List<IMusicUnit> result = new(Editors.Children.Count);
                foreach (TrackVisual child in Editors.Children)
                {
                    result = [.. result, .. child.Value];
                }
                return result;
            }
        }
        public string SongName { get; set; } = "default";
        public MusicTheory MusicTheory { get; set; } = new MusicTheory();

        public List<List<List<IVisualEditUnit>>> VisualEditUnit => throw new NotImplementedException();

        public void Render()
        {
            Section0.RenderTracks(MusicTheory);
            Section0.RenderTracks(MusicTheory);
            Section0.RenderTracks(MusicTheory);
            Section0.RenderTracks(MusicTheory);
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

        private void NameInput_MouseEnter(object sender, MouseEventArgs e)
        {
            NameInput.BeginTransition(_selected);
        }
        private void NameInput_MouseLeave(object sender, MouseEventArgs e)
        {
            NameInput.BeginTransition(_noselected);
            Keyboard.ClearFocus();
        }
    }
}
