using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_PHONE
using System.Windows.Controls;
using System.Windows.Media;
#elif NETFX_CORE
using Windows.UI.Xaml.Controls;
#endif
namespace NTD.Controls
{
    public class ButtonPrompt : Button
    {
        /// <summary>
        /// If BlueDefault, then Normal of button is blue, otherwise it's gray
        /// </summary>
        /// <param name="BlueDefault"></param>
        public ButtonPrompt(bool BlueDefault = false)
        {
            DefaultStyleKey = typeof(ButtonPrompt);
        }
    }
}
