using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using NAudio.Wave;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;

namespace Disableton.Views.Controls
{
    public class AudioFilesList : ListBox
    {
        private DropControl? _dropControl;

        private readonly string[] mediaExtensions = {
                    ".WAV", ".MP3", ".OGG", ".AIF", ".MPA", 
                    ".AIFF", ".AAC", ".WMA", ".FLAC",
                };

        public AudioFilesList()
        {
        }

        private void FileDropCallback(object? sender, DragEventArgs e)
        {
            var files = e.Data.GetFiles();
            if (files is not null)
            {
                string path = Uri.UnescapeDataString(files.First().Path.AbsolutePath);
                Debug.WriteLine($"{Uri.UnescapeDataString(files.First().Path.AbsolutePath)}");

                var audioFileIndex = Array.IndexOf(mediaExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());

                if (-1 != audioFileIndex)
                {
                    AudioFileReader waveFile = new AudioFileReader(path);
                    AddFile(path, waveFile.TotalTime);
                }
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
                Name = System.IO.Path.GetFileName(path),
                Length = duration
            });
        }

        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            if (item is not AudioFileInfo) throw new ArgumentException("Wrong type, expecting " + typeof(AudioFileInfo));

            var listItem = new AudioFilesListItem();

            listItem.ContextMenu = new ContextMenu()
            {
                Padding = new Thickness(5),
                Items =
                {
                    new MenuItem()
                    {
                        Header = "Remove",
                        Command = ReactiveCommand.Create(() =>
                        {
                            this.Items.Remove(this.SelectedItem);
                        }),
                    }
                }
            };

            return listItem;
        }

        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<AudioFilesListItem>(item, out recycleKey);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _dropControl = e.NameScope.Find<DropControl>("PART_Drop");

            if (_dropControl != null)
                _dropControl.FileDrop += FileDropCallback;
        }
    }
}
