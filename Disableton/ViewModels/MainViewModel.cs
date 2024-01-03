using Disableton.Components.MIDI;
using Melanchall.DryWetMidi.Multimedia;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Disableton.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<InputDevice>? TotalMidiDevices
    {
        get => MidiConnections.MidiInputDevices;
    }

    public ObservableCollection<InputDevice> ConnectedDevices
    {
        get => _midiManager.InputDevices;
    }

    private MidiManager _midiManager = new MidiManager();

    public MainViewModel()
    {
        MidiConnections.ReloadConnections();

        DevicesWatcher.Instance.DeviceAdded += Instance_DeviceAdded;
        DevicesWatcher.Instance.DeviceRemoved += Instance_DeviceAdded;
    }

    private void Instance_DeviceAdded(object? sender, DeviceAddedRemovedEventArgs e)
    {
        MidiConnections.ReloadConnections();
    }
}
