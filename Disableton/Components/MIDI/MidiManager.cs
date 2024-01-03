using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Disableton.Components.MIDI
{
    public class MidiManager
    {
        public ObservableCollection<InputDevice> InputDevices
        {
            get => _inputDevices;
            private set => _inputDevices = value;
        }

        private ObservableCollection<InputDevice> _inputDevices = new ObservableCollection<InputDevice>();

        public MidiManager()
        {
        }

        public void AddMidiDevice(InputDevice device)
        {
            InputDevices.Add(device);
        }

        public void RemoveMidiDevice(int index)
        {
            if (InRange(index))
                InputDevices.RemoveAt(index);
        }

        public void ListenToAllMidis(EventHandler<MidiEventReceivedEventArgs> action)
        {
            for (int i = 0; i < InputDevices.Count; i++)
            {
                ListenToMidi(i, action);
            }
        }

        public void ListenToMidi(int index, EventHandler<MidiEventReceivedEventArgs> action)
        {
            if (InRange(index))
            {
                if (!InputDevices[index].IsListeningForEvents)
                    InputDevices[index].StartEventsListening();

                InputDevices[index].EventReceived += action;
            }    
        }

        public void Clear()
        {
            foreach (InputDevice device in InputDevices)
            {
                if (device.IsListeningForEvents)
                    device.StopEventsListening();
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
