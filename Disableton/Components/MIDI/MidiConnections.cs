using Melanchall.DryWetMidi.Multimedia;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disableton.Components.MIDI
{
    static public class MidiConnections
    {
        static public List<InputDevice>? MidiInputDevices
        {
            get => _midiInputDevices;
            private set
            {
                _midiInputDevices = value;
            }
        }

        static private List<InputDevice>? _midiInputDevices = null;


        static public void ReloadConnections()
        {
            MidiInputDevices = new List<InputDevice>(InputDevice.GetAll());
        }
    }
}
