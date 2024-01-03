using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disableton.Components.Audio
{
    //idk if this will be used
    //I dont think i have this setup properly where nothing will be broken
    static public class AudioFileManager
    {
        static public Dictionary<string, RawSourceWaveStream> StoredStreams
        {
            get => _storedStreams;
            private set => _storedStreams = value;
        }

        //Key is File Path
        static private Dictionary<string, RawSourceWaveStream> _storedStreams = new Dictionary<string, RawSourceWaveStream>();

        //1: File successfully added
        //0: File already exists
        //-1: File wasn't added
        static public int AddAudio(string filePath)
        {
            if (Exists(filePath))
                return 0;

            if (File.Exists(filePath))
            {
                AudioFileReader audioFileReader = new AudioFileReader(filePath);
                StoredStreams[filePath] = new RawSourceWaveStream(audioFileReader, audioFileReader.WaveFormat);
                return 1;
            }

            return -1;
        }

        static public bool RemoveAudio(string filePath)
        {
            if (StoredStreams.ContainsKey(filePath))
            {
                StoredStreams[filePath].Dispose();
                return StoredStreams.Remove(filePath);
            }

            return false;
        }

        static public bool Exists(string filePath)
        {
            return StoredStreams.ContainsKey(filePath);
        }

        static public void Clear()
        {
            foreach (var stream in StoredStreams)
            {
                stream.Value.Dispose();
            }

            StoredStreams.Clear();
        }
    }
}
