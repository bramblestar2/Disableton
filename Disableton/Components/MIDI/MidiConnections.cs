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
        static public List<MidiDevice>? MidiDevices
        {
            get => _midiDevices;
            private set
            {
                _midiDevices = value;
            }
        }

        static private List<MidiDevice>? _midiDevices = null;


        static public void ReloadConnections()
        {

        }
    }
}
