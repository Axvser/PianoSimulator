using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Visualization
{
    public interface IVisualEditUnit
    {
        int Paragraph { get; set; }
        int AudioTrack { get; set; }
        int Order { get; set; }
        int NoteType { get; set; }
    }
}
