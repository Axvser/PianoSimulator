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
        /// <summary>
        /// 曲名
        /// </summary>
        string SongName { get; set; }

        /// <summary>
        /// 操作合集
        /// </summary>
        List<IMusicUnit> Operation { get; }
    }
}
