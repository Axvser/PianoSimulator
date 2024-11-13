using Microsoft.Win32;
using MinimalisticWPF;
using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsInput.Native;

namespace PianoSimulator.BasicService
{
    public static class StringService
    {
        /// <summary>
        /// 将NKS数据转换为通用乐曲数据
        /// </summary>
        /// <remarks>
        /// NKS指代 [Name] [Keys] [Span] …… 格式的乐曲数据
        /// </remarks>
        /// <param name="data">NKS数据</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static NormalFormData ParseNKSToNormalFormData(string data)
        {
            var values = data.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var contentLength = values.Length - 1;
            if (contentLength % 2 == 0 && contentLength != 0)
            {
                var index = 1;
                NormalFormData result = new NormalFormData();
                result.Name = values[0];
                do
                {
                    var next = index + 1;
                    List<VirtualKeyCode> keys = [];
                    List<int> spans = [];
                    List<SimulatorModes> modes = [];
                    foreach (var key in values[index])
                    {
                        keys.Add(key.ToVirtualKeyCode());
                        spans.Add(values[next].ToInt() ?? 0);
                        modes.Add(SimulatorModes.LongPress);
                    }
                    result.Operations.Add([.. keys]);
                    result.Durations.Add([.. spans]);
                    result.SimulatorModes.Add([.. modes]);
                    index += 2;
                }
                while (index + 1 < values.Length);
                return result;
            }
            else
            {
                throw new InvalidOperationException("SS01 你在尝试读取 Name Keys Time Keys Time …… 结构的乐曲数据时 , 给定的数据实际为空或格式错误");
            }
        }

        /// <summary>
        /// 读取选中txt文件的文本内容
        /// </summary>
        public static string[] SelectTxtFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.Multiselect = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileNames.Select(x => File.ReadAllText(x)).ToArray();
            }
            return [];
        }
    }
}
