using Avalonia.Data.Converters;
using RtMidi.Core.Devices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disableton.Components.Converters
{
    public class InputMidiDeviceToStringName : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is IMidiInputDevice device)
            {
                return device.Name;
            }

            return "CAN'T RETRIEVE NAME";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
