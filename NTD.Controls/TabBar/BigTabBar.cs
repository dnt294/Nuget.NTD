using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
#if WINDOWS_PHONE
using System.Windows.Controls;
#elif NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

namespace NTD.Controls
{    
    [TemplatePartAttribute(Name = "MyItemPresenter", Type = typeof(ItemsPresenter))]
    public class BigTabBar : ItemsControl
    {
        public BigTabBar()
        {
            DefaultStyleKey = typeof(BigTabBar);
        }

#if WINDOWS_PHONE
        public override void OnApplyTemplate()
#elif NETFX_CORE
        protected override void OnApplyTemplate()
#endif
        {
            base.OnApplyTemplate();
            foreach (var item in Items)
                #if WINDOWS_PHONE
                (item as UIElement).Tap += BigTabBarItem_Tap;
                #elif NETFX_CORE
                (item as UIElement).Tapped += BigTabBar_Tapped;
                #endif
            ClipToBounds();
            
        }
#if WINDOWS_PHONE
        protected override Size ArrangeOverride(Size finalSize)
#elif NETFX_CORE
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
#endif
        {
            ArrangeSize(finalSize);
            return base.ArrangeOverride(finalSize);            
        }

        private void ClipToBounds()
        {
            var radius = this.CornerRadius;
            if (this.Items.Count ==0) return;
            if (this.Items.Count == 1)
                (this.Items[0] as BigTabBarItem).CornerRadius = radius;
            else
            {
                var count = this.Items.Count() - 1;
                (this.Items[0] as BigTabBarItem).CornerRadius = new CornerRadius(radius.TopLeft,0,0,radius.BottomLeft);
                (this.Items[count] as BigTabBarItem).CornerRadius = new CornerRadius(0, radius.TopRight, radius.BottomRight, 0);
            }
        }

#if WINDOWS_PHONE
        private void ArrangeSize(Size finalSize)
#elif NETFX_CORE
        private void ArrangeSize(Windows.Foundation.Size finalSize)
#endif        
        {
            if (this.Items.Count == 0) return;
            double width = finalSize.Width;
            double height = finalSize.Height;
            double splitWidth = width / this.Items.Count;
            foreach (var item in this.Items)
            {
                (item as BigTabBarItem).Width = splitWidth;
                (item as BigTabBarItem).Height = height;
            }
        }

        public event EventHandler SelectedChanged;
        #if WINDOWS_PHONE
        void BigTabBarItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        #elif NETFX_CORE
        void BigTabBar_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)            
        #endif
        {
            var index = Items.IndexOf(sender as UIElement);
            this.SelectedIndex = index;
            if (SelectedChanged != null)
            {
                SelectedChanged(this, null);
            }
        }

        #region CornerRadius Property
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(BigTabBar), new PropertyMetadata(new CornerRadius(0)));
        #endregion

        #region SelectedIndex
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex", typeof(int), typeof(BigTabBar), new PropertyMetadata(-1, OnSelectedIndexChanged));
        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BigTabBar;
            if (control == null || e.NewValue == null || e.NewValue == e.OldValue) return;
            control.OnSelectedIndexChanged(e);
        }
        private void OnSelectedIndexChanged(DependencyPropertyChangedEventArgs e)
        {
            int oldindex = int.Parse(e.OldValue.ToString());
            int newindex = int.Parse(e.NewValue.ToString());
            if (Items.Count() == 0) return;
            if (oldindex >= 0 && oldindex <= Items.Count() && Items.ElementAt(oldindex) as BigTabBarItem != null)
                (Items.ElementAt(oldindex) as BigTabBarItem).IsSelected = false;
            if (newindex >= 0 && newindex <= Items.Count() && Items.ElementAt(newindex) as BigTabBarItem != null)
                (Items.ElementAt(newindex) as BigTabBarItem).IsSelected = true;            
        }
        #endregion
    }
}
