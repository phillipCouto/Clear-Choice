using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemstudios.UIControls
{
    public interface ISTabContent
    {
        /// <summary>
        /// This provides a call back to the Tab when it is about to be closed.
        /// </summary>
        /// <returns></returns>
        bool TabIsClosingCallBack();

        bool TabIsLosingFocusCallBack();
    }
}
