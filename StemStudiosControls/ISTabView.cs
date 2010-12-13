using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Stemstudios.UIControls
{
    public interface ISTabView
    {

        bool TabIsClosing();

        bool TabIsLosingFocus();

        void TabIsGainingFocus();

        String TabTitle();

        Image TabIcon();
    }
}
