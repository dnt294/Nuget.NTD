using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
#if WINDOWS_PHONE
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
    [TemplateVisualState(GroupName = "SelectedGroups", Name = "Selected")]
    [TemplateVisualState(GroupName = "SelectedGroups", Name = "Unselected")]
    public class BigTabBarItem : Control
    {
        public BigTabBarItem()
        {
            DefaultStyleKey = typeof(BigTabBarItem);
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",typeof(string),typeof(BigTabBarItem),new PropertyMetadata(string.Empty));
        //
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof(bool), typeof(BigTabBarItem), new PropertyMetadata(false, OnIsSelecedChanged));

        private static void OnIsSelecedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BigTabBarItem;
            if (control == null || e.NewValue == null || e.NewValue == e.OldValue) return;
            string state = (bool.Parse(e.NewValue.ToString())) ? "Selected" : "Unselected";
            VisualStateManager.GoToState(control, state, true);            
        }

        // Image
        public ImageSource SourceSelected
        {
            get { return (ImageSource)GetValue(SourceSelectedProperty); }
            set { SetValue(SourceSelectedProperty, value); }
        }
        public static readonly DependencyProperty SourceSelectedProperty = DependencyProperty.Register(
            "SourceSelected", typeof(ImageSource), typeof(BigTabBarItem), new PropertyMetadata(null));
        public ImageSource SourceUnselected
        {
            get { return (ImageSource)GetValue(SourceUnselectedProperty); }
            set { SetValue(SourceUnselectedProperty, value); }
        }
        public static readonly DependencyProperty SourceUnselectedProperty = DependencyProperty.Register(
            "SourceUnselected", typeof(ImageSource), typeof(BigTabBarItem), new PropertyMetadata(null));

        // Text
        public SolidColorBrush ColorSelected
        {
            get { return (SolidColorBrush)GetValue(ColorSelectedProperty); }
            set { SetValue(ColorSelectedProperty, value); }
        }
        public static readonly DependencyProperty ColorSelectedProperty = DependencyProperty.Register(
            "ColorSelected", typeof(SolidColorBrush), typeof(BigTabBarItem), new PropertyMetadata(new SolidColorBrush(Colors.White)));
        public SolidColorBrush ColorUnselected
        {
            get { return (SolidColorBrush)GetValue(ColorUnselectedProperty); }
            set { SetValue(ColorUnselectedProperty, value); }
        }
        public static readonly DependencyProperty ColorUnselectedProperty = DependencyProperty.Register(
            "ColorUnselected", typeof(SolidColorBrush), typeof(BigTabBarItem), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #region CornerRadius Property
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(BigTabBarItem), new PropertyMetadata(new CornerRadius(0)));
        #endregion
    }
}
