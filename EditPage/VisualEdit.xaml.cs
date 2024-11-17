﻿using PianoSimulator.Generalization;
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
    public partial class VisualEdit : UserControl, IExecutable
    {
        public VisualEdit()
        {
            InitializeComponent();
        }

        private TransitionBoard<TextBox> _selected = Transition.CreateBoardFromType<TextBox>()
            .SetProperty(x => x.BorderBrush, Brushes.Cyan)
            .SetProperty(x => x.Foreground, Brushes.Cyan)
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

        private void NameInput_MouseEnter(object sender, MouseEventArgs e)
        {
            NameInput.BeginTransition(_selected);
        }
        private void NameInput_MouseLeave(object sender, MouseEventArgs e)
        {
            NameInput.BeginTransition(_noselected);
            Keyboard.ClearFocus();
        }
    }
}
