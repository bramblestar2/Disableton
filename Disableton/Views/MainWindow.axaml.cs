using Avalonia.Controls;
using Avalonia.Input;
using Disableton.Components.Audio;
using Disableton.Components.Drumrack;
using Disableton.Components.MIDI;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Diagnostics;

namespace Disableton.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
