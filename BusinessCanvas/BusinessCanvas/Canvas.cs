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
            if (d is FrameworkElement fe && !fe.IsInitialized)
            {
                (e.NewValue as Layout).Parent = fe;
                fe.Loaded += Fe_Loaded;
            }
        }

        private static void Fe_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.IsInitialized)
            {
                fe.Loaded -= Fe_Loaded;
                //var children = fe.GetChildren().As<FrameworkElement>();
                Action<DependencyObject, DependencyProperty, Func<DependencyObject, double>> coerceChild = (d, dp, get) =>
                {
                    if (!double.IsNaN(get(d)))
                    {
                        d.CoerceValue(dp);
                    }
                };
                coerceChild(fe,XProperty, GetX);
                coerceChild(fe,YProperty, GetY);
                coerceChild(fe,WProperty, GetW);
                coerceChild(fe,HProperty, GetH);
                //Action < DependencyProperty, Func<DependencyObject, double>> coerceChildren = (dp,get) => 
                //{
                //    var notNaNchildren = children.Where(d => !double.IsNaN(get(d)));
                //    foreach (var child in notNaNchildren)
                //    {
                //        child.CoerceValue(dp);
                //    }
                //};
                //coerceChildren(XProperty, GetX);
                //coerceChildren(YProperty, GetY);
                //coerceChildren(WProperty, GetW);
                //coerceChildren(HProperty, GetH);
            }
        }

        #endregion

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
            if (d is FrameworkElement fe && !fe.IsInitialized) return baseValue;

            var layout = GetLayout(d);
            var value_to_set = (double)baseValue;
            if (layout != null)
            {
                value_to_set = layout.Parent.ActualWidth * value_to_set / layout.xMax;
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
            if (d is FrameworkElement fe && !fe.IsInitialized) return baseValue;

            var layout = GetLayout(d);
            var value_to_set = (double)baseValue;
            if (layout != null)
            {
                value_to_set = layout.Parent.ActualHeight * value_to_set / layout.yMax;
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
            if (d is FrameworkElement fe && !fe.IsInitialized) return baseValue;

            var layout = GetLayout(d);
            var value_to_set = (double)baseValue;
            if (layout != null)
            {
                value_to_set = layout.Parent.ActualWidth * value_to_set / layout.xMax;
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
            if (d is FrameworkElement fe && !fe.IsInitialized) return baseValue;

            var layout = GetLayout(d);
            var value_to_set = (double)baseValue;
            if (layout != null)
            {
                value_to_set = layout.Parent.ActualHeight * value_to_set / layout.yMax;
            }
            d.SetValue(System.Windows.FrameworkElement.HeightProperty, value_to_set);
            return baseValue;
        }

        #endregion
    }
}
