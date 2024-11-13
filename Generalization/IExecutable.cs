using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Generalization
{
    /// <summary>
    /// 可执行的乐曲对象
    /// </summary>
    public interface IExecutable
    {
        void Play(int index = 0);
        void Preview(int index = 0);
        void Pause();
        void Stop();
    }
}
