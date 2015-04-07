using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Windows.Foundation;
#elif NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
#endif
namespace NTD.Controls
{
    [TemplateVisualState(GroupName = "TopStates", Name = "ShowTop")]
    [TemplateVisualState(GroupName = "TopStates", Name = "HideTop")]
    [TemplateVisualState(GroupName = "BottomStates", Name = "ShowBottom")]
    [TemplateVisualState(GroupName = "BottomStates", Name = "HideBottom")]
    public class AutoHideControl : ContentControl
    {
        public enum PositionType { Top, Bottom }
        private UIElement _scrollBar;
        private DoubleAnimation _hideAnimation1;
        private DoubleAnimation _hideAnimation2;
        private double _offsetValue;
        private double _minimumOffsetUp = 50;
        private double _minimumOffsetDown = 10;

        public AutoHideControl()
        {
            DefaultStyleKey = typeof(AutoHideControl);
        }

#if WINDOWS_PHONE
        public override void OnApplyTemplate()
#elif NETFX_CORE
        protected override void OnApplyTemplate()
#endif
        {
            base.OnApplyTemplate();
            var _hideState1 = (base.GetTemplateChild("HideTop") as VisualState);
            var _hideState2 = (base.GetTemplateChild("HideBottom") as VisualState);
            if (_hideState1 != null)
                _hideAnimation1 = _hideState1.Storyboard.Children[0] as DoubleAnimation;
            if (_hideState2 != null)
                _hideAnimation2 = _hideState2.Storyboard.Children[0] as DoubleAnimation;
        }

        #region PositionProperty
        public PositionType Position
        {
            get { return (PositionType)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register
            ("Position", typeof(PositionType), typeof(AutoHideControl), new PropertyMetadata(PositionType.Top));
        #endregion

        #region IsShowProperty
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }
        public static readonly DependencyProperty IsShowProperty = DependencyProperty.Register(
            "IsShow", typeof(bool), typeof(AutoHideControl), new PropertyMetadata(true, new PropertyChangedCallback(IsShowChanged)));

        private static void IsShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AutoHideControl control = d as AutoHideControl;
            if (control == null || e.NewValue == null || e.NewValue == e.OldValue) return;
            bool isshow = Convert.ToBoolean(e.NewValue);
            string state = (isshow) ? ((control.Position == PositionType.Top) ? "ShowTop" : "ShowBottom")
                                        : ((control.Position == PositionType.Top) ? "HideTop" : "HideBottom");
            if (!isshow)
            {
                if (control.Position == PositionType.Top)
                {
                    if (control._hideAnimation1 != null)
                        control._hideAnimation1.To = -control.ActualHeight;
                }
                else
                {
                    if (control._hideAnimation2 != null)
                        control._hideAnimation2.To = control.ActualHeight;
                }
            }
            VisualStateManager.GoToState(control, state, true);
        }

        #endregion

        #region ScrollBindProperty
        public FrameworkElement ScrollBind
        {
            get { return (FrameworkElement)GetValue(ScrollBindProperty); }
            set { SetValue(ScrollBindProperty, value); }
        }

        public static readonly DependencyProperty ScrollBindProperty =
            DependencyProperty.Register("ScrollBind", typeof(FrameworkElement), typeof(AutoHideControl),
            new PropertyMetadata(null, new PropertyChangedCallback(OnScrollBindChanged)));
        private static void OnScrollBindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as AutoHideControl;
            if (control == null || e.NewValue == null || e.OldValue == e.NewValue) return;
            (control).OnSScrollBindChanged(e);
        }
        private void OnSScrollBindChanged(DependencyPropertyChangedEventArgs e)
        {      
            if (_scrollBar != null)
                DetachScroller(_scrollBar);
            if (e.NewValue != null)
            {
                var el = e.NewValue as UIElement;
                FindAndAttachScrollViewer(el);
            }
        }
      
        private void FindAndAttachScrollViewer(UIElement start)
        {
            _scrollBar = FindScroller(start);
            AttachScroller(_scrollBar);
        }
        private UIElement FindScroller(UIElement start)
        {
            UIElement target = null;

            if (IsScroller(start))
            {
                target = start;
            }
            else
            {
                var childCount = VisualTreeHelper.GetChildrenCount(start);

                for (var i = childCount - 1; i >= 0; i--)
                {
                    var el = VisualTreeHelper.GetChild(start, i) as UIElement;
                    target = IsScroller(el) ? el : FindScroller(el);
                    if (target != null)
                        break;
                }
            }

            return target as UIElement;
        }
        private void AttachScroller(UIElement scroller)
        {
            if (scroller == null) return;
            if (scroller is ScrollBar)
                ((ScrollBar)scroller).ValueChanged += scrollbar_ValueChanged;
        }
        private void DetachScroller(UIElement scroller)
        {
            if (scroller == null) return;
            if (scroller is ScrollBar)
                ((ScrollBar)scroller).ValueChanged -= scrollbar_ValueChanged;
        }
        private bool IsScroller(UIElement el)
        {
            return ((el is ScrollBar && ((ScrollBar)el).Orientation == Orientation.Vertical));
        }
        #if WINDOWS_PHONE
        private void scrollbar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        #elif NETFX_CORE
        private void scrollbar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        #endif        
        {
            if (_offsetValue - e.NewValue < -_minimumOffsetDown) // kéo xuống
            {
                _offsetValue = e.NewValue;
                IsShow = false;
            }
            else if (_offsetValue - e.NewValue > _minimumOffsetUp) // kéo lên
            {
                _offsetValue = e.NewValue;
                IsShow = true;
            }
        }

        #endregion
    }
}
