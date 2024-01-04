using Avalonia.Controls;
using Avalonia.Input;
using System.Diagnostics;
using System.Linq;

namespace Disableton.Views.Controls
{
    public partial class Drop : UserControl
    {
        public Drop()
        {
            InitializeComponent();

            this.AddHandler(DragDrop.DropEvent, FileDrop);
        }

        private void FileDrop(object? sender, DragEventArgs e)
        {
            var files = e.Data.GetFiles();
            if (files == null || files.Count() == 0) return;


        }
    }
}
