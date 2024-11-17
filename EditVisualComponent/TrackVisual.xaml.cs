using PianoSimulator.BasicService;
using PianoSimulator.Generalization;
using PianoSimulator.Visualization;
using System;
using System.Collections.Generic;
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

namespace PianoSimulator.EditVisualComponent
{
    /// <summary>
    /// TrackVisual.xaml 的交互逻辑
    /// </summary>
    public partial class TrackVisual : VisualUnitControlBase
    {
        public TrackVisual()
        {
            InitializeComponent();
        }

        public TrackVisual(MusicTheory musicTheory)
        {
            InitializeComponent();
            RenderCells(musicTheory);
        }

        private static TrackVisual? _selectedInstance = null;
        public static TrackVisual? Selected
        {
            get => _selectedInstance;
            set
            {
                _selectedInstance = value;
            }
        }
        public List<IMusicUnit> Value
        {
            get
            {
                List<IMusicUnit> result = new(Area.Children.Count);
                foreach (CellVisual child in Area.Children)
                {
                    result = [.. result, child.Value];
                }
                return result;
            }
        }

        public void RenderCells(MusicTheory theory)
        {
            Area.ColumnDefinitions.Clear();
            for (int i = 0; i < theory.Column; i++)
            {
                var definition = new ColumnDefinition();
                Area.ColumnDefinitions.Add(definition);
                var cell = new CellVisual();
                Area.Children.Add(cell);
                Grid.SetColumn(cell, i);
            }
        }

        private void VisualUnitControlBase_MouseEnter(object sender, MouseEventArgs e)
        {
            Selected = this;
        }
        private void VisualUnitControlBase_MouseLeave(object sender, MouseEventArgs e)
        {
            Selected = null;
        }
    }
}
