using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ClockLineIntervalConverter : IMultiValueConverter
    {
        public ClockDisplayMode DisplayMode { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (DateTime)values[0];
            var interval = (int)values[1]; // values[1] is ClockMinutesInterval

            return DisplayMode == ClockDisplayMode.Hours
                ? ((time.Hour > 13) ? time.Hour - 12 : time.Hour) * (360 / 12)
                : (time.Minute == 0 ? 60 : Math.Round(time.Minute / (double)interval) * interval) * (360 / 60);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[] { Binding.DoNothing, Binding.DoNothing };
        }
    }
}
