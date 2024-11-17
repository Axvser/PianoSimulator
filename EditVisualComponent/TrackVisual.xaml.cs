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

        public List<IMusicUnit> Value
        {
            get
            {
                List<IMusicUnit> result = new(Area.Children.Count);
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
    }
}
