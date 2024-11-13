using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WindowsInput.Native;

namespace PianoSimulator.Visualization
{
    public class Cell : IVisualEditUnit
    {
        public Cell() { }

        public Note Note { get; set; } = new Note();

        public int Paragraph { get; set; } = 0;
        public int AudioTrack { get; set; } = 0;
        public int Order { get; set; } = 0;
        public int NoteType { get; set; } = 16;

        public void NewKey(VirtualKeyCode value)
        {
            Note.Key = value;
        }
        public void NewKey(char value)
        {
            Note.Key = value.ToVirtualKeyCode();
        }
        public void NewSpan(int value)
        {
            if (value > 0)
            {
                Note.Span = value;
            }
        }
        public void NewMode(SimulatorModes mode)
        {
            Note.Mode = mode;
        }
    }
}
