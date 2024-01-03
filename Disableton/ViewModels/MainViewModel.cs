using Avalonia.Threading;
using Disableton.Components.MIDI;
using RtMidi.Core.Devices;
using System;
using System.Collections.ObjectModel;

namespace Disableton.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<string> TotalMidiDevices
    {
        get => _totalMidiDevices;
        private set => _totalMidiDevices = value;
    }

    public ObservableCollection<IMidiInputDevice> ConnectedDevices
    {
        get => _midiManager.InputDevices;
    }

    private MidiManager _midiManager = new MidiManager();
    private ObservableCollection<string> _totalMidiDevices = new ObservableCollection<string>();

    public MainViewModel()
    {
        MidiConnections.ReloadConnections();
        RefreshTotalDevicesList();

        var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
        timer.Tick += (s, e) =>
        {
            if (MidiConnections.ReloadConnections())
                RefreshTotalDevicesList();
        };

        timer.Start();

        //_midiManager.AddMidiDevice(MidiConnections.MidiInputDevices[0]);
        //
        //_midiManager.ListenToAllMidis((IMidiInputDevice s, in NoteOnMessage e) =>
        //{
        //    Debug.WriteLine($"{e.Key}");
        //});
    }

    private void RefreshTotalDevicesList()
    {
        TotalMidiDevices.Clear();
        foreach (var device in MidiConnections.MidiInputDevices)
        {
            TotalMidiDevices.Add(device.Name);
        }
    }
}
