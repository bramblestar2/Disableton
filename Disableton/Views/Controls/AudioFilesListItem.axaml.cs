using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;

namespace Disableton.Views.Controls
{
    public class AudioFileInfo
    {
        public string? Path 
        { 
            get => _path; 
            set => _path = value; 
        }
        public string? Name
        {
            get => _name;
            set => _name = value;
        }
        public TimeSpan? Length
        {
            get => _length;
            set => _length = value;
        }

        private string? _path = string.Empty;
        private string? _name = string.Empty;
        private TimeSpan? _length = TimeSpan.Zero;
    }

    public class AudioFilesListItem : ListBoxItem
    {
    }
}
