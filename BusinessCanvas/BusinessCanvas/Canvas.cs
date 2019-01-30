using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LoLaSoft.Controls.BusinessCanvas
{
    using LoLaSoft.Extensions;

    public class Canvas
    {
        #region Layout property

        public static Layout GetLayout(DependencyObject obj)
        {
            return (Layout)obj.GetValue(LayoutProperty);
        }

        public static void SetLayout(DependencyObject obj, Layout value)
        {
            obj.SetValue(LayoutProperty, value);
        }

        // Using a DependencyProperty as the backing store for Layout.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutProperty =
            DependencyProperty.RegisterAttached("Layout", typeof(Layout), typeof(Canvas),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnLayoutChanged)));

        private static void OnLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Layout layout && !layout.IsConfigured)
            {
                if (layout.xLength == 0)
                    throw new System.Configuration.ConfigurationErrorsException($"{nameof(layout.xMin)} or {nameof(layout.xMin)} should be set");
                if (layout.yLength == 0)
                    throw new System.Configuration.ConfigurationErrorsException($"{nameof(layout.yMin)} or {nameof(layout.yMin)} should be set");
            }
            if (d is FrameworkElement fe && !fe.IsInitialized)
            {
                fe.Loaded += Fe_Loaded;
            }
        }

        private static void CoerceChild(DependencyObject d, DependencyProperty dp, Func<DependencyObject, double> get)
        {
            if (!double.IsNaN(get(d)))
            {
                d.CoerceValue(dp);
            }
        }

        private static void Fe_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.IsInitialized)
            {
                fe.Loaded -= Fe_Loaded;
                CoerceChild(fe, XProperty, GetX);
                CoerceChild(fe, YProperty, GetY);
                CoerceChild(fe, WProperty, GetW);
                CoerceChild(fe, HProperty, GetH);

                // init canvas SizeChanged handler
                if (fe is ItemsControl itemsControl)
                {
                    var panel = itemsControl.GetFirstVisualChild<System.Windows.Controls.Panel>();
                    panel.SizeChanged += Panel_SizeChanged;
                }
            }
        }

        private static void Panel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var panel = sender as Panel;
            foreach (var item in panel.GetChildren())
            {
                if (e.HeightChanged)
                {
                    CoerceChild(item, YProperty, GetY);
                    CoerceChild(item, HProperty, GetH);
                }
                if (e.WidthChanged)
                {
                    CoerceChild(item, XProperty, GetX);
                    CoerceChild(item, WProperty, GetW);
                }
            }
        }

        #endregion

        static System.Windows.Controls.Canvas VerifAndGetCanvas(DependencyObject d)
        {
            if (d is FrameworkElement fe && fe.IsInitialized)
            {
                var parent = fe.Parent ?? fe.TemplatedParent;
                if (parent == null) return null;
                var canvas = parent.GetFirstVisualChild<System.Windows.Controls.Canvas>();
                return canvas;
            }
            else
            {
                return null;
            }
        }

        #region X property

        public static double GetX(DependencyObject obj)
        {
            return (double)obj.GetValue(XProperty);
        }

        public static void SetX(DependencyObject obj, double value)
        {
            obj.SetValue(XProperty, value);
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.RegisterAttached("X", typeof(double), typeof(Canvas),
                new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnXChanged), new CoerceValueCallback(OnXCoerce)));

        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(e.Property);
        }

        private static object OnXCoerce(DependencyObject d, object baseValue)
        {
            var canvas = VerifAndGetCanvas(d);
            if (canvas == null) return baseValue;

            var layout = GetLayout(d);
            var value_to_set = (double)baseValue;
            if (layout != null)
            {
                value_to_set = canvas.ActualWidth * value_to_set / layout.xLength;
            }
            d.SetValue(System.Windows.Controls.Canvas.LeftProperty, value_to_set);
            return baseValue;
        }

        #endregion

        #region Y property

        public static double GetY(DependencyObject obj)
        {
            return (double)obj.GetValue(YProperty);
        }

        public static void SetY(DependencyObject obj, double value)
        {
            obj.SetValue(YProperty, value);
        }

        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.RegisterAttached("Y", typeof(double), typeof(Canvas),
                new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnYChanged), new CoerceValueCallback(OnYCoerce)));

        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(e.Property);
        }

        private static object OnYCoerce(DependencyObject d, object baseValue)
        {
            var canvas = VerifAndGetCanvas(d);
            if (canvas == null) return baseValue;

            var layout = GetLayout(d);
            var value_to_set = (double)baseValue;
            if (layout != null)
            {
                value_to_set = canvas.ActualHeight * value_to_set / layout.yLength;
            }
            d.SetValue(System.Windows.Controls.Canvas.BottomProperty, value_to_set);
            return baseValue;
        }

        #endregion

        #region W property

        public static double GetW(DependencyObject obj)
        {
            return (double)obj.GetValue(WProperty);
        }

        public static void SetW(DependencyObject obj, double value)
        {
            obj.SetValue(WProperty, value);
        }

        // Using a DependencyProperty as the backing store for W.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WProperty =
            DependencyProperty.RegisterAttached("W", typeof(double), typeof(Canvas),
                new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnWChanged), new CoerceValueCallback(OnWCoerce)));

        private static void OnWChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(e.Property);
        }

        private static object OnWCoerce(DependencyObject d, object baseValue)
        {
            var canvas = VerifAndGetCanvas(d);
            if (canvas == null) return baseValue;

            var layout = GetLayout(d);
            var value_to_set = (double)baseValue;
            if (layout != null)
            {
                value_to_set = canvas.ActualWidth * value_to_set / layout.xLength;
            }
            d.SetValue(System.Windows.FrameworkElement.WidthProperty, value_to_set);
            return baseValue;
        }

        #endregion
        
        #region H property

        public static double GetH(DependencyObject obj)
        {
            return (double)obj.GetValue(HProperty);
        }

        public static void SetH(DependencyObject obj, double value)
        {
            obj.SetValue(HProperty, value);
        }

        // Using a DependencyProperty as the backing store for H.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HProperty =
            DependencyProperty.RegisterAttached("H", typeof(double), typeof(Canvas),
                new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnHChanged), new CoerceValueCallback(OnHCoerce)));

        private static void OnHChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(e.Property);
        }

        private static object OnHCoerce(DependencyObject d, object baseValue)
        {
            var canvas = VerifAndGetCanvas(d);
            if (canvas == null) return baseValue;

            var layout = GetLayout(d);
            var value_to_set = (double)baseValue;
            if (layout != null)
            {
                value_to_set = canvas.ActualHeight * value_to_set / layout.yLength;
            }
            d.SetValue(System.Windows.FrameworkElement.HeightProperty, value_to_set);
            return baseValue;
        }

        #endregion
    }
}
