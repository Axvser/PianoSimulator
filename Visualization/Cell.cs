using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WindowsInput.Native;
using PianoSimulator.BasicService;

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
    }
}
