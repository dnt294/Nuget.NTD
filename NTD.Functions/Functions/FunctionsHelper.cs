using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NTD.Functions
{
    public class GlobalNavigation
    {
        private static PhoneApplicationFrame _frame;
        public static void AttachFrame(PhoneApplicationFrame frame)
        {
            _frame = frame;
        }
        public static void Navigate(string uri)
        {
            Uri url = new Uri(uri, UriKind.RelativeOrAbsolute);
            _frame.Navigate(url);
        }
        public static void RemoveBackEntry()
        {
            _frame.RemoveBackEntry();
        }
        public static void GoBack()
        {
            _frame.GoBack();
        }
    }

    public static class WebFunctions
    {
        /// <summary>
        /// Nếu muốn có thông báo in ra khi download có error
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public static Task<string> DownloadStringTask(this WebClient client, string uri, string ErrorMessage = null)
        {
            var url = new Uri(uri,UriKind.RelativeOrAbsolute);
            var tcs = new TaskCompletionSource<string>();
            client.DownloadStringCompleted += (sender, e) =>
                {
                    if (e.Error != null)
                    {
                        if (!string.IsNullOrEmpty(ErrorMessage))
                            System.Diagnostics.Debug.WriteLine(ErrorMessage + e.Error.Message);
                        tcs.SetResult(null);
                    }
                    else
                    {
                        tcs.SetResult(e.Result);                        
                    }
                };
            client.DownloadStringAsync(url);
            return tcs.Task;
        }
        public static Task<string> UploadStringTask(this WebClient client, string uri,string data,string method = null)
        {
            var url = new Uri(uri, UriKind.RelativeOrAbsolute);
            var tcs = new TaskCompletionSource<string>();
            client.UploadStringCompleted += (s, e) =>
                {
                    if (e.Error != null)
                        tcs.SetResult(null);
                    else
                        tcs.SetResult(e.Result);
                };            
            if (method != null)
                client.UploadStringAsync(url, method, data);
            else client.UploadStringAsync(url, data);
            return tcs.Task;
        }
    }
}
