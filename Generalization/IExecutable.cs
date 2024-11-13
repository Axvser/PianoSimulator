using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Generalization
{
    public interface IExecutable
    {
        void Play();
        void Preview();
        void Pause();
        void Stop();
    }
}
