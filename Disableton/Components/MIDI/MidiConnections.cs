using DynamicData;
using RtMidi.Core;
using RtMidi.Core.Devices.Infos;
using System.Collections.ObjectModel;
using System.Linq;

namespace Disableton.Components.MIDI
{
    

    static public class MidiConnections
    {
        static public ObservableCollection<IMidiInputDeviceInfo> MidiInputDevices
        {
            get => _midiInputDevices;
            private set
            {
                _midiInputDevices = value;
            }
        }

        static private ObservableCollection<IMidiInputDeviceInfo> _midiInputDevices = new ObservableCollection<IMidiInputDeviceInfo>();


        static public bool ReloadConnections()
        {
            if (MidiInputDevices.Count == MidiDeviceManager.Default.InputDevices.Count()) return false;

            MidiInputDevices.Clear();
            MidiInputDevices.AddRange(MidiDeviceManager.Default.InputDevices);

            return true;
        }
    }
}
