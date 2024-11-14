using PianoSimulator.BasicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace PianoSimulator.Generalization
{
    public class NormalFormData
    {
        public NormalFormData() { }
        public NormalFormData(IMusicUnitAggregation song)
        {
            Name = song.Name;
            Operations = song.Operation.Select(x => x.Operation).ToList();
            Durations = song.Operation.Select(x => x.Duration).ToList();
            SimulatorModes = song.Operation.Select(x => x.SimulatorMode).ToList();
        }
        public NormalFormData(string nks)
        {
            var data = StringService.NKS_ParseToNormalFormData(nks);
            Name = data.Name;
            Operations = data.Operations;
            Durations = data.Durations;
            SimulatorModes = data.SimulatorModes;
        }

        public string Name { get; set; } = string.Empty;
        public List<VirtualKeyCode[]> Operations { get; set; } = [];
        public List<int[]> Durations { get; set; } = [];
        public List<SimulatorModes[]> SimulatorModes { get; set; } = [];

        /// <summary>
        /// 转换为Song实例以执行播放
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Song ToSong()
        {
            if (Operations.Count == Durations.Count && Durations.Count == SimulatorModes.Count)
            {
                var song = new Song();
                song.Name = Name;
                for (int i = 0; i < Operations.Count; i++)
                {
                    if (Operations[i].Length == 1)
                    {
                        var note = new Note();
                        note.Key = Operations[i][0];
                        note.Span = Durations[i][0];
                        note.Mode = SimulatorModes[i][0];
                        song.Operation.Add(note);
                    }
                    else if (Operations[i].Length > 1 && Operations[i].Length == Durations[i].Length && Operations[i].Length == SimulatorModes[i].Length)
                    {
                        var chord = new Chord();
                        Note[] notes = new Note[Operations[i].Length];
                        for (int j = 0; j < Operations[i].Length; j++)
                        {
                            var unitnote = new Note();
                            unitnote.Key = Operations[i][j];
                            unitnote.Span = Durations[i][j];
                            unitnote.Mode = SimulatorModes[i][j];
                            notes[j] = unitnote;
                        }
                        chord.NewValue(notes);
                        song.Operation.Add(chord);
                    }
                }
                return song;
            }
            else
            {
                throw new InvalidOperationException($"NFD01 传入的数据存在全局性损坏\n操作描述符 {Operations.Count}\n持续描述符 {Durations.Count}\n模式描述符 {SimulatorModes.Count}\n以上三个参数应相同");
            }
        }

        /// <summary>
        /// 转换为NKS格式的字符串数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string ToNKS()
        {
            if (Operations.Count == Durations.Count && Durations.Count == SimulatorModes.Count)
            {
                var song = string.Empty;
                song += Name + " ";
                for (int i = 0; i < Operations.Count; i++)
                {
                    if (Operations[i].Length == 1)
                    {
                        var key = Operations[i][0];
                        var span = Durations[i][0];
                        song += key.ToString() + " " + span.ToString() + " ";
                    }
                    else if (Operations[i].Length > 1 && Operations[i].Length == Durations[i].Length && Operations[i].Length == SimulatorModes[i].Length)
                    {
                        var keys = string.Empty;
                        var span = 0;
                        for (int j = 0; j < Operations[i].Length; j++)
                        {
                            keys += Operations[i][j].ToString();
                            span = Durations[i][j];
                        }
                        song += keys + " " + span.ToString() + " ";
                    }
                }
                return song;
            }
            else
            {
                throw new InvalidOperationException($"NFD01 传入的数据存在全局性损坏\n操作描述符 {Operations.Count}\n持续描述符 {Durations.Count}\n模式描述符 {SimulatorModes.Count}\n以上三个参数应相同");
            }
        }
    }
}
