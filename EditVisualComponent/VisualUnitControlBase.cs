using PianoSimulator.BasicService;
using PianoSimulator.Visualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PianoSimulator.EditVisualComponent
{
    public class VisualUnitControlBase : UserControl, IVisualEditUnit
    {
        public int Paragraph
        {
            get { return (int)GetValue(ParagraphProperty); }
            set { SetValue(ParagraphProperty, value); }
        }
        public int Track
        {
            get { return (int)GetValue(AudioTrackProperty); }
            set { SetValue(AudioTrackProperty, value); }
        }
        public int Order
        {
            get { return (int)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }
        public int NoteType
        {
            get { return (int)GetValue(NoteTypeProperty); }
            set { SetValue(NoteTypeProperty, value); }
        }

        public static readonly DependencyProperty ParagraphProperty =
            DependencyProperty.Register("Paragraph", typeof(int), typeof(VisualUnitControlBase), new PropertyMetadata(0));
        public static readonly DependencyProperty AudioTrackProperty =
            DependencyProperty.Register("Track", typeof(int), typeof(VisualUnitControlBase), new PropertyMetadata(0));
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(int), typeof(VisualUnitControlBase), new PropertyMetadata(0));
        public static readonly DependencyProperty NoteTypeProperty =
            DependencyProperty.Register("NoteType", typeof(int), typeof(VisualUnitControlBase), new PropertyMetadata(0));

        public virtual void Play()
        {

        }
        public virtual void Preview()
        {

        }
        public virtual void Pause()
        {

        }
        public virtual void Stop()
        {

        }
    }
}
