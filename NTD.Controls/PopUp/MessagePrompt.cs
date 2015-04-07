using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_PHONE
using System.Windows.Controls;
#elif NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif
using System.Windows;
using Coding4Fun.Toolkit.Controls;
using Coding4Fun.Toolkit.Controls.Common;


namespace NTD.Controls
{
    public class MessagePrompt : ActionPopUpEx<object, PopUpResult>
    {
        public MessagePrompt()
        {
            DefaultStyleKey = typeof(MessagePrompt);
            okButton = new ButtonPrompt();
            okButton.Click += okButton_Click;
            ActionPopUpButtons.Add(okButton);
        }

#if WINDOWS_PHONE
        public override void OnApplyTemplate()
#elif NETFX_CORE
        protected override void OnApplyTemplate()
#endif
        {
            base.OnApplyTemplate();
        }
        ButtonPrompt okButton;
        #if WINDOWS_PHONE
        private void okButton_Click(object sender, RoutedEventArgs e)
        #elif NETFX_CORE
        void okButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        #endif        
        {
            okButton.Click -= okButton_Click;
            OnCompleted(new PopUpEventArgs<object, PopUpResult> { PopUpResult = PopUpResult.Ok });
        }
        public void Show(string body, string title = null, string ButtonMessage = "OK")
        {
            if (!string.IsNullOrEmpty(title))
                Title = title;
            Body = body;
            okButton.Content = ButtonMessage;
            okButton.Width = 440;
            base.Show();
        }
      

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

        public string Body
        {
            get { return (string)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Body.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register("Body", typeof(string), typeof(MessagePrompt), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MessagePrompt), new PropertyMetadata(null));
        #endregion
    }
}
