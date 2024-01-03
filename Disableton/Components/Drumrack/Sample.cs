using Disableton.Components.Audio;
using NAudio.Mixer;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;

namespace Disableton.Components.Drumrack
{
    public class Sample
    {
        public AudioFileReader? Audio
        {
            get => _audio;
            private set => _audio = value;
        }

        public float Volume
        {
            get => _volume;
            set => _volume = value;
        }

        private AudioFileReader? _audio;
        private OffsetSampleProvider? _offset;
        private WaveOutEvent? _output;
        private float _volume = 1.0f;

        #region Effects Variables

        private TimeSpan _start = TimeSpan.Zero;
        private TimeSpan _end   = TimeSpan.Zero;

        #endregion

        public Sample(string file)
        {
            if (File.Exists(file))
            {
                Audio = new AudioFileReader(file);
                Load();
            }
        }

        public Sample(string file, TimeSpan from, TimeSpan to)
        {
            if (File.Exists(file))
            {
                Audio = new AudioFileReader(file);
                _output = new WaveOutEvent();

                this.Trim(from, to);
                _start = from;
                _end = to;
            }
        }

        public void Trim(TimeSpan from, TimeSpan to)
        {
            _offset = new OffsetSampleProvider(Audio);
            _offset.SkipOver = from;
            _offset.Take = to - from;

            _output?.Stop();
            _output?.Init(_offset);
        }

        public void Play()
        {
            if (Audio != null)
            {
                if (_output != null)
                {
                    Audio.Seek(0, SeekOrigin.Begin);

                    Reset();

                    _output.Play();
                }
            }
        }

        public void Dispose()
        {
            _output?.Dispose();
            Audio?.Dispose();
        }

        private void Load()
        {
            _output = new WaveOutEvent();
            _output.Init(Audio);
        }

        private void Reset()
        {
            if (_start == TimeSpan.Zero && _end == TimeSpan.Zero) return;

            _offset = new OffsetSampleProvider(Audio.Skip(_start).Take(_end - _start));
            _output?.Dispose();
            _output?.Init(_offset);
        }
    }
}

