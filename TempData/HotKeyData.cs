using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.TempData
{
    public struct HotKeyData
    {
        public HotKeyData() { }

        public int ID { get; set; } = -1;
        public uint Left { get; set; } = 0;
        public NormalKeys Right { get; set; } = 0;

        public void Apply(HotKeyEventHandler handler)
        {
            GlobalHotKey.Add(Left, Right, handler);
        }
    }
}
