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
        /// <summary>
        /// 使用速度、拍号等细节描述乐曲节奏
        /// </summary>
        MusicTheory MusicTheory { get; set; }

        /// <summary>
        /// 按照段落-音轨-音符的顺序访问音符单元
        /// </summary>
        IVisualEditUnit[] VisualEditUnit { get; }
    }
}
