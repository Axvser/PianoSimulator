using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace PianoSimulator.Visualization
{
    /// <summary>
    /// 高级可视化编辑单元,包含单元的位置信息
    /// </summary>
    public interface IVisualEditUnit
    {
        /// <summary>
        /// 所在段落
        /// </summary>
        int Paragraph { get; set; }

        /// <summary>
        /// 所在音轨
        /// </summary>
        int Track { get; set; }

        /// <summary>
        /// 所在序列
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// 音符类型
        /// </summary>
        int NoteType { get; set; }
    }
}
