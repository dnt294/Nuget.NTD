using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.Specialized;

namespace NTD.Controls
{
    [TemplatePart(Name = LayoutRootName, Type = typeof(StackPanel))]
    public class SlideView : Control
    {
        public SlideView()
        {
            DefaultStyleKey = typeof(SlideView);
        }        
        private const string LayoutRootName = "LayoutRoot";
        internal PanoramaFullSize LayoutRoot;
        private List<UIElement> internalList = new List<UIElement>();

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            LayoutRoot = GetTemplateChild(LayoutRootName) as PanoramaFullSize;
        }

        private DataTemplate itemTemplate;
        public DataTemplate ItemTemplate
        {
            get { return itemTemplate; }
            set { itemTemplate = value; }
        }
        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(SlideView),
            new PropertyMetadata(null, new PropertyChangedCallback(ItemsSourceChanged)));
        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null || e.NewValue == e.OldValue || (d as SlideView) == null) return;

            SlideView slideView = d as SlideView;
            var objList = e.NewValue as INotifyCollectionChanged;
            if (objList != null)
            {
                objList.CollectionChanged += (sender, eventArgs) =>
                {
                    switch (eventArgs.Action)
                    {
                        case NotifyCollectionChangedAction.Remove:
                            foreach (var oldItem in eventArgs.OldItems)
                            {
                                for (int i = 0; i < slideView.internalList.Count; i++)
                                {
                                    var fe = slideView.internalList[i] as FrameworkElement;
                                    if (fe == null || fe.DataContext != oldItem) return;
                                    slideView.RemovePanoramaItemAt(i);
                                }
                            }
                            break;
                        case NotifyCollectionChangedAction.Add:
                            foreach (var newItem in eventArgs.NewItems)
                            {
                                slideView.CreatePanoramaItem(newItem);
                            }
                            break;
                    }
                };
                slideView.BindData();
            }
        }
        private void BindData()
        {
            if (ItemsSource == null) return;
            LayoutRoot.Items.Clear();
            this.internalList.Clear();
            foreach (var item in ItemsSource)
                this.CreatePanoramaItem(item);
        }
        private void CreatePanoramaItem(object item)
        {
            FrameworkElement element = ItemTemplate.LoadContent() as FrameworkElement;
            if (element == null) return;
            element.DataContext = item;
            element.Tap += element_Tap;
            LayoutRoot.Items.Add(element);
        }
        private void RemovePanoramaItemAt(int index)
        {
            var element = this.internalList[index];
            element.Tap -= element_Tap;
            this.internalList.RemoveAt(index);
            LayoutRoot.Items.Remove(element);
            LayoutRoot.SetValue(PanoramaFullSize.SelectedItemProperty, LayoutRoot.Items[0]);
            LayoutRoot.Measure(new Size());
        }

        // when tap in item
        public event EventHandler TabTap;

        void element_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (this.TabTap != null)
                TabTap(sender, e);
        }

    }
}
