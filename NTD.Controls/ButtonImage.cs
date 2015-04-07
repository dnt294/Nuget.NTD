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
#elif NETFX_CORE
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#endif

namespace NTD.Controls
{
    public class ButtonImage : Button
    {
        public ButtonImage()
        {
            DefaultStyleKey = typeof(ButtonImage);
        }

        public ImageSource SourceNormal
        {
            get { return (ImageSource)GetValue(SourceNormalProperty); }
            set { SetValue(SourceNormalProperty, value); }
        }
        public static readonly DependencyProperty SourceNormalProperty = DependencyProperty.RegisterAttached(
           "SourceNormal", typeof(ImageSource), typeof(ButtonImage), new PropertyMetadata(null));
        public ImageSource SourcePressed
        {
            get
            {
                if (null == GetValue(SourcePressedProperty))
                    return (ImageSource)GetValue(SourceNormalProperty);
                return (ImageSource)GetValue(SourcePressedProperty);
            }
            set { SetValue(SourcePressedProperty, value); }
        }
        public static readonly DependencyProperty SourcePressedProperty = DependencyProperty.RegisterAttached(
            "SourcePressed", typeof(ImageSource), typeof(ButtonImage), new PropertyMetadata(null));
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }
        public static readonly DependencyProperty StretchProperty = DependencyProperty.RegisterAttached(
            "Stretch", typeof(Stretch), typeof(ButtonImage), new PropertyMetadata(Stretch.Uniform));

    }
}
