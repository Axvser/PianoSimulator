using PianoSimulator.MusicBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Generalization
{
    /// <summary>
    /// 数据单元聚合体 , 拥有完整演奏数据序列且可以运行播放相关操作
    /// </summary>
    public interface IMusicUnitAggregation : IExecutable
    {
        string Name { get; set; }
        List<IMusicUnit> Operation { get; }
    }
}
