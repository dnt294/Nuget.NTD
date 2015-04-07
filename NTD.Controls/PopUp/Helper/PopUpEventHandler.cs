using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTD.Controls
{
#if WINDOWS_PHONE
    public delegate void PopUpEventHandler(object sender, object parser, EventArgs PopUpEvent);
#elif NETFX_CORE
    public delegate void PopUpEventHandler(object sender, object parser, Windows.UI.Xaml.RoutedEventArgs PopUpEvent);
#endif
}
