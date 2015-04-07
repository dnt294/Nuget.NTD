using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NTD.Controls
{
    [TemplateVisualState(GroupName = "RefreshStates", Name = "None")]
    [TemplateVisualState(GroupName = "RefreshStates", Name = "False")]
    [TemplateVisualState(GroupName = "RefreshStates", Name = "True")]        
    public class RefreshIndicator : Control
    {        
        public RefreshIndicator()
        {
            DefaultStyleKey = typeof(RefreshIndicator);
        }
        
        #region variable

        LongListSelector lls;
        ScrollViewer sv;
        bool viewportChanged;
        public event OnCompression Compression;

        double PullDistance;
        double PullFraction;

        int offsetLLS = -100;
        int offsetSV = 100;
        double manipulationStart = 0;
        double manipulationEnd = 0;

        #endregion

        #region Compression
        public enum EnoughType { None, True, False };
        public enum CompressionType { Top, Bottom, Left, Right };
        public class CompressionEventArgs : EventArgs
        {
            public CompressionType Type { get; protected set; }

            public CompressionEventArgs(CompressionType type)
            {
                this.Type = type;
            }
        }
        public delegate void OnCompression(object sender, CompressionEventArgs e);
        #endregion

        #region DependencyProperty ScrollBind
        public UIElement ScrollBind
        {
            get { return (UIElement)GetValue(ScrollBindProperty); }
            set { SetValue(ScrollBindProperty, value); }
        }

        public static readonly DependencyProperty ScrollBindProperty =
            DependencyProperty.Register("ScrollBind", typeof(UIElement), typeof(RefreshIndicator),
            new PropertyMetadata(null, new PropertyChangedCallback(OnScrollBindChanged)));

        private static void OnScrollBindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null || e.NewValue == e.OldValue || (d as RefreshIndicator) == null) return;
            RefreshIndicator indicator = d as RefreshIndicator;
            if (indicator.lls != null) indicator.UnBindLLS();
            if (indicator.sv != null) indicator.UnBindSV();
            if (e.NewValue as LongListSelector != null)
            {
                indicator.BindLLS(e.NewValue as LongListSelector);
            }
            else if (e.NewValue as ScrollViewer != null)
            {
                indicator.BindSV(e.NewValue as ScrollViewer);
            }            
        }

        private void BindLLS(LongListSelector element)
        {
            this.lls = element;
            this.lls.ManipulationStateChanged += lls_ManipulationStateChanged;
            this.lls.MouseLeftButtonDown += lls_MouseLeftButtonDown;
            this.lls.MouseMove += lls_MouseMove;
            this.lls.ItemRealized += OnViewportChanged;
            this.lls.ItemUnrealized += OnViewportChanged;
        }
        private void UnBindLLS()
        {
            if (this.lls != null)
            {
                this.lls.ManipulationStateChanged -= lls_ManipulationStateChanged;
                this.lls.MouseLeftButtonDown -= lls_MouseLeftButtonDown;
                this.lls.MouseMove -= lls_MouseMove;
                this.lls.ItemRealized -= OnViewportChanged;
                this.lls.ItemUnrealized -= OnViewportChanged;
            }
        }
        private void BindSV(ScrollViewer element)
        {
            this.sv = element;
            this.sv.MouseMove += SV_MouseMove;
            this.sv.MouseLeftButtonUp += SV_MouseUp;
        }
        private void UnBindSV()
        {
            if (this.sv != null)
            {
                this.sv.MouseMove -= SV_MouseMove;
                this.sv.MouseLeftButtonUp -= SV_MouseUp;
            }
        }


        ///////////// SV
        private void SV_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UIElement content = (UIElement)this.sv.Content;
            CompositeTransform ct = content.RenderTransform as CompositeTransform;
            if (ct != null)
            {
                if (ct.TranslateY > this.offsetSV)
                {
                    this.PullDistance = ct.TranslateY;
                    this.PullFraction = 1.0;
                    this.Enough = EnoughType.True;
                }
                else if (ct.TranslateY > 0)
                {
                    this.PullDistance = ct.TranslateY;
                    double threshold = this.offsetSV;
                    this.PullFraction = this.offsetSV == 0.0 ? 1.0 : Math.Min(1.0, ct.TranslateY / this.offsetSV);
                    this.Enough = EnoughType.False;
                }
                else
                {
                    this.PullDistance = 0;
                    this.PullFraction = 0;
                    this.Enough = EnoughType.None;
                }
            }
        }
        private void SV_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UIElement content = (UIElement)this.sv.Content;
            CompositeTransform ct = content.RenderTransform as CompositeTransform;
            if (ct != null)
            {
                this.Enough = EnoughType.None;
                this.PullDistance = 0;
                this.PullFraction = 0;

                if (ct.TranslateY >= offsetSV && Compression != null)
                {
                    Compression(this, new CompressionEventArgs(CompressionType.Top));
                }
            }
        }

        ///////////// LLS
        private void OnViewportChanged(object sender, ItemRealizationEventArgs e)
        {
            viewportChanged = true;
        }
        private void lls_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(null);
            manipulationStart = pos.Y;
        }
        private void lls_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            manipulationEnd = e.GetPosition(null).Y;
            var value = manipulationStart - manipulationEnd;

            if (!viewportChanged)
            {
                if (value < offsetLLS)
                {                    
                    this.Enough = EnoughType.True;
                }
                else if (value < 0 && value >= offsetLLS)
                {                    
                    this.Enough = EnoughType.False;
                }
                else if (value >= 0)
                {
                    this.Enough = EnoughType.None;
                }
            }
        }
        private void lls_ManipulationStateChanged(object sender, EventArgs e)
        {
            if (manipulationStart != 0 && lls.ManipulationState == ManipulationState.Animating)
            {
                var total = manipulationStart - manipulationEnd;

                if (!viewportChanged && Compression != null)
                {                    
                    if (total < offsetLLS)
                        Compression(this, new CompressionEventArgs(CompressionType.Top));
                    //else if (total > 0) 
                    //    Compression(this, new CompressionEventArgs(CompressionType.Bottom));
                }
            }
            else
                viewportChanged = false;
            this.Enough = EnoughType.None;
        }
        #endregion

        #region DependencyProperty Enough

        public EnoughType Enough
        {
            get { return (EnoughType)GetValue(EnoughProperty); }
            set { SetValue(EnoughProperty, value); }
        }

        public static readonly DependencyProperty EnoughProperty =
            DependencyProperty.Register("Enough", typeof(EnoughType), typeof(RefreshIndicator),
            new PropertyMetadata(EnoughType.None, new PropertyChangedCallback(OnEnoughChanged)));

        private static void OnEnoughChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as RefreshIndicator;
            if (control == null || e.NewValue == null || e.NewValue == e.OldValue) return;
            control.UpdateVisualState(e.NewValue);         
        }

        private void UpdateVisualState(object state)
        {
            switch ((EnoughType)state)
            {
                case (EnoughType.None):
                    {
                        VisualStateManager.GoToState(this, "None", true);
                        break;
                    }
                case (EnoughType.True):
                    {
                        VisualStateManager.GoToState(this, "True", true);
                        break;
                    }
                case (EnoughType.False):
                    {
                        VisualStateManager.GoToState(this, "False", true);
                        break;
                    }
            }
        }

        #endregion

        public ImageSource SourceImage
        {
            get { return (ImageSource)GetValue(SourceImageProperty); }
            set { SetValue(SourceImageProperty, value); }
        }                
        public static readonly DependencyProperty SourceImageProperty = DependencyProperty.RegisterAttached(
           "SourceImage", typeof(ImageSource), typeof(ButtonImage),
           new PropertyMetadata(new BitmapImage(new Uri("NTD.Controls;component/Resources/RefreshIndicator.png", UriKind.Relative)))
           );
    }
}
