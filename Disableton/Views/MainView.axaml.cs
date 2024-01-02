using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Disableton.Components.Drumrack;
using Disableton.Components.MIDI;
using Disableton.ViewModels;
using Melanchall.DryWetMidi.Core;
using System.Diagnostics;

namespace Disableton.Views;

public partial class MainView : UserControl
{
    private MainViewModel viewModel;

    public MainView()
    {
        InitializeComponent();

        viewModel = new MainViewModel();
        this.DataContext = viewModel;

        pages.AddPage();
        pages.AddSample(0, 80, new Sample(@"C:\Users\thega\Downloads\Store Bell Sound Effect.mp3"));
    }

    public MidiManager midiManager = new MidiManager();
    public Pages pages = new Pages();

    public void ReloadButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MidiConnections.ReloadConnections();
    }

    public void AddDeviceButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (MidiConnections.MidiInputDevices != null)
            midiManager.AddMidiDevice(MidiConnections.MidiInputDevices[0]);
    }

    public void AddListenersButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (MidiConnections.MidiInputDevices != null)
            midiManager.ListenToMidi(0, (sender, e) =>
            {
                if (e.Event is not NoteOnEvent noteEvent)
                    return;

                pages.Play(0, noteEvent.NoteNumber);
                Debug.WriteLine($"{noteEvent.NoteNumber}");
            });
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);

        midiManager.Clear();
    }
}
