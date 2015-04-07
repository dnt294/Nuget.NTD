using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace NTD.Test.WP81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void ButtonImage_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void popup_click(object sender, RoutedEventArgs e)
        {
            NTD.Controls.DynamicPrompt pr = new Controls.DynamicPrompt();
            pr.AddButton("left", leftbutton_click);
            pr.AddButton("right", null);
            pr.Show("abc", "title");
            //NTD.Controls.MessagePrompt pr = new Controls.MessagePrompt();
            //pr.Show("“Bến xe phải bố trí đủ phương tiện để nhân dân được về quê ăn Tết. Nếu thiếu, lấy xe của Giám đốc bến, thậm chí lấy cả xe của Tổng Giám đốc Công ty vận tải Hà Nội đưa hành khách về quê”, Phó Thủ tướng Nguyễn Xuân Phúc nói.", "title", "OK");
        }

        private void leftbutton_click(object sender, object parser, RoutedEventArgs PopUpEvent)
        {
            int a = 1;
        }
    }
}
