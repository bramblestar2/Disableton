using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Disableton.Views.Controls
{
    [PseudoClasses(pcDragEnter, pcDrop)]
    public class DropControl : TemplatedControl
    {
        #region Pseudo Classes

        public const string pcDragEnter = ":drag-enter";
        public const string pcDrop      = ":drop";

        #endregion

        public static readonly RoutedEvent<DragEventArgs> FileDropEvent =
            RoutedEvent.Register<DragEventArgs>(nameof(FileDrop), RoutingStrategies.Bubble, typeof(DropControl));


        public event EventHandler<DragEventArgs> FileDrop
        {
            add => AddHandler(FileDropEvent, value);
            remove => RemoveHandler(FileDropEvent, value);
        }

        public DropControl()
        {
            this.AddHandler(DragDrop.DropEvent, Drop);
            this.AddHandler(DragDrop.DragEnterEvent, DragEnter);
            this.AddHandler(DragDrop.DragLeaveEvent, DragLeave);
        }

        private void Drop(object? sender, DragEventArgs e)
        {
            var files = e.Data.GetFiles();
            if (files == null || files.Count() == 0) return;

            PseudoClasses.Set(pcDrop, true);
            PseudoClasses.Set(pcDragEnter, false);

            
            Task.Delay(1000).ContinueWith(t =>
            {
                Dispatcher.UIThread.Invoke(() =>
                {
                    PseudoClasses.Set(pcDrop, false);
                });
            });
        }

        private void DragEnter(object? sender, DragEventArgs e)
        {
            var files = e.Data.GetFiles();
            if (files == null || files.Count() == 0) return;

            PseudoClasses.Set(pcDragEnter, true);
        }

        private void DragLeave(object? sender, DragEventArgs e)
        {
            var files = e.Data.GetFiles();
            if (files == null || files.Count() == 0) return;

            PseudoClasses.Set(pcDragEnter, false);
        }
    }
}
