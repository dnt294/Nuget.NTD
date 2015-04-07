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
#endif

namespace NTD.Controls
{
    public class LoadingIndicator : Control
    {
        public LoadingIndicator()
        {
            this.DefaultStyleKey = typeof(LoadingIndicator);
        }
        
#if WINDOWS_PHONE
        public override void OnApplyTemplate()
#elif NETFX_CORE
        protected override void OnApplyTemplate()
#endif
        {
            base.OnApplyTemplate();
            //TemplateApplied = true;
        }
        /// <summary>
        /// Active state for the loading indicator
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LoadingIndicator), new PropertyMetadata(false, new PropertyChangedCallback(IsActiveChanged)));
        private static void IsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ld = d as LoadingIndicator;
            if (ld == null || e.NewValue == e.OldValue) return;
            var isActive = (bool)e.NewValue;
            ld.UpdateState(isActive);
        }
        void UpdateState(bool isActive)
        {
            if (true)//TemplateApplied)
            {
                string active = isActive ? "Active" : "Inactive";
                VisualStateManager.GoToState(this, active, true);
            }
        }
        /// <summary>
        /// ImageSource for the loading indicator
        /// </summary>
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.RegisterAttached(
           "ImageSource", typeof(ImageSource), typeof(LoadingIndicator), new PropertyMetadata(null));
    }
}
