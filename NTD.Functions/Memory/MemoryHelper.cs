using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace NTD.Functions
{
    public static class MemoryHelper
    {
        public static void DetachDependencyProperty(this DependencyObject obj)
        {
            foreach (FieldInfo field in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field.FieldType == typeof(DependencyProperty))
                {
                    ClearValue(obj, field.GetValue(obj) as DependencyProperty);
                }
            }
        }
        private static void ClearValue(DependencyObject oObject, DependencyProperty oProperty)
        {
            object localValue = oObject.ReadLocalValue(oProperty);
            if (localValue != DependencyProperty.UnsetValue) oObject.ClearValue(oProperty);
        }

        public static void RemoveCurrentBackEntry(this PhoneApplicationPage page)
        {
            var entry = page.NavigationService.BackStack.FirstOrDefault();
            string name = page.Name;
            if (entry != null && entry.Source.OriginalString.Contains(name))
            {
                Debug.WriteLine("Remove Page: " + name);
                page.NavigationService.RemoveBackEntry();
            }
        }
        public static void DisposeImage(this Image image)
        {
            try
            {
                if (image != null)
                {
                    var bitmap = image.Source as BitmapImage;
                    if (bitmap != null)
                    {
                        bitmap.UriSource = null;
                        bitmap.DisposeImage();
                    }
                    image.Source = null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Dispose Image exception: " + e.Message);
            }
        }
        public static void DisposeImage(this BitmapImage img)
        {
            if (img != null)
            {
                try
                {
                    Uri uri = new Uri("NTD.Functions;component/Resources/oxo.png", UriKind.Relative);
                    StreamResourceInfo sr = Application.GetResourceStream(uri);
                    using (Stream stream = sr.Stream)
                    {
                        img.DecodePixelWidth = 1; //This is essential!
                        img.SetSource(stream);
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("ImageDispose FAILED " + e.Message);
                }
            }
        }
    }   
}
