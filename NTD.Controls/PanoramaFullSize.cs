using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NTD.Controls
{
    public class PanoramaFullSize : Panorama
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            availableSize.Width += 48;
            return base.MeasureOverride(availableSize);
        }
    }
}
