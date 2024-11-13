using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace PianoSimulator.Generalization
{
    public enum SimulatorModes
    {
        LongPress,
        ShortPress,
        Ignore,
    }

    public interface IMusicUnit
    {
        VirtualKeyCode[] Operation { get; set; }
        int[] Duration { get; set; }
        SimulatorModes[] SimulatorMode { get; set; }
    }
}
