using MinimalisticWPF;
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
            Save();
        }

        public void Save()
        {
            var song = new Song();
            song.Name = "转换用例";
            song.Operation.Add(new Note());
            song.Operation.Add(new Note());
            song.Operation.Add(new Chord());

            var data = new NormalFormData(song);
            data.Name.CreatJsonFile(FolderSet.Generalization, data);

            var result = System.IO.File.ReadAllText(System.IO.Path.Combine(FolderSet.Generalization, data.Name + ".json")).JsonParse<NormalFormData>().ToSong();
            result.Name = "反转结果";
            var data2 = new NormalFormData(result);
            data2.Name.CreatJsonFile(FolderSet.Generalization, data2);
        }
    }
}