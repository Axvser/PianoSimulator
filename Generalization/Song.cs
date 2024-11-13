using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PianoSimulator.Generalization
{
    public class Song : IMusicUnitAggregation
    {
        public Song() { }
        public string Name { get; set; } = string.Empty;
        public List<IMusicUnit> Operation { get; set; } = [];

        public bool IsRunning { get; private set; } = false;
        public bool IsStop { get; private set; } = false;

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void Preview(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public static Song operator +(Song song1, Song song2)
        {
            var newSong = new Song();
            newSong.Operation = [.. song1.Operation, .. song2.Operation];
            return newSong;
        }
        public static Song operator -(Song song1, Song song2)
        {
            song1.Operation.RemoveSubList(song2.Operation);
            return song1;
        }
    }
}
