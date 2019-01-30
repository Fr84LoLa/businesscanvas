using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ArcelorMittal.OptimWorks
{
    internal static class DependencyObjectExtensions
    {
        /// <summary>
        /// function to retrieve in visual tree the first child of type T
        /// </summary>
        internal static T GetVisualChild<T>(this DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        /// <summary>
        /// function to retrieve in visual tree all visual children
        /// </summary>
        internal static IEnumerable<Visual> GetVisualChildren(this DependencyObject parent) 
        {
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                yield return v;
                foreach (var child in GetVisualChildren(v))
                {
                    yield return child;
                }
            }
        }
    }
}
