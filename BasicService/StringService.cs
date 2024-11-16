using Microsoft.Win32;
using MinimalisticWPF;
using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static NormalFormData NKS_ParseToNormalFormData(string data, SimulatorModes simulatorModes = SimulatorModes.ShortPress)
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
                        modes.Add(simulatorModes);
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
        /// 将CKS数据转换为通用乐曲数据
        /// </summary>
        /// <remarks>
        /// CKS指代 无Name开头,仅以符号、字母、空格标记数据的格式
        /// </remarks>
        /// <param name="data">CKS数据</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static NormalFormData CKS_ParseToNormalFormData(string data, SimulatorModes simulatorModes = SimulatorModes.ShortPress, string name = "default")
        {
            Song result = new Song();
            result.Name = name;
            RecursivParse(data, false, 0, result, simulatorModes);
            return new NormalFormData(result);
        }

        /// <summary>
        /// 读取选中txt文件的文本内容
        /// </summary>
        public static string[] SelectTxtFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "选择文件（可多选）|*.txt";
            openFileDialog.Multiselect = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileNames.Select(x => File.ReadAllText(x)).ToArray();
            }
            return [];
        }
        /// <summary>
        /// 读取选中Json文件的文本内容
        /// </summary>
        public static string[] SelectJsonFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "选择文件（可多选）|*.json";
            openFileDialog.Multiselect = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileNames.Select(x => File.ReadAllText(x)).ToArray();
            }
            return [];
        }

        private static int _blankspace = 187;
        private static double _cutrate = 0.5;
        private static int _cutstartposition = 3;
        /// <summary>
        /// 若两音符间无空格，则应用此时值
        /// </summary>
        public static int BlankSpace
        {
            get { return _blankspace; }
            set
            {
                if (value > 0 && value < 600)
                {
                    _blankspace = value;
                }
            }
        }
        /// <summary>
        /// 当键盘谱空格过多时，时值的递增按此值削减
        /// </summary>
        public static int CutStartPosition
        {
            get { return _cutstartposition; }
            set
            {
                if (value >= 2)
                {
                    _cutstartposition = value;
                }
            }
        }
        /// <summary>
        /// 从(CutStartPosition+1)开始的空格，添加的间隔值将被削减
        /// </summary>
        public static double CutRate
        {
            get { return _cutrate; }
            set
            {
                if (value >= 0)
                {
                    _cutrate = value;
                }
            }
        }
        /// <summary>
        /// 筛选条件: 单音符、和弦、间断都将被囊括(用于初步筛选)
        /// </summary>
        private static string target1 { get; set; } = @"\([A-Z]+\)| |[A-Z]|[\[A-Z()\]]+";
        /// <summary>
        /// 筛选条件: 在连弹中只包含单个音符、和弦(用于递归解决连弹问题)
        /// </summary>
        private static string target2 { get; set; } = @"\([A-Z]+\)|[A-Z]";

        private static void RecursivParse(string text, bool isRecursiv, int value, Song Target, SimulatorModes simulatorModes)//解析按键谱
        {
            MatchCollection matches;//操作合集
            if (isRecursiv)
            {
                matches = Regex.Matches(text, target2);//若一个连弹操作被递归进来
            }
            else
            {
                matches = Regex.Matches(text, target1);//若是一般音符、和弦
            }

            for (int i = 0; i < matches.Count; i++)//遍历所有操作
            {
                if (isRecursiv)
                {
                    if (matches[i].Value.ToString()[0] >= 'A' && matches[i].Value.ToString()[0] <= 'Z')//若捕获到单个音符
                    {
                        if (i == matches.Count - 1)
                        {
                            Target.Operation.Add(new Note(matches[i].Value.ToString()[0], value, simulatorModes));
                        }
                        else
                        {
                            Target.Operation.Add(new Note(matches[i].Value.ToString()[0], BlankSpace / 4, simulatorModes));
                        }
                    }
                    else if (matches[i].Value.ToString()[0] == '(')//若获取到一个和弦
                    {
                        if (i == matches.Count - 1)
                        {
                            Target.Operation.Add(new Chord(matches[i].Value.ToString(), value, simulatorModes));
                        }
                        else
                        {
                            Target.Operation.Add(new Chord(matches[i].Value.ToString(), BlankSpace / 4, simulatorModes));
                        }
                    }
                }
                else
                {
                    int span = GetSpanFromSpaceNum(FindBlankSpaceNumber(matches, i));
                    if (matches[i].Value.ToString()[0] >= 'A' && matches[i].Value.ToString()[0] <= 'Z')//若捕获到单个音符
                    {
                        Target.Operation.Add(new Note(matches[i].Value.ToString()[0], span, simulatorModes));
                    }
                    else if (matches[i].Value.ToString()[0] == '(')//若获取到一个和弦
                    {
                        Target.Operation.Add(new Chord(matches[i].Value.ToString(), span, simulatorModes));
                    }
                    else if (matches[i].Value.ToString()[0] == '[')//连弹操作要进入递归
                    {
                        RecursivParse(matches[i].Value.ToString(), true, span, Target, simulatorModes);
                    }
                }
            }
        }
        private static int GetSpanFromSpaceNum(int num)//获取时值
        {
            int result = 0;

            if (num == 0)
            {
                result = BlankSpace / 2;
            }
            else if (num > 0 && num < CutStartPosition)
            {
                result = BlankSpace * num;
            }
            else if (num >= CutStartPosition)//空格数量很多时，间隔的叠加效率将参考CutRate
            {
                result = (BlankSpace * (CutStartPosition - 1)) + (int)(BlankSpace * CutRate * (num - CutStartPosition + 1));
            }

            return result;
        }
        private static int FindBlankSpaceNumber(MatchCollection matches, int start)//获取跟随空格数量
        {
            int result = 0;

            for (int i = start + 1; i < matches.Count; i++)
            {
                if (matches[i].Value.ToString()[0] == ' ')
                {
                    result++;
                }
                else
                {
                    break;
                }
            }

            return result;
        }
    }
}
