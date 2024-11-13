using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Generalization
{
    public interface IExecutable
    {
        void Play(int index = 0);
        void Preview(int index = 0);
        void Pause();
        void Stop();
    }
}
