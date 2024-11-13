using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Visualization
{
    /// <summary>
    /// 可视化编辑细节信息单元
    /// </summary>
    public interface IVisualEditUnit
    {
        /// <summary>
        /// 段落标号
        /// </summary>
        int Paragraph { get; set; }

        /// <summary>
        /// 音轨标号
        /// </summary>
        int AudioTrack { get; set; }

        /// <summary>
        /// 行间顺序
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// 音符时值类型
        /// </summary>
        int NoteType { get; set; }
    }
}
