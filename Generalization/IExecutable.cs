using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Generalization
{
    /// <summary>
    /// 选择模拟按键操作/音频预览
    /// </summary>
    public enum ExecutionModes
    {
        Keyboard,
        Audio
    }

    /// <summary>
    /// 可播放对象
    /// </summary>
    public interface IExecutable
    {
        /// <summary>
        /// 按键模拟
        /// </summary>
        void Play();

        /// <summary>
        /// 音频预览
        /// </summary>
        void Preview();

        /// <summary>
        /// 暂停
        /// </summary>
        void Pause();

        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
    }
}
