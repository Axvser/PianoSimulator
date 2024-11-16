using PianoSimulator.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WindowsInput.Native;

namespace PianoSimulator.Generalization
{
    public class Note : IMusicUnit
    {
        public Note() { }

        public Note(char key, int span)
        {
            Key = key.ToVirtualKeyCode();
            Span = span;
        }

        public VirtualKeyCode Key { get; set; } = VirtualKeyCode.SPACE;
        public int Span { get; set; } = 0;

        public int Count => ConversionService.KeyCodeToChar.ContainsKey(Key) ? 1 : 0;
        public VirtualKeyCode[] Operation
        {
            get => [Key];
            set => Key = value.FirstOrDefault();
        }
        public int[] Duration
        {
            get => [Span];
            set => Span = value.FirstOrDefault();
        }
    }
}
