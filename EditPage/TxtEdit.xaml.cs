﻿using PianoSimulator.BasicService;
using PianoSimulator.EditVisualComponent;
using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PianoSimulator.EditPage
{
    [Navigable]
    public partial class TxtEdit : UserControl, IExecutable
    {
        public TxtEdit()
        {
            InitializeComponent();
            Instance = this;
        }

        public static TxtEdit? Instance { get; set; }

        private NormalFormData[] _normalformdatas = [];
        private TransitionBoard<TextBox> _selected = Transition.CreateBoardFromType<TextBox>()
            .SetProperty(x => x.BorderBrush, Brushes.Wheat)
            .SetProperty(x => x.Foreground, Brushes.Lime)
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
        private bool _isRendering = false;

        public NormalFormData[] Value
        {
            get => _normalformdatas;
            set
            {
                _normalformdatas = value;
                RenderSelectList();
            }
        }
        public NormalFormData? this[string name]
        {
            get => Value.FirstOrDefault(x => x.Name == name);
            set
            {
                var target = Value.FirstOrDefault(x => x.Name == name);
                if (target != null)
                {
                    target = value;
                }
            }
        }
        public Song? Selected { get; private set; }

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

        private void RenderSelectList()
        {
            Options.Children.Clear();
            var count = 1;
            foreach (var data in _normalformdatas)
            {
                var option = new MButton();
                option.Text = $"[{count}]  " + data.Name;
                option.EdgeThickness = new Thickness(0, 0, 2, 0);
                option.WiseWidth = 810;
                option.WiseHeight = 40;
                option.Click += (s, e) =>
                {
                    NameInput.Text = data.Name;
                    var song = this[data.Name]?.ToSong();
                    if (MainWindow.Instance != null && song != null)
                    {
                        Selected = song;
                        MainWindow.Instance.Actuator = this;
                        MainWindow.Lock(() =>
                        {
                            _ = Task.Run(() => RenderEditArea(song));
                        });
                    }
                };
                Options.Children.Add(option);
                count++;
            }
        }
        private void RenderEditArea(IMusicUnitAggregation data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Editors.Children.Clear();
                _isRendering = true;
            });
            List<List<IMusicUnit>> ValueGroups = [[]];
            var nowLength = 0;
            var nowRow = 0;
            foreach (var unit in data.Operation)
            {
                var span = unit.Duration?.LastOrDefault() ?? StringService.BlankSpace;
                if (nowLength + 40 + span > 810)
                {
                    ValueGroups.Add([]);
                    nowRow++;
                    nowLength = 0;
                }
                else
                {
                    nowLength += 40 + span;
                }
                ValueGroups[nowRow].Add(unit);
            }
            List<TxtTrackVisual> tracks = new List<TxtTrackVisual>(ValueGroups.Count);
            foreach (var group in ValueGroups)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var ttk = new TxtTrackVisual();
                    ttk.Value = group;
                    tracks.Add(ttk);
                });
            }
            foreach (var tack in tracks)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Editors.Children.Add(tack);
                }, DispatcherPriority.Render);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow.UnLock(() => { _isRendering = false; });
            }, DispatcherPriority.Background);
        }

        public static void Scroll(Thickness value)
        {
            if (Instance != null)
            {
                var horizontal = Instance.Table.HorizontalOffset;
                var vertical = Instance.Table.VerticalOffset;
                var newHorizontal = horizontal - value.Left + value.Right;
                var newVertical = vertical - value.Top + value.Bottom;
                Instance.Table.ScrollToHorizontalOffset(newHorizontal);
                Instance.Table.ScrollToVerticalOffset(newVertical);
            }
        }

        private void FromKeyBoard_Click(object sender, MouseButtonEventArgs e)
        {
            if (!Notification.Select("这将从粘贴板获取数据,是否继续?", "询问", "继续", "取消")) return;

            if (Notification.Select("选择数据格式", "询问", "NKS", "KB"))
            {
                var text = Clipboard.GetText();
                try
                {
                    Value = [StringService.NKS_ParseToNormalFormData(text)];
                }
                catch (InvalidOperationException)
                {
                    Notification.Message("数据存在格式错误", "⚠ 警告", "已知晓");
                }
            }
            else
            {
                var text = Clipboard.GetText();
                try
                {
                    Value = [StringService.CKS_ParseToNormalFormData(text)];
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
                Value = StringService.SelectJsonFiles().Select(x => x.JsonParse<NormalFormData>()).Where(x => x != null).ToArray();
            }
            catch
            {
                Notification.Message($"选中文件中存在异常个体,无法进行解析", "⚠ 警告", "已知晓");
            }
        }
        private void NKSTxt_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Value = StringService.SelectTxtFiles().Select(x => StringService.NKS_ParseToNormalFormData(x)).ToArray();
            }
            catch (InvalidOperationException)
            {
                Notification.Message($"选中文件中存在异常个体,无法进行解析", "⚠ 警告", "已知晓");
            }
        }
        private void CKSTxt_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Value = StringService.SelectTxtFiles().Select(x => StringService.CKS_ParseToNormalFormData(x)).ToArray();
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
        private void CreateNew_Click(object sender, MouseButtonEventArgs e)
        {

        }
        private void ReadData_Click(object sender, MouseButtonEventArgs e)
        {

        }
        private void Save_Click(object sender, MouseButtonEventArgs e)
        {

        }
        private void Refresh_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
