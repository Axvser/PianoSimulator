using PianoSimulator.Generalization;
using PianoSimulator.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Visualization
{
    /// <summary>
    /// 高级可视化编辑单元聚合体
    /// </summary>
    public interface IVisualEditUnitAggregation
    {
        MusicTheory MusicTheory { get; set; }
        List<List<List<IVisualEditUnit>>> VisualEditUnit { get; }
    }
}
