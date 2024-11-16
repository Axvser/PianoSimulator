using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WindowsInput.Native;

namespace PianoSimulator.Generalization
{
    public class Chord : IMusicUnit
    {
        public Chord() { }

        public Chord(string keys, int span)
        {
            foreach (var key in keys)
            {
                if (key == '(' || key == ')') continue;
                var note = new Note(key, span);
                Notes.Add(note);
            }
        }

        public List<Note> Notes { get; set; } = [];

        public int Count => Notes.Count;
        public VirtualKeyCode[] Operation
        {
            get => Notes.Select(x => x.Key).ToArray();
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (i < Notes.Count)
                    {
                        Notes[i].Key = value[i];
                    }
                }
            }
        }
        public int[] Duration
        {
            get => Notes.Select(x => x.Duration[0]).ToArray();
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (i < Notes.Count)
                    {
                        Notes[i].Span = value[i];
                    }
                }
            }
        }

        public void AddValue(params Note[] notes)
        {
            Notes = [.. Notes, .. notes];
        }
        public void NewValue(params Note[] notes)
        {
            Notes = [.. notes];
        }
    }
}
