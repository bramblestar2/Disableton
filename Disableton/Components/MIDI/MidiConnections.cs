using Melanchall.DryWetMidi.Multimedia;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Disableton.Components.MIDI
{
    static public class MidiConnections
    {
        static public ObservableCollection<InputDevice>? MidiInputDevices
        {
            get => _midiInputDevices;
            private set
            {
                _midiInputDevices = value;
            }
        }

        static private ObservableCollection<InputDevice>? _midiInputDevices = null;


        static public void ReloadConnections()
        {
            MidiInputDevices = new ObservableCollection<InputDevice>(InputDevice.GetAll());
        }
    }
}
