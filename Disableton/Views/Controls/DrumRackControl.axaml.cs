using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using System;

namespace Disableton.Views.Controls
{
    public class DrumRackControl : TemplatedControl
    {
        public DrumRackControl()
        {

        }

        private Control MakeButton()
        {
            var button = new ContentPresenter();
            button.Content = new Border()
            {
                Width = 50,
                Height = 50,
                
            };

            throw new NotImplementedException();
        }
    }
}
