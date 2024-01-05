using Avalonia.Threading;
using Disableton.Components.MIDI;
using ReactiveUI;
using RtMidi.Core.Devices;
using RtMidi.Core.Messages;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;

namespace Disableton.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Properties

    public ObservableCollection<string> TotalMidiDevices
    {
        get => _totalMidiDevices;
        private set => this.RaiseAndSetIfChanged(ref _totalMidiDevices, value);
    }

    public ObservableCollection<string> ConnectedDevices
    {
        get => _connectedMidiDevices;
        private set => this.RaiseAndSetIfChanged(ref _connectedMidiDevices, value);
    }

    public int? SelectedMidiDevice
    {
        get => _selectedMidiDevice;
        set => this.RaiseAndSetIfChanged(ref _selectedMidiDevice, value);
    }

    #endregion

    #region Member Variables

    private ObservableCollection<string> _totalMidiDevices = new ObservableCollection<string>();
    private ObservableCollection<string> _connectedMidiDevices = new ObservableCollection<string>();

    private int? _selectedMidiDevice = null;

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> SetActiveMidiCommand { get; } 
    public ReactiveCommand<Unit, Unit> RemoveMidiCommand { get; } 

    #endregion


    public MainViewModel()
    {
        MidiConnections.ReloadConnections(true);
        RefreshTotalDevicesList();

        var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
        timer.Tick += (s, e) =>
        {
            if (MidiConnections.ReloadConnections())
            {
                RefreshTotalDevicesList();
                this.RaisePropertyChanged(nameof(TotalMidiDevices));
            }
        };

        timer.Start();



        SetActiveMidiCommand = ReactiveCommand.Create(() =>
        {
            if (SelectedMidiDevice != null)
            {
                MidiManager.Clear();
                MidiManager.AddMidiDevice(MidiConnections.MidiInputDevices[(int)SelectedMidiDevice]);

                MidiManager.ListenToAllMidis((IMidiInputDevice s, in NoteOnMessage e) =>
                {
                    int note = (int)e.Key;

                    Task.Run(() =>
                    {
                        Debug.WriteLine($"{note}");
                    });
                });

                RefreshConnectedDevicesList();
            }
        });

        RemoveMidiCommand = ReactiveCommand.Create(() =>
        {
            MidiManager.Clear();
            RefreshConnectedDevicesList();
        });
    }

    public void Dispose()
    {
        MidiManager.Clear();

    }

    private void RefreshConnectedDevicesList()
    {
        ConnectedDevices.Clear();
        foreach (var device in MidiManager.InputDevices)
        {
            ConnectedDevices.Add(device.Name);
        }
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
