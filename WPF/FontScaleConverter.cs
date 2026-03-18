using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF;

public class FontScaleConverter : IValueConverter
{
    private const double ReferenceWidth = 1200.0;
    private const double MinScale = 0.8;
    private const double MaxScale = 1.3;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not double actualWidth || actualWidth <= 0)
            return parameter is double fallback ? fallback : 14.0;

        var baseSize = 14.0;
        if (parameter is double paramDouble)
            baseSize = paramDouble;
        else if (parameter is string paramString && double.TryParse(paramString, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
            baseSize = parsed;

        var scale = actualWidth / ReferenceWidth;
        scale = Math.Clamp(scale, MinScale, MaxScale);

        return baseSize * scale;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}
