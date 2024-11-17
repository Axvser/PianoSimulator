using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PianoSimulator.BasicService
{
    public class MusicTheory
    {
        public MusicTheory() { }

        [XmlIgnore]
        [JsonIgnore]
        private int _speed = 80;
        [XmlIgnore]
        [JsonIgnore]
        private int _leftnum = 4;
        [XmlIgnore]
        [JsonIgnore]
        private int _rightnum = 4;
        [XmlIgnore]
        [JsonIgnore]
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

        [JsonIgnore]
        public int Column { get => (16 / RightNum) * LeftNum; }
        [JsonIgnore]
        public int WholeNote { get => _basicvalue; }
        [JsonIgnore]
        public int HalfNote { get => _basicvalue / 2; }
        [JsonIgnore]
        public int QuarterNote { get => _basicvalue / 4; }
        [JsonIgnore]
        public int EighthNote { get => _basicvalue / 8; }
        [JsonIgnore]
        public int SixteenthNote { get => _basicvalue / 16; }
        public int FastNote { get; set; } = 100;

        private void ReCalculateSets()
        {
            _basicvalue = (int)((60000f / Speed) * LeftNum);
        }
    }
}
