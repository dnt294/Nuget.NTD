using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
#elif NETFX_CORE
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Primitives;
#endif

namespace NTD.Controls
{
    public class ToggleButtonImage : ToggleButton
    {
        public ToggleButtonImage()
        {
            DefaultStyleKey = typeof(ToggleButtonImage);
        }

        public ImageSource SourceChecked
        {
            get { return (ImageSource)GetValue(SourceCheckedProperty); }
            set { SetValue(SourceCheckedProperty, value); }
        }
        public static readonly DependencyProperty SourceCheckedProperty = DependencyProperty.RegisterAttached(
            "SourceChecked", typeof(ImageSource), typeof(ToggleButtonImage), new PropertyMetadata(null));
        public ImageSource SourceUnchecked
        {
            get { return (ImageSource)GetValue(SourceUncheckedProperty); }
            set { SetValue(SourceUncheckedProperty, value); }
        }
        public static readonly DependencyProperty SourceUncheckedProperty = DependencyProperty.RegisterAttached(
            "SourceUnchecked", typeof(ImageSource), typeof(ToggleButtonImage), new PropertyMetadata(null));

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }
        public static readonly DependencyProperty StretchProperty = DependencyProperty.RegisterAttached(
            "Stretch", typeof(Stretch), typeof(ButtonImage), new PropertyMetadata(Stretch.Uniform));
    }
}
