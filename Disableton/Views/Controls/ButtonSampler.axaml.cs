using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Disableton.Views.Controls
{
    public class ButtonSampler : TemplatedControl
    {
        #region Properties

        public static readonly StyledProperty<int> MidiButtonProperty =
            AvaloniaProperty.Register<ButtonSampler, int>(nameof(MidiButton), defaultValue: 1);

        #endregion

        #region Properties Variables

        public int MidiButton
        {
            get => GetValue(MidiButtonProperty);
            set => SetValue(MidiButtonProperty, value);
        }

        #endregion

        public ButtonSampler()
        {

        }
    }
}
