using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Visualization
{
    /// <summary>
    /// 高级可视化编辑单元,包含单元的位置信息
    /// </summary>
    public interface IVisualEditUnit
    {
        int Paragraph { get; set; }
        int AudioTrack { get; set; }
        int Order { get; set; }
        int NoteType { get; set; }
    }
}
