using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.BasicService
{
    public class MusicTheory
    {
        public MusicTheory() { }

        private int _speed = 80;
        private int _leftnum = 4;
        private int _rightnum = 4;
        private int _basicvalue = 3000;

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed
        {
            get { return _speed; }
            set
            {
                if (value > 0)
                {
                    _speed = value;
                }
                else
                {
                    _speed = 80;
                }
                ReCalculateSets();
            }
        }

        /// <summary>
        /// 每小节拍数
        /// </summary>
        public int LeftNum
        {
            get { return _leftnum; }
            set
            {
                if (value > 0)
                {
                    _leftnum = value;
                }
                else
                {
                    _leftnum = 4;
                }
                ReCalculateSets();
            }
        }

        /// <summary>
        /// 几分音符为一拍
        /// </summary>
        public int RightNum
        {
            get { return _rightnum; }
            set
            {
                if (value > 0)
                {
                    _rightnum = value;
                }
                else
                {
                    _rightnum = 4;
                }
            }
        }

        /// <summary>
        /// 基于此值获取不同音符的绝对时值
        /// </summary>     
        public int BasicValue
        {
            get { return _basicvalue; }
            set
            {
                if (value > 0)
                {
                    _basicvalue = value;
                    return;
                }
                _basicvalue = 3000;
            }
        }

        public int WholeNote { get { return _basicvalue; } }
        public int HalfNote { get { return _basicvalue / 2; } }
        public int QuarterNote { get { return _basicvalue / 4; } }
        public int EighthNote { get { return _basicvalue / 8; } }
        public int SixteenthNote { get { return _basicvalue / 16; } }
        public int ThirtySecondNote { get { return _basicvalue / 32; } }
        public int SixtyFourthNote { get { return _basicvalue / 64; } }
        public int FastNote { get; set; } = 100;

        private void ReCalculateSets()
        {
            _basicvalue = (int)((60000f / Speed) * LeftNum);
        }
    }
}
