using System.Collections.Generic;
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
    [TemplatePart(Name=LayoutRootName,Type=typeof(StackPanel))]
    public class SmallIndicator : Control
    {
        private const string LayoutRootName = "LayoutRoot";        
        internal StackPanel LayoutRoot { get; set; }
        public SmallIndicator()
        {
            DefaultStyleKey = typeof(SmallIndicator);            
        }
#if WINDOWS_PHONE
        public 
#elif NETFX_CORE
        protected
#endif
        override void OnApplyTemplate()
        {            
            base.OnApplyTemplate();
            LayoutRoot = GetTemplateChild(LayoutRootName) as StackPanel;
            if (ItemsCount > 0)
                OnItemsCountChanged(ItemsCount);
        }

        private List<UIElement> internalList = new List<UIElement>();

        public Brush SelectedColor
        {
            get { return (Brush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
            "SelectedColor", typeof(Brush), typeof(SmallIndicator), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));
        public Brush UnselectedColor
        {
            get { return (Brush)GetValue(UnselectedColorProperty); }
            set { SetValue(UnselectedColorProperty, value); }
        }
        public static readonly DependencyProperty UnselectedColorProperty = DependencyProperty.Register(
            "UnselectedColor", typeof(Brush), typeof(SmallIndicator), new PropertyMetadata(new SolidColorBrush(Colors.White)));
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }       

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(SmallIndicator), new PropertyMetadata(new CornerRadius(0)));

        public double IndicatorWidth
        {
            get { return (double)GetValue(IndicatorWidthProperty); }
            set { SetValue(IndicatorWidthProperty, value); }
        }
        public static readonly DependencyProperty IndicatorWidthProperty = DependencyProperty.Register(
            "IndicatorWidth", typeof(double), typeof(SmallIndicator), new PropertyMetadata(10d));

        public double IndicatorHeight
        {
            get { return (double)GetValue(IndicatorHeightProperty); }
            set { SetValue(IndicatorHeightProperty, value); }
        }
        public static readonly DependencyProperty IndicatorHeightProperty = DependencyProperty.Register(
            "IndicatorHeight", typeof(double), typeof(SmallIndicator), new PropertyMetadata(10d));

        public Thickness IndicatorMargin
        {
            get { return (Thickness)GetValue(IndicatorMarginProperty); }
            set { SetValue(IndicatorMarginProperty, value); }
        }
        public static readonly DependencyProperty IndicatorMarginProperty = DependencyProperty.Register(
            "IndicatorMargin", typeof(Thickness), typeof(SmallIndicator), new PropertyMetadata(new Thickness(5)));

        #region ItemsCount
        public int ItemsCount { get; set; }
        
        //public static readonly DependencyProperty ItemsCountProperty = DependencyProperty.Register(
        //    "ItemsCount", typeof(int), typeof(SmallIndicator), new PropertyMetadata(-1, OnItemsCountChanged));

        //private static void OnItemsCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{            
        //    var indicator = d as SmallIndicator;            
        //    if (indicator == null || e.NewValue == e.OldValue || (int)e.NewValue == 0 || indicator.LayoutRoot == null) return;
        //    indicator.OnItemsCountChanged(e);
        //}

        private void OnItemsCountChanged(int count)
        {            
            if (LayoutRoot.Children.Count > 0)
            {
                for (int i = 0; i < LayoutRoot.Children.Count; i++)
                    LayoutRoot.Children[i] = null;
                LayoutRoot.Children.Clear();
            }
            for (int i = 0; i < (int)count; i++)
            {
                Border element = new Border()
                {
                    Width = IndicatorWidth,
                    Height = IndicatorHeight,
                    CornerRadius = CornerRadius,
                    Margin = IndicatorMargin,
                    Background = UnselectedColor
                };
                LayoutRoot.Children.Add(element);
            }
        }
        #endregion

        #region SelectedIndex
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex", typeof(int), typeof(SmallIndicator), new PropertyMetadata(-1, OnSelectedIndexChanged)
            );

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var indicator = d as SmallIndicator;
            if (indicator == null || e.NewValue == e.OldValue) return;
            indicator.OnSelectedIndexChanged(e);
        }

        private void OnSelectedIndexChanged(DependencyPropertyChangedEventArgs e)
        {
            int oldvalue = (int)e.OldValue;
            int newvalue = (int)e.NewValue;
            if (oldvalue >= 0 && oldvalue < LayoutRoot.Children.Count)
                (LayoutRoot.Children[oldvalue] as Border).Background = UnselectedColor;
            if (newvalue >= 0 && newvalue < LayoutRoot.Children.Count)
                (LayoutRoot.Children[newvalue] as Border).Background = SelectedColor;
        }
        #endregion
    }
}
