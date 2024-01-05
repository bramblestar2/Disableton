using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Selection;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Disableton.Views.Controls
{
    public class AudioFilesList : ListBox
    {
        private DropControl? _dropControl;

        public AudioFilesList()
        {
            if (_dropControl != null)
                _dropControl.FileDrop += FileDropCallback;
        }

        private void FileDropCallback(object? sender, DragEventArgs e)
        {
            var files = e.Data.GetFiles();
            if (files is not null)
            {
                Debug.WriteLine($"{Uri.UnescapeDataString(files.First().Path.AbsolutePath)}");
            }
        }

        public void AddFile(AudioFileInfo file)
        {
            this.Items.Add(file);
        }

        public void AddFile(string path, TimeSpan duration)
        {
            this.Items.Add(new AudioFileInfo()
            {
                Path = path,
                Name = Path.GetFileName(path),
                Length = duration
            });
        }

        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            if (item is not AudioFileInfo) throw new ArgumentException("Wrong type, expecting " + typeof(AudioFileInfo));

            return new AudioFilesListItem();
        }

        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<AudioFilesListItem>(item, out recycleKey);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _dropControl = e.NameScope.Find<DropControl>("PART_Drop");
        }
    }
}
