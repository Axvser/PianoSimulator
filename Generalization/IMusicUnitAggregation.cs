using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Generalization
{
    public interface IMusicUnitAggregation
    {
        string Name { get; set; }
        List<IMusicUnit> Operation { get; set; }
    }
}
