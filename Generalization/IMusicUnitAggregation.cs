using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Generalization
{
    /// <summary>
    /// 乐曲单元数据聚合体
    /// </summary>
    public interface IMusicUnitAggregation
    {
        string Name { get; set; }
        List<IMusicUnit> Operation { get; set; }
    }
}
