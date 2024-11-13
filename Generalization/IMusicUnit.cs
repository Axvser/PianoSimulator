using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace PianoSimulator.Generalization
{
    /// <summary>
    /// 按压方式
    /// </summary>
    public enum SimulatorModes
    {
        LongPress,
        ShortPress,
        AsynchronousPress,
        Ignore,
    }

    /// <summary>
    /// 时刻对应音符信息
    /// </summary>
    public interface IMusicUnit
    {
        VirtualKeyCode[] Operation { get; }
        int[] Duration { get; }
        SimulatorModes[] SimulatorMode { get; }
    }
}
