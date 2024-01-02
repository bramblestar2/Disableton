using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

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

        #endregion

        public Sample(string file)
        {
            Audio = new AudioFileReader(file);
            _output = new WaveOutEvent();
            _offset = new OffsetSampleProvider(Audio);
            _output.Init(_offset);
        }

        public Sample(string file, TimeSpan from, TimeSpan to)
        {
            Audio = new AudioFileReader(file);
            _output = new WaveOutEvent();
            this.Trim(from, to);
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
                Audio.CurrentTime = new TimeSpan(0);
                if (_output != null)
                {
                    _output.Volume = Volume;
                    _output?.Play();
                }
            }
        }

        public void Dispose()
        {
            _output?.Dispose();
            Audio?.Dispose();
        }
    }
}
