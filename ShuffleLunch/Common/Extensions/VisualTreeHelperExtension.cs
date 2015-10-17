using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ShuffleLunch.Common.Extensions
{
    public static class VisualTreeHelperExtension
    {
        public static T FindFirstChild<T>(this DependencyObject d)
            where T : DependencyObject
        {
            // fast-pass
            if (d is T) return (T)d;
            var q = new Queue<DependencyObject>();
            q.Enqueue(d);
            while (q.Count > 0)
            {
                var e = q.Dequeue();
                var n = VisualTreeHelper.GetChildrenCount(e);
                for (var i = 0; i < n; ++i)
                {
                    var c = VisualTreeHelper.GetChild(e, i);
                    if (c is T) return (T) c;
                    q.Enqueue(c);
                }
            }
            return null;
        }

        public static T FindAncestor<T>(this DependencyObject o)
            where T : DependencyObject
        {
            var e = o;
            while (e != null)
            {
                var p = VisualTreeHelper.GetParent(e);
                if (p is T) return (T)p;
                e = p;
            }
            return null;
        }
    }
}
