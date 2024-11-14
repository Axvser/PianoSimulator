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

        public NormalFormData[] NormalFormDatas { get; set; } = [];

        private void NKS_Click(object sender, MouseButtonEventArgs e)
        {
            if (Notification.Select("这将从您的粘贴板获取NKS格式的乐曲数据,确认执行此操作吗?", "询问", "确定", "取消"))
            {

            }
        }

        private void Keys_Click(object sender, MouseButtonEventArgs e)
        {
            if (Notification.Select("这将从您的粘贴板获取Keys格式的乐曲数据,确认执行此操作吗?", "询问", "确定", "取消"))
            {

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
                Notification.Message($"选中文件中存在异常,无法进行解析", "⚠ 警告", "已知晓");
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
                Notification.Message($"选中文件中存在异常,无法进行解析", "⚠ 警告", "已知晓");
            }
        }
    }
}
