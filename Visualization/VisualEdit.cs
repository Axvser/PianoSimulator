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
    public class VisualEdit : IMusicUnitAggregation, IVisualEditUnitAggregation, IExecutable
    {
        public VisualEdit() { }

        public List<Section> Sections { get; set; } = [];

        public string Name { get; set; } = string.Empty;
        public List<IMusicUnit> Operation { get; set; }

        public MusicTheory MusicTheory { get; set; } = new MusicTheory();
        public List<List<List<IVisualEditUnit>>> VisualEditUnit
        {
            get
            {
                List<List<List<IVisualEditUnit>>> result = [];
                for (int i = 0; i < Sections.Count; i++)
                {
                    result.Add([]);
                    for (int j = 0; j < Sections[i].Tracks.Count; j++)
                    {
                        result[i].Add([]);
                        for (int k = 0; k < Sections[i].Tracks[j].Cells.Count; k++)
                        {
                            result[i][j].Add(Sections[i].Tracks[j].Cells[k]);
                        }
                    }
                }
                return result;
            }
        }

        public void Pause()
        {

        }
        public void Play()
        {

        }
        public void Preview()
        {

        }
        public void Stop()
        {

        }
    }
}
