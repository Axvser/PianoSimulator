using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace PianoSimulator.BasicService
{
    public static class ConversionService
    {
        public static readonly Dictionary<char, VirtualKeyCode> CharToKeyCode = new Dictionary<char, VirtualKeyCode>//操作 → 虚拟按键
        {
        {'Q', VirtualKeyCode.VK_Q},
        {'W', VirtualKeyCode.VK_W},
        {'E', VirtualKeyCode.VK_E},
        {'R', VirtualKeyCode.VK_R},
        {'T', VirtualKeyCode.VK_T},
        {'Y', VirtualKeyCode.VK_Y},
        {'U', VirtualKeyCode.VK_U},
        {'I', VirtualKeyCode.VK_I},
        {'O', VirtualKeyCode.VK_O},
        {'P', VirtualKeyCode.VK_P},

        {'A', VirtualKeyCode.VK_A},
        {'S', VirtualKeyCode.VK_S},
        {'D', VirtualKeyCode.VK_D},
        {'F', VirtualKeyCode.VK_F},
        {'G', VirtualKeyCode.VK_G},
        {'H', VirtualKeyCode.VK_H},
        {'J', VirtualKeyCode.VK_J},
        {'K', VirtualKeyCode.VK_K},
        {'L', VirtualKeyCode.VK_L},

        {'Z', VirtualKeyCode.VK_Z},
        {'X', VirtualKeyCode.VK_X},
        {'C', VirtualKeyCode.VK_C},
        {'V', VirtualKeyCode.VK_V},
        {'B', VirtualKeyCode.VK_B},
        {'N', VirtualKeyCode.VK_N},
        {'M', VirtualKeyCode.VK_M},
        };
        public static readonly Dictionary<VirtualKeyCode, char> KeyCodeToChar = CharToKeyCode.ToDictionary(x => x.Value, x => x.Key);

        public static VirtualKeyCode ToVirtualKeyCode(this char source)
        {
            var upper = char.ToUpper(source);
            if (CharToKeyCode.TryGetValue(upper, out var value))
            {
                return value;
            }
            else
            {
                throw new ArgumentException($"KCS01 字符 {upper} 不受项目支持");
            }
        }
        public static char ToChar(this VirtualKeyCode source)
        {
            if (KeyCodeToChar.TryGetValue(source, out var value))
            {
                return value;
            }
            else
            {
                throw new ArgumentException($"KCS02 按键码 {source} 不受项目支持");
            }
        }
        public static int ToSpan(this int source, MusicTheory musicTheory)
        {
            switch (source)
            {
                case 1:
                    return musicTheory.WholeNote;
                case 2:
                    return musicTheory.HalfNote;
                case 4:
                    return musicTheory.QuarterNote;
                case 8:
                    return musicTheory.EighthNote;
                case 16:
                    return musicTheory.SixteenthNote;
                case 32:
                    return musicTheory.ThirtySecondNote;
                case 64:
                    return musicTheory.SixtyFourthNote;
                default:
                    return musicTheory.FastNote;
            }
        }
        public static Note ToNote(this IMusicUnit unit)
        {
            var key = unit.Operation.FirstOrDefault();
            var span = unit.Duration.FirstOrDefault();
            if (key == default || span == default) throw new ArgumentException($"CONS01 未能将单元换算为Note实例");
            var result = new Note();
            result.Key = key;
            result.Duration[0] = span;
            return result;
        }
        public static Chord ToChord(this IMusicUnit unit)
        {
            var keys = unit.Operation;
            var spans = unit.Duration;
            if (keys.Length != spans.Length) throw new ArgumentException($"CONS02 未能将单元换算为Chord实例");
            var result = new Chord();
            for (int i = 0; i < spans.Length; i++)
            {
                var key = keys[i];
                var span = spans[i];
                var note = new Note();
                note.Key = key;
                note.Duration[0] = span;
                result.Notes.Add(note);
            }
            return result;
        }
    }
}
