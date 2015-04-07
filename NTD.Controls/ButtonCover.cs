using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#elif NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
#endif 

namespace NTD.Controls
{
    public class ButtonCover : Button
    {
        public ButtonCover()
        {
            DefaultStyleKey = typeof(ButtonCover);            
        }
        public SolidColorBrush CoverColor
        {
            get { return (SolidColorBrush)GetValue(CoverColorProperty); }
            set { SetValue(CoverColorProperty, value); }
        }
        public static readonly DependencyProperty CoverColorProperty = DependencyProperty.Register(
            "CoverColor", typeof(SolidColorBrush), typeof(ButtonCover), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        #region CornerRadius Property
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(ButtonCover), new PropertyMetadata(new CornerRadius(0)));
        #endregion
    }
}
