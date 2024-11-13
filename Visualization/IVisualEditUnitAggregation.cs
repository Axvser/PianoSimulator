using PianoSimulator.Generalization;
using PianoSimulator.MusicBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Visualization
{
    /// <summary>
    /// 可视化编辑单元的聚合体 , 拥有完整演奏与可视化数据序列且可以运行播放相关操作
    /// </summary>
    public interface IVisualEditUnitAggregation : IMusicUnitAggregation
    {
        MusicTheory MusicTheory { get; set; }
        List<IVisualEditUnit> VisualEditUnit { get; }
    }
}
