using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LoLaSoft.Extensions
{
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// function to retrieve in visual tree the first child of type T
        /// </summary>
        public static T GetFirstVisualChild<T>(this DependencyObject parent) 
            where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetFirstVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        /// <summary>
        /// function to retrieve in visual tree all children
        /// </summary>
        public static IEnumerable<DependencyObject> GetChildren(this DependencyObject obj) 
        {
            int childrencount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childrencount; i++)
            {
                var v = VisualTreeHelper.GetChild(obj, i);
                yield return v;
                foreach (var child in GetChildren(v))
                {
                    yield return child;
                }
            }
        }
        /// <summary>
        /// function to filter an enumeration of Dependency Objects on a specific T type
        /// </summary>
        /// <typeparam name="T">Type inheriting from DependencyObject</typeparam>
        /// <param name="objects">an enumeration of Dependency Objects</param>
        /// <returns>an enumeration of T inheriting from DependencyObject</returns>
        public static IEnumerable<DependencyObject> As<T>(this IEnumerable<DependencyObject> objects)
            where T : DependencyObject
        {
            return objects.OfType<T>();
        }

    }
}
