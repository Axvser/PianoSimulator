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
        public int CurrentIndex { get; private set; } = 0;

        public void Pause()
        {
            IsStop = IsRunning;
        }

        public async void Play(int index = 0)
        {
            if (IsRunning || IsStop) return;
            IsRunning = true;

            for (int i = index; i < Operation.Count; i++)
            {
                if (IsStop) return;
                await UnitExecutePaly(Operation[i]);
            }
        }

        public async void Preview(int index = 0)
        {
            if (IsRunning || IsStop) return;
            IsRunning = true;

            for (int i = index; i < Operation.Count; i++)
            {
                if (IsStop) return;
                await UnitExecutePreview(Operation[i]);
            }
        }

        public void Stop()
        {
            IsStop = IsRunning;
            CurrentIndex = 0;
        }

        private async Task UnitExecutePaly(IMusicUnit value)
        {
            MusicPlayService.Play(value.Operation);
            foreach (var item in GetWaitingSequence(value))
            {
                await Task.Delay(item.Item1);
                MusicPlayService.ReleasePlay(value.Operation[item.Item2]);
            }
        }
        private async Task UnitExecutePreview(IMusicUnit value)
        {
            MusicPlayService.Preview(value.Operation);
            foreach (var item in GetWaitingSequence(value))
            {
                await Task.Delay(item.Item1);
                MusicPlayService.ReleasePreview(value.Operation[item.Item2]);
            }
        }

        private static List<Tuple<int, int>> GetWaitingSequence(IMusicUnit value)
        {
            int[] result = new int[value.Operation.Length];
            for (int i = 0; i < value.SimulatorMode.Length; i++)
            {
                switch (value.SimulatorMode[i])
                {
                    case SimulatorModes.LongPress:
                        result[i] = value.Duration[i];
                        break;
                    default:
                        result[i] = 0;
                        break;
                }
            }
            return SequenceAggregation(result);
        }
        private static List<Tuple<int, int>> SequenceAggregation(int[] target)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            int currentTime = 0;

            List<Tuple<int, int>> link = [];
            for (int i = 0; i < target.Length; i++)
            {
                link.Add(Tuple.Create(target[i], i));
            }

            link.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            for (int i = 0; i < link.Count; i++)
            {
                var wait = link[i].Item1 - currentTime;
                result.Add(Tuple.Create(wait, link[i].Item2));
                currentTime += wait;
            }

            return result;
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
