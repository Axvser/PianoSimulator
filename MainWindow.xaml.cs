global using FastHotKeyForWPF;
global using MinimalisticWPF;
using PianoSimulator.BasicService;
using PianoSimulator.EditPage;
using PianoSimulator.EditVisualComponent;
using PianoSimulator.Generalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PianoSimulator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
        }

        private IExecutable _actuator = new Song();
        private ExecutionModes _executionModes = ExecutionModes.Keyboard;
        private int _pagetype = 1;
        private int _edittime = 10;
        private static TransitionBoard<Border> _noselected = Transition.CreateBoardFromType<Border>()
            .SetProperty(x => x.Background, Brushes.Gray)
            .SetProperty(x => x.Opacity, 0.6)
            .SetParams((x) =>
            {
                x.Duration = 0.2;
            });
        private static TransitionBoard<Border> _selected = Transition.CreateBoardFromType<Border>()
            .SetProperty(x => x.Background, Brushes.Cyan)
            .SetProperty(x => x.Opacity, 1)
            .SetParams((x) =>
            {
                x.Duration = 0.5;
            });

        public static MainWindow? Instance { get; private set; }
        public IExecutable Actuator//执行器
        {
            get => _actuator;
            set
            {
                _actuator.Pause();
                _actuator = value;
            }
        }
        public ExecutionModes ExecuteMode//执行模式
        {
            get => _executionModes;
            set
            {
                Actuator.Pause();
                _executionModes = value;
            }
        }
        public int PageType//当前页面标号
        {
            get => _pagetype;
            set
            {
                if (value != _pagetype)
                {
                    PageLightChange(_pagetype, value);
                    _pagetype = value;
                }
            }
        }
        public int EditTime//时长缩进
        {
            get => _edittime;
            set
            {
                if (value > 0)
                {
                    _edittime = value;
                }
            }
        }

        private void PageLightChange(int last, int next)
        {
            switch (last)
            {
                case 1:
                    B1.BeginTransition(_noselected);
                    break;
                case 2:
                    B2.BeginTransition(_noselected);
                    break;
                case 3:
                    B3.BeginTransition(_noselected);
                    break;
                case 4:
                    B4.BeginTransition(_noselected);
                    break;
                case 5:
                    B5.BeginTransition(_noselected);
                    break;
                case 6:
                    B6.BeginTransition(_noselected);
                    break;
            }
            switch (next)
            {
                case 1:
                    B1.BeginTransition(_selected);
                    break;
                case 2:
                    B2.BeginTransition(_selected);
                    break;
                case 3:
                    B3.BeginTransition(_selected);
                    break;
                case 4:
                    B4.BeginTransition(_selected);
                    break;
                case 5:
                    B5.BeginTransition(_selected);
                    break;
                case 6:
                    B6.BeginTransition(_selected);
                    break;
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, (sender, e) => { if (ExecuteMode == 0) Actuator.Play(); else Actuator.Preview(); });
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, (sender, e) => { Actuator.Pause(); });
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F3, (sender, e) => { Actuator.Stop(); });
            GlobalHotKey.Add(ModelKeys.SHIFT, NormalKeys.X, AddTxtEditorSpan);
            GlobalHotKey.Add(ModelKeys.SHIFT, NormalKeys.Z, ReduceTxtEditorSpan);
            NowPage.Navigate(typeof(MusicConfiguration));
        }
        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();
            Transition.Dispose();
            base.OnClosed(e);
        }

        private void RightScroll(object sender, HotKeyEventArgs e)
        {

        }
        private void LeftScroll(object sender, HotKeyEventArgs e)
        {

        }
        private void TopScroll(object sender, HotKeyEventArgs e)
        {

        }
        private void BottomScroll(object sender, HotKeyEventArgs e)
        {

        }
        private void AddTxtEditorSpan(object sender, HotKeyEventArgs e)
        {
            if (TxtSingleVisual.Selected != null)
            {
                TxtSingleVisual.Selected.Width += EditTime;
                var index = TxtSingleVisual.Selected.Value.Duration.Length - 1;
                if (index >= 0)
                {
                    TxtSingleVisual.Selected.Value.Duration[index] += EditTime;
                }
            }
        }
        private void ReduceTxtEditorSpan(object sender, HotKeyEventArgs e)
        {
            if (TxtSingleVisual.Selected != null)
            {
                var index = TxtSingleVisual.Selected.Value.Duration.Length - 1;
                if (index >= 0)
                {
                    var newTime = TxtSingleVisual.Selected.Value.Duration[index] - EditTime;
                    if (newTime > 50)
                    {
                        TxtSingleVisual.Selected.Value.Duration[index] = newTime;
                        TxtSingleVisual.Selected.Width -= EditTime;
                    }
                }
            }
        }

        private void Config_Click(object sender, MouseButtonEventArgs e)
        {
            NowPage.Navigate(typeof(MusicConfiguration));
            PageType = 1;
        }
        private void TxtEdit_Click(object sender, MouseButtonEventArgs e)
        {
            NowPage.Navigate(typeof(TxtEdit));
            PageType = 2;
        }
        private void VisualEdit_Click(object sender, MouseButtonEventArgs e)
        {
            NowPage.Navigate(typeof(VisualEdit));
            PageType = 3;
        }
        private void Cover_Click(object sender, MouseButtonEventArgs e)
        {
            NowPage.Navigate(typeof(GameCover));
            PageType = 4;
        }
        private void IDESet_Click(object sender, MouseButtonEventArgs e)
        {
            NowPage.Navigate(typeof(IDESet));
            PageType = 5;
        }
        private void Help_Click(object sender, MouseButtonEventArgs e)
        {
            NowPage.Navigate(typeof(UserHelp));
            PageType = 6;
        }
    }
}