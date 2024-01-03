using RtMidi.Core.Devices;
using RtMidi.Core.Devices.Infos;
using System.Collections.ObjectModel;

namespace Disableton.Components.MIDI
{
    public class MidiManager
    {
        public ObservableCollection<IMidiInputDevice> InputDevices
        {
            get => _inputDevices;
            private set => _inputDevices = value;
        }

        private ObservableCollection<IMidiInputDevice> _inputDevices = new ObservableCollection<IMidiInputDevice>();

        public MidiManager()
        {
        }

        public void AddMidiDevice(IMidiInputDeviceInfo device)
        {
            var inputDevice = device.CreateDevice();
            inputDevice.Open();
            InputDevices.Add(inputDevice);
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
                device.Close();
                device.Dispose();
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
