using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Generalization
{
    public class Song : IMusicUnitAggregation
    {
        public Song() { }
        public string Name { get; set; } = string.Empty;
        public List<IMusicUnit> Operation { get; set; } = [];

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void Preview(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
