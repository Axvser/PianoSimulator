using PianoSimulator.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator.Visualization
{
    public class VisualFormData
    {
        public VisualFormData() { }
        public VisualFormData(IVisualEditUnitAggregation visualEdit) { Parse(visualEdit); }

        public MusicTheory MusicTheory { get; set; } = new MusicTheory();
        public List<Tuple<int, int, int, int>> Value { get; set; } = [];

        private void Parse(IVisualEditUnitAggregation visualEdit)
        {
            MusicTheory = visualEdit.MusicTheory;
            Value.Clear();
            foreach (var unit in visualEdit.VisualEditUnit)
            {
                Value.Add(Tuple.Create(unit.Paragraph, unit.Track, unit.Order, unit.NoteType));
            }
        }
    }
}
