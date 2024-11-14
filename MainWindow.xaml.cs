using FastHotKeyForWPF;
using MinimalisticWPF;
using PianoSimulator.BasicService;
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
            InitializeComponent();
        }

        private IExecutable _actuator = new Song();
        private ExecutionModes _executionModes = ExecutionModes.Keyboard;

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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, (sender, e) => { if (ExecuteMode == 0) Actuator.Play(); else Actuator.Preview(); });
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, (sender, e) => { Actuator.Pause(); });
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F3, (sender, e) => { Actuator.Stop(); });
        }
    }
}