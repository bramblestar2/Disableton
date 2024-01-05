using RtMidi.Core.Devices;
using RtMidi.Core.Devices.Infos;
using System.Collections.ObjectModel;

namespace Disableton.Components.MIDI
{
    static public class MidiManager
    {
        static public ObservableCollection<IMidiInputDevice> InputDevices
        {
            get => _inputDevices;
            private set => _inputDevices = value;
        }

        static private ObservableCollection<IMidiInputDevice> _inputDevices = new ObservableCollection<IMidiInputDevice>();

        static MidiManager()
        {
        }

        static public void AddMidiDevice(IMidiInputDeviceInfo device)
        {
            var inputDevice = device.CreateDevice();
            inputDevice.Open();
            InputDevices.Add(inputDevice);
        }

        static public void RemoveMidiDevice(int index)
        {
            if (InRange(index))
                InputDevices.RemoveAt(index);
        }

        static public void ListenToAllMidis(NoteOnMessageHandler action)
        {
            for (int i = 0; i < InputDevices.Count; i++)
            {
                ListenToMidi(i, action);
            }
        }

        static public void ListenToMidi(int index, NoteOnMessageHandler action)
        {
            if (InRange(index))
            {
                InputDevices[index].NoteOn += action;
            }    
        }

        static public void Clear()
        {
            foreach (IMidiInputDevice device in InputDevices)
            {
                device.Close();
                device.Dispose();
            }

            InputDevices.Clear();
        }

        static private bool InRange(int index)
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
