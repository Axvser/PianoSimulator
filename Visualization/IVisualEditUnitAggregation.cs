using PianoSimulator.Generalization;
using PianoSimulator.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Visualization
{
    public interface IVisualEditUnitAggregation : IMusicUnitAggregation
    {
        MusicTheory MusicTheory { get; set; }
        List<IVisualEditUnit> VisualEditUnit { get; }
    }
}
