using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PianoSimulator.TextAnalizeService
{
    public static class StringAnalize
    {
        private static int _blankspace = 187;
        private static int re_blankspace = 10;
        private static double _cutrate = 0.5;
        private static int _cutstartposition = 3;

        /// <summary>
        /// 若两音符间无空格，则应用此时值
        /// </summary>
        public static int BlankSpace
        {
            get { return _blankspace; }
            set
            {
                if (value > 0 && value < 600)
                {
                    _blankspace = value;
                }
            }
        }
        /// <summary>
        /// 回调模式下，时值增减的具体值
        /// </summary>
        public static int BlankSpace_Re
        {
            get { return re_blankspace; }
            set
            {
                if (value > 0 && value < 100)
                {
                    re_blankspace = value;
                }
            }
        }
        /// <summary>
        /// 当键盘谱空格过多时，时值的递增按此值削减
        /// </summary>
        public static int CutStartPosition
        {
            get { return _cutstartposition; }
            set
            {
                if (value >= 2)
                {
                    _cutstartposition = value;
                }
            }
        }
        /// <summary>
        /// 从(CutStartPosition+1)开始的空格，添加的间隔值将被削减
        /// </summary>
        public static double CutRate
        {
            get { return _cutrate; }
            set
            {
                if (value >= 0)
                {
                    _cutrate = value;
                }
            }
        }

       
    }
}
