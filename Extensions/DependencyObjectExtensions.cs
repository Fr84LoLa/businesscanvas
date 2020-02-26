using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LoLaSoft.Extensions
{
    internal static class DependencyObjectExtensions
    {
        /// <summary>
        /// function to retrieve in visual tree the first child of type T
        /// </summary>
        internal static T GetFirstVisualChild<T>(this DependencyObject parent) 
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
        /// function to recusurvely retrieve in visual tree all children
        /// </summary>
        internal static IEnumerable<DependencyObject> GetImmediateChildren(this DependencyObject obj) 
        {
            return obj.GetChildren(false);
        }
        /// <summary>
        /// function to recusurvely retrieve in visual tree all children
        /// </summary>
        internal static IEnumerable<DependencyObject> GetRecursiveChildren(this DependencyObject obj)
        {
            return obj.GetChildren(true);
        }
        /// <summary>
        /// function to retrieve in visual tree children with option recursive ou immediate
        /// </summary>
        private static IEnumerable<DependencyObject> GetChildren(this DependencyObject obj, bool recursive)
        {
            int childrencount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childrencount; i++)
            {
                var v = VisualTreeHelper.GetChild(obj, i);
                yield return v;
                if (recursive)
                {
                    foreach (var child in GetRecursiveChildren(v))
                    {
                        yield return child;
                    }
                }
            }
        }
        /// <summary>
        /// function to filter an enumeration of Dependency Objects on a specific T type
        /// </summary>
        /// <typeparam name="T">Type inheriting from DependencyObject</typeparam>
        /// <param name="objects">an enumeration of Dependency Objects</param>
        /// <returns>an enumeration of T inheriting from DependencyObject</returns>
        internal static IEnumerable<DependencyObject> As<T>(this IEnumerable<DependencyObject> objects)
            where T : DependencyObject
        {
            return objects.OfType<T>();
        }

    }
}
