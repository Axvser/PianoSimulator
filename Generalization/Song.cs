using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using PianoSimulator.BasicService;

namespace PianoSimulator.Generalization
{
    public class Song : IMusicUnitAggregation, IExecutable
    {
        public Song() { }
        public string SongName { get; set; } = "default";
        public List<IMusicUnit> Operation { get; set; } = [];

        public bool IsRunning { get; private set; } = false;
        public bool IsStop { get; private set; } = false;
        public int CurrentIndex { get; private set; } = 0;

        public void Pause()
        {
            IsStop = IsRunning;
        }

        public async void Play()
        {
            if (IsRunning || IsStop) return;
            IsRunning = true;

            for (int i = CurrentIndex; i < Operation.Count; i++)
            {
                if (IsStop) return;
                await UnitExecutePaly(Operation[i]);
                CurrentIndex++;
            }

            IsRunning = false;
            IsStop = false;
            CurrentIndex = 0;
        }

        public async void Preview()
        {
            if (IsRunning || IsStop) return;
            IsRunning = true;

            for (int i = CurrentIndex; i < Operation.Count; i++)
            {
                if (IsStop) return;
                await UnitExecutePreview(Operation[i]);
                CurrentIndex++;
            }

            IsRunning = false;
            IsStop = false;
            CurrentIndex = 0;
        }

        public void Stop()
        {
            IsStop = IsRunning;
            CurrentIndex = 0;
        }

        public NormalFormData ToNormalFormData()
        {
            return new NormalFormData(this);
        }

        private static async Task UnitExecutePaly(IMusicUnit value)
        {
            MusicPlayService.Play(value.Operation);
            await Task.Delay(value.Duration.LastOrDefault());
        }
        private static async Task UnitExecutePreview(IMusicUnit value)
        {
            MusicPlayService.Preview(value.Operation);
            await Task.Delay(value.Duration.LastOrDefault());
            MusicPlayService.ReleasePreview(value.Operation);
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
