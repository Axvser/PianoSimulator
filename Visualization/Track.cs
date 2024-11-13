﻿using PianoSimulator.Generalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WindowsInput.Native;

namespace PianoSimulator.Visualization
{
    public class Track : IVisualEditUnit
    {
        public Track() { }

        public List<Cell> Cells { get; set; } = [];

        public int Paragraph { get; set; } = 0;
        public int AudioTrack { get; set; } = -1;
        public int Order { get; set; } = 0;
        public int NoteType { get; set; } = -1;       
    }
}
