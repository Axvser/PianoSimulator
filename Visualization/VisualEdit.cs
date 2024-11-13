using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalisticWPF;
using PianoSimulator.Generalization;
using PianoSimulator.BasicService;

namespace PianoSimulator.Visualization
{
    public class VisualEdit : IVisualEditUnitAggregation
    {
        public VisualEdit() { }

        public List<Section> Sections { get; set; } = [];
             
        public MusicTheory MusicTheory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IVisualEditUnit> VisualEditUnit => throw new NotImplementedException();

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
