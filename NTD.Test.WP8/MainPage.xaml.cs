using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NTD.Test.WP8.Resources;
using NTD.Functions;

namespace NTD.Test.WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();            
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            WebClient client = new WebClient();
            string url = "http://mp3.hdvietpro.com/mp3_zing/home.php?type=song";
            //string result = await client.DownloadStringTask(url, "Get home 1st debug error: ");
            client.DownloadStringCompleted += client_DownloadStringCompleted;
            Uri uri = new Uri(url, UriKind.Absolute);
            client.DownloadStringAsync(uri);
            //if (string.IsNullOrEmpty(result)) return;
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string result = e.Result;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            li.IsActive = !li.IsActive;
        }

     
    }
}