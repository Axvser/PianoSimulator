global using FastHotKeyForWPF;
global using MinimalisticWPF;
using PianoSimulator.BasicService;
using PianoSimulator.EditPage;
using PianoSimulator.EditVisualComponent;
using PianoSimulator.Generalization;
using System;
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
        private int _scrolldelta = 30;
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
        public int ScrollDelta//按键滚动
        {
            get => _scrolldelta;
            set
            {
                _scrolldelta = Math.Abs(value);
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
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, Start);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, Pause);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F3, Stop);
            GlobalHotKey.Add(ModelKeys.SHIFT, NormalKeys.W, UpScroll);
            GlobalHotKey.Add(ModelKeys.SHIFT, NormalKeys.S, DownScroll);
            GlobalHotKey.Add(ModelKeys.SHIFT, NormalKeys.A, LeftScroll);
            GlobalHotKey.Add(ModelKeys.SHIFT, NormalKeys.D, RightScroll);
            GlobalHotKey.Add(ModelKeys.SHIFT, NormalKeys.X, AddTxtEditorSpan);
            GlobalHotKey.Add(ModelKeys.SHIFT, NormalKeys.Z, ReduceTxtEditorSpan);
            NowPage.Navigate(typeof(MusicConfiguration));
        }
        protected override void OnClosed(EventArgs e)
        {
            var time = DateTime.Now.ToString().Replace(' ', '-').Replace(':', '-').Replace('\\', '-').Replace('/', '-');
            GlobalHotKey.Destroy();
            Transition.Dispose();
            TxtEdit.SaveTemp(time);
            base.OnClosed(e);
        }

        public static void Lock(Action action)
        {
            Instance?.LoadingBlock.Open(action);
        }
        public static void UnLock(Action action)
        {
            Instance?.LoadingBlock.Close(action);
        }

        public void Start(object sender, HotKeyEventArgs e)
        {
            switch (ExecuteMode)
            {
                case ExecutionModes.Keyboard:
                    Actuator.Play();
                    break;
                case ExecutionModes.Audio:
                    Actuator.Preview();
                    break;
            }
        }
        public void Pause(object sender, HotKeyEventArgs e)
        {
            Actuator.Pause();
        }
        public void Stop(object sender, HotKeyEventArgs e)
        {
            Actuator.Stop();
        }
        public void RightScroll(object sender, HotKeyEventArgs e)
        {
            TxtEdit.Scroll(new Thickness(0, 0, ScrollDelta, 0));
        }
        public void LeftScroll(object sender, HotKeyEventArgs e)
        {
            TxtEdit.Scroll(new Thickness(ScrollDelta, 0, 0, 0));
        }
        public void UpScroll(object sender, HotKeyEventArgs e)
        {
            TxtEdit.Scroll(new Thickness(0, ScrollDelta, 0, 0));
        }
        public void DownScroll(object sender, HotKeyEventArgs e)
        {
            TxtEdit.Scroll(new Thickness(0, 0, 0, ScrollDelta));
        }
        public void AddTxtEditorSpan(object sender, HotKeyEventArgs e)
        {
            if (TxtSingleVisual.Selected != null)
            {
                TxtSingleVisual.Selected.Width += EditTime;
                var index = TxtSingleVisual.Selected.Value.Duration.Length - 1;
                if (index >= 0)
                {
                    var newSpan = TxtSingleVisual.Selected.Value.Duration[index] + EditTime;
                    int[] newTimes = new int[TxtSingleVisual.Selected.Value.Count];
                    for (int i = 0; i < TxtSingleVisual.Selected.Value.Count; i++)
                    {
                        newTimes[i] = newSpan;
                    }
                    TxtSingleVisual.Selected.Value.Duration = newTimes;
                }
            }
        }
        public void ReduceTxtEditorSpan(object sender, HotKeyEventArgs e)
        {
            if (TxtSingleVisual.Selected != null)
            {
                var index = TxtSingleVisual.Selected.Value.Duration.Length - 1;
                if (index >= 0)
                {
                    var newSpan = TxtSingleVisual.Selected.Value.Duration[index] - EditTime;
                    if (newSpan > 50)
                    {
                        int[] newTimes = new int[TxtSingleVisual.Selected.Value.Count];
                        for (int i = 0; i < TxtSingleVisual.Selected.Value.Count; i++)
                        {
                            newTimes[i] = newSpan;
                        }
                        TxtSingleVisual.Selected.Value.Duration = newTimes;
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