using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Disableton.Views.Controls
{
    public class DropControl : TemplatedControl
    {
        public static readonly RoutedEvent<DragEventArgs> FileDropEvent =
            RoutedEvent.Register<DragEventArgs>(nameof(FileDrop), RoutingStrategies.Bubble, typeof(DropControl));

        public event EventHandler<DragEventArgs> FileDrop
        {
            add => AddHandler(FileDropEvent, value);
            remove => RemoveHandler(FileDropEvent, value);
        }

        public DropControl()
        {

        }
    }
}
