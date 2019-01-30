using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoLaSoft.Controls.BusinessCanvas
{
    public class Layout : DependencyObject
    {
        internal FrameworkElement Parent { get; set; }

        #region xMax

        public double xMax
        {
            get { return (double)GetValue(xMaxProperty); }
            set { SetValue(xMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for xMax.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xMaxProperty =
            DependencyProperty.Register("xMax", typeof(double), typeof(Layout),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnxMaxChanged), new CoerceValueCallback(OnxMaxCoerce)));

        private static void OnxMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(e.Property);
        }

        private static object OnxMaxCoerce(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        #endregion

        #region yMax

        public double yMax
        {
            get { return (double)GetValue(yMaxProperty); }
            set { SetValue(yMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for yMax.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yMaxProperty =
            DependencyProperty.Register("yMax", typeof(double), typeof(Layout),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnyMaxChanged), new CoerceValueCallback(OnyMaxCoerce)));

        private static void OnyMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(e.Property);
        }

        private static object OnyMaxCoerce(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        #endregion

    }
}
