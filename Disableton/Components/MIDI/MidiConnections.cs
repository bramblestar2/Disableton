using RtMidi.Core;
using RtMidi.Core.Devices;
using RtMidi.Core.Devices.Infos;
using System.Collections.Generic;

namespace Disableton.Components.MIDI
{
    static public class MidiConnections
    {
        static public List<IMidiInputDeviceInfo>? MidiInputDevices
        {
            get => _midiInputDevices;
            private set
            {
                _midiInputDevices = value;
            }
        }

        static private List<IMidiInputDeviceInfo>? _midiInputDevices = null;


        static public void ReloadConnections()
        {
            MidiInputDevices = new List<IMidiInputDeviceInfo>(MidiDeviceManager.Default.InputDevices);
        }
    }
}
