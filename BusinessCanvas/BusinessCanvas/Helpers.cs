using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoLaSoft.Controls.BusinessCanvas
{
    using System.Diagnostics;
    using System.Windows;

    static class Helpers
    {

        internal static void DebugWriteOrTrace(DependencyPropertyChangedEventArgs e)
        {
#if DEBUG || TRACE
            var messageHasChanged = $"{e.Property} has changed from {e.OldValue} to {e.NewValue}";
#if DEBUG
            Debug.WriteLine(messageHasChanged);
#else
#if TRACE
            Trace.TraceInformation(messageHasChanged);
#endif
#endif
#endif
        }

    }
}
