using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WindowsInput.Native;

namespace PianoSimulator.Generalization
{
    public class Chord : IMusicUnit
    {
        public Chord() { }
        public List<Note> Notes { get; set; } = [];

        [XmlIgnore]
        public VirtualKeyCode[] Operation
        {
            get => Notes.Select(x => x.Key).ToArray();
        }
        [XmlIgnore]
        public int[] Duration
        {
            get => Notes.Select(x => x.Duration[0]).ToArray();
        }
        [XmlIgnore]
        public SimulatorModes[] SimulatorMode { get; } = [SimulatorModes.AsynchronousPress];
    }
}
