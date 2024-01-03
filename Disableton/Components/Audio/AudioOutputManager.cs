using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disableton.Components.Audio
{
    static public class AudioOutputManager
    {
        static public List<DirectSoundDeviceInfo>? Devices
        {
            get => _devices;
            private set => _devices = value;
        }

        static public DirectSoundOut? SelectedDevice
        {
            get => _selectedDevice;
            private set => _selectedDevice = value;
        }

        static private List<DirectSoundDeviceInfo>? _devices;
        static private DirectSoundOut? _selectedDevice;

        static public void ReloadSpeakersList()
        {
            Devices = new List<DirectSoundDeviceInfo>(DirectSoundOut.Devices);
        }
    }
}
