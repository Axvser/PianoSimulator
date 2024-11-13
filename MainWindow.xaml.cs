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

        public Song Song { get; set; } = new Song();

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            GlobalHotKey.Awake();
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F1, TestA);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, TestB);
            GlobalHotKey.Add(ModelKeys.CTRL, NormalKeys.F2, TestC);
        }

        public void TestA(object sender, HotKeyEventArgs e)
        {
            var file = StringService.SelectTxtFiles().First();
            var song = StringService.NKS_ParseToNormalFormData(file).ToSong();
            Song = song;
        }

        public void TestB(object sender, HotKeyEventArgs e)
        {
            Song.Play();
        }
        public void TestC(object sender, HotKeyEventArgs e)
        {
            Song.Stop();
        }
    }
}