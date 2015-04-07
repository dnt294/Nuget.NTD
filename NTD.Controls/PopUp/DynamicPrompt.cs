using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Coding4Fun.Toolkit.Controls;
using Coding4Fun.Toolkit.Controls.Common;
#if WINDOWS_PHONE
using System.Windows.Controls;
#elif NETFX_CORE
using Windows.UI.Xaml;
using Windows.Foundation;
#endif

namespace NTD.Controls
{
    public class DynamicPrompt : ActionPopUpEx<object, PopUpResult>
    {
        public DynamicPrompt()
        {
            DefaultStyleKey = typeof(DynamicPrompt);
            _ButtonList = new Dictionary<ButtonPrompt, PopUpEventHandler>();            
        }

        #region ApplyTemplate
#if WINDOWS_PHONE
        public override void OnApplyTemplate()
#elif NETFX_CORE
        protected override void OnApplyTemplate()
#endif
        {
            base.OnApplyTemplate();
        }
        #endregion
                
        IDictionary<ButtonPrompt, PopUpEventHandler> _ButtonList;
        public void AddButton(string ButtonContent, PopUpEventHandler e)
        {
            ButtonPrompt button = new ButtonPrompt();
            button.Content = ButtonContent;
            ActionPopUpButtons.Add(button);                
            _ButtonList.Add(new KeyValuePair<ButtonPrompt, PopUpEventHandler>(button, e));                
            button.Click += Buttons_Click;
        }

        #region buttonClick
#if WINDOWS_PHONE
        private void Buttons_Click(object sender, RoutedEventArgs e)
#elif NETFX_CORE
        void Buttons_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        #endif        
        {
            (sender as ButtonPrompt).Click -= Buttons_Click;
            PopUpEventHandler e1 = null;
            OnCompleted(new PopUpEventArgs<object, PopUpResult> { PopUpResult = PopUpResult.Ok });
            _ButtonList.TryGetValue(sender as ButtonPrompt, out e1);
            if (e1 != null)
                e1(this, Body, e);
        }
        #endregion

        public void Show(object body, string title = null)
        {
            if (!string.IsNullOrEmpty(title))
                Title = title;
            Body = body;            
            base.Show();
        }

        #region Control Events
        #if WINDOWS_PHONE
        private void ok_Click(object sender, RoutedEventArgs e)
#elif NETFX_CORE
        void ok_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
#endif        
        {
            OnCompleted(new PopUpEventArgs<object, PopUpResult> { PopUpResult = PopUpResult.Ok });
        }
        #endregion

        #region helper methods
        private void SetIsPromptMode(bool value)
        {
            if (ActionButtonArea != null)
                ActionButtonArea.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion

        #region Dependency Properties
        /// <summary>
        /// Should control show the bottom buttons to act as a prompt.
        /// IE: Example usage would be if control wanted to be used in control on a pivot rather than a popup.
        /// </summary>
        public object Body
        {
            get { return GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Body.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register("Body", typeof(object), typeof(DynamicPrompt), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DynamicPrompt), new PropertyMetadata(null));
        #endregion
    }
}
