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
    public partial class WhileLoading : UserControl
    {
        public WhileLoading()
        {
            InitializeComponent();
        }

        private static TransitionBoard<WhileLoading> _selected = Transition.CreateBoardFromType<WhileLoading>()
            .SetProperty(x => x.Opacity, 1);
        private static TransitionBoard<WhileLoading> _noselected = Transition.CreateBoardFromType<WhileLoading>()
            .SetProperty(x => x.Opacity, 0);

        public void Open(Action action)
        {
            _selected.SetParams((x) =>
            {
                x.Duration = 0.5;
                x.Start = () =>
                {
                    Width = 960;
                };
                x.Completed = action;
            });
            this.BeginTransition(_selected);
        }
        public void Close(Action action)
        {
            _noselected.SetParams((x) =>
            {
                x.Duration = 1;
                x.Completed = () =>
                {
                    action.Invoke();
                    Width = 0;
                };
            });
            this.BeginTransition(_noselected);
        }
    }
}
