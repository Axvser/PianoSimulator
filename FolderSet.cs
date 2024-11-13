using MinimalisticWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator
{
    public static class FolderSet
    {
        public static string Meta { get; private set; } = nameof(Meta).CreatFolder();
        public static string Audio { get; private set; } = nameof(Audio).CreatFolder(Meta);
        public static string Generalization { get; private set; } = nameof(Generalization).CreatFolder(Meta);
        public static string VisualEdit { get; private set; } = nameof(VisualEdit).CreatFolder(Meta);
    }
}
