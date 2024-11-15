using PianoSimulator.BasicService;
using PianoSimulator.Generalization;
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
    public partial class TxtEdit : UserControl
    {
        public TxtEdit()
        {
            InitializeComponent();
        }

        private NormalFormData[] _normalformdatas = [];
        private TransitionBoard<TextBox> _selected = Transition.CreateBoardFromType<TextBox>()
            .SetProperty(x => x.BorderBrush, Brushes.Wheat)
            .SetProperty(x=>x.Foreground,Brushes.Lime)
            .SetParams((x) =>
            {
                x.Duration = 0.2;
            });
        private TransitionBoard<TextBox> _noselected = Transition.CreateBoardFromType<TextBox>()
            .SetProperty(x => x.BorderBrush, Brushes.White)
            .SetProperty(x => x.Foreground, Brushes.White)
            .SetParams((x) =>
            {
                x.Duration = 0.2;
            });

        public NormalFormData[] NormalFormDatas
        {
            get => _normalformdatas;
            set
            {
                _normalformdatas = value;
                RenderList();
            }
        }

        public NormalFormData? this[string name]
        {
            get => NormalFormDatas.FirstOrDefault(x => x.Name == name);
            set
            {
                var target = NormalFormDatas.FirstOrDefault(x => x.Name == name);
                if (target != null)
                {
                    target = value;
                }
            }
        }

        private void RenderList()
        {
            Options.Children.Clear();
            foreach (var data in _normalformdatas)
            {
                var option = new MButton();
                option.Text = data.Name;
                option.EdgeThickness = new Thickness(0, 0, 2, 0);
                option.WiseWidth = 810;
                option.WiseHeight = 40;
                option.Click += (s, e) =>
                {
                    NameInput.Text = data.Name;
                    var song = this[data.Name]?.ToSong();
                    if (MainWindow.Instance != null && song != null)
                    {
                        MainWindow.Instance.Actuator = song;
                    }
                };
                Options.Children.Add(option);
            }
        }

        private void NKS_Click(object sender, MouseButtonEventArgs e)
        {
            if (Notification.Select("这将从您的粘贴板获取NKS格式的乐曲数据,确认执行此操作吗?", "询问", "确定", "取消"))
            {
                var text = Clipboard.GetText();
                try
                {
                    NormalFormDatas = [StringService.NKS_ParseToNormalFormData(text)];
                }
                catch (InvalidOperationException)
                {
                    Notification.Message("数据存在格式错误", "⚠ 警告", "已知晓");
                }
            }
        }
        private void Keys_Click(object sender, MouseButtonEventArgs e)
        {
            if (Notification.Select("这将从您的粘贴板获取Keys格式的乐曲数据,确认执行此操作吗?", "询问", "确定", "取消"))
            {
                var text = Clipboard.GetText();
                try
                {
                    NormalFormDatas = [StringService.BiliZJ_ParseToNormalFormData(text)];
                }
                catch (InvalidOperationException)
                {
                    Notification.Message("数据存在格式错误", "⚠ 警告", "已知晓");
                }
            }
        }
        private void Json_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                NormalFormDatas = StringService.SelectJsonFiles().Select(x => x.JsonParse<NormalFormData>()).Where(x => x != null).ToArray();
            }
            catch
            {
                Notification.Message($"选中文件中存在异常个体,无法进行解析", "⚠ 警告", "已知晓");
            }
        }
        private void Txt_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                NormalFormDatas = StringService.SelectTxtFiles().Select(x => StringService.BiliZJ_ParseToNormalFormData(x)).ToArray();
            }
            catch (InvalidOperationException)
            {
                Notification.Message($"选中文件中存在异常个体,无法进行解析", "⚠ 警告", "已知晓");
            }
        }
        private void NameInput_MouseEnter(object sender, MouseEventArgs e)
        {
            NameInput.BeginTransition(_selected);
        }
        private void NameInput_MouseLeave(object sender, MouseEventArgs e)
        {
            NameInput.BeginTransition(_noselected);
        }
    }
}
