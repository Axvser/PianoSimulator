using PianoSimulator.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Visualization
{
    internal class VisualFormData
    {
        public VisualFormData() { }
        public VisualFormData(IVisualEditUnitAggregation visualEdit) { Parse(visualEdit); }

        public MusicTheory MusicTheory { get; set; } = new MusicTheory();
        public List<List<List<int>>> Paragraphs { get; set; } = [[[]]];
        public List<List<List<int>>> AudioTracks { get; set; } = [[[]]];
        public List<List<List<int>>> Order { get; set; } = [[[]]];
        public List<List<List<int>>> NoteType { get; set; } = [[[]]];

        private void Parse(IVisualEditUnitAggregation visualEdit)
        {
            MusicTheory = visualEdit.MusicTheory;

        }
    }
}
