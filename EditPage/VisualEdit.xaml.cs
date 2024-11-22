using PianoSimulator.BasicService;
using PianoSimulator.EditVisualComponent;
using PianoSimulator.Generalization;
using PianoSimulator.Visualization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PianoSimulator.EditPage
{
    [Navigable]
    public partial class VisualEdit : UserControl, IExecutable, IMusicUnitAggregation, IVisualEditUnitAggregation
    {
        public VisualEdit()
        {
            InitializeComponent();
            MusicTheory = MusicConfiguration.MusicTheory;
            Instance = this;
        }

        private static bool _isrendering = false;
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

        public static VisualEdit? Instance { get; set; }
        public List<IMusicUnit> Operation
        {
            get
            {
                List<IMusicUnit> result = new(Editors.Children.Count);
                foreach (SectionVisual child in Editors.Children)
                {
                    result = [.. result, .. child.Value];
                }
                return result;
            }
        }
        public string SongName { get; set; } = "default";
        public MusicTheory MusicTheory { get; set; } = new MusicTheory();

        public IVisualEditUnit[] VisualEditUnit
        {
            get
            {
                List<IVisualEditUnit> result = [];



                return [.. result];
            }
        }

        public static void Scroll(Thickness value)
        {
            if (Instance != null)
            {
                var horizontal = Instance.Table.HorizontalOffset;
                var newHorizontal = horizontal - value.Left + value.Right;
                Instance.Table.ScrollToHorizontalOffset(newHorizontal);
                SectionVisual.Selected?.Scroll(value);
            }
        }

        public void Render()
        {
            MainWindow.Lock(() =>
            {
                StateMachine.MachinePool.Clear();
                _isrendering = true;
                Section0.RenderTracks(MusicTheory);
                Section1.RenderTracks(MusicTheory);
                Section2.RenderTracks(MusicTheory);
                Section3.RenderTracks(MusicTheory);
                MainWindow.UnLock(() => { _isrendering = false; });
            });
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

        private void CreateNew_Click(object sender, MouseButtonEventArgs e)
        {
            if (Notification.Select("确定要新建简谱吗,这将清空当前区域！", "⚠ 警告", "是", "否"))
            {
                Render();
            }
        }
    }
}
