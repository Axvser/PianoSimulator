using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace PianoSimulator.Generalization
{
    /// <summary>
    /// 允许用户选择长按/短按
    /// </summary>
    public enum SimulatorModes
    {
        LongPress,
        ShortPress
    }

    /// <summary>
    /// 音乐单元,包含模拟演奏所需要的基本数据
    /// </summary>
    public interface IMusicUnit
    {
        /// <summary>
        /// 按键数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 按键序列
        /// </summary>
        VirtualKeyCode[] Operation { get; set; }

        /// <summary>
        /// 持续时间序列
        /// </summary>
        int[] Duration { get; set; }
    }
}
