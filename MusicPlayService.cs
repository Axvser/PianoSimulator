using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace PianoSimulator
{
    public static class MusicPlayService
    {
        private static readonly InputSimulator Simulator = new InputSimulator();

        public static void Play(ICollection<VirtualKeyCode> keys)
        {
            foreach (var key in keys)
            {
                Simulator.Keyboard.KeyDown(key);
            }
        }

        public static void ReleasePlay(VirtualKeyCode key)
        {
            Simulator.Keyboard.KeyUp(key);
        }

        public static void Preview(ICollection<VirtualKeyCode> keys)
        {

        }

        public static void ReleasePreview(VirtualKeyCode key)
        {

        }
    }
}
