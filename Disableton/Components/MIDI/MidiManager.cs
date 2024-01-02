using RtMidi.Core.Devices;
using RtMidi.Core.Devices.Infos;
using System.Collections.Generic;

namespace Disableton.Components.MIDI
{
    public class MidiManager
    {
        public List<IMidiInputDevice> InputDevices
        {
            get => _inputDevices;
            private set => _inputDevices = value;
        }

        private List<IMidiInputDevice> _inputDevices = new List<IMidiInputDevice>();

        public MidiManager()
        {
        }

        public void AddMidiDevice(IMidiInputDeviceInfo device)
        {
            IMidiInputDevice inputDevice = device.CreateDevice();
            InputDevices.Add(inputDevice);
            inputDevice.Open();
        }

        public void RemoveMidiDevice(int index)
        {
            if (InRange(index))
                InputDevices.RemoveAt(index);
        }

        public void ListenToAllMidis(NoteOnMessageHandler action)
        {
            for (int i = 0; i < InputDevices.Count; i++)
            {
                ListenToMidi(i, action);
            }
        }

        public void ListenToMidi(int index, NoteOnMessageHandler action)
        {
            if (InRange(index))
            { 
                InputDevices[index].NoteOn += action;
            }    
        }

        public void Clear()
        {
            foreach (IMidiInputDevice device in InputDevices)
            {
                device?.Dispose();
            }

            InputDevices.Clear();
        }

        private bool InRange(int index)
        {
            if (index >= 0 &&
                index < InputDevices.Count)
            {
                return true;
            }
            return false;
        }
    }
}
