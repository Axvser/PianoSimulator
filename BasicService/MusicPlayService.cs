using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace PianoSimulator.BasicService
{
    public static class MusicPlayService
    {
        private static readonly InputSimulator Simulator = new InputSimulator();

        public static void Play(ICollection<VirtualKeyCode> keys)
        {
            foreach (var key in keys)
            {
                Simulator.Keyboard.KeyDown(key);
                Simulator.Keyboard.KeyUp(key);
            }
        }
        public static void ReleasePlay(ICollection<VirtualKeyCode> keys)
        {
            foreach (var key in keys)
            {
                Simulator.Keyboard.KeyUp(key);
            }
        }

        public static void Preview(ICollection<VirtualKeyCode> keys)
        {

        }
        public static void ReleasePreview(ICollection<VirtualKeyCode> keys)
        {

        }
    }
}
