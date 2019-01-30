using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SysWinCtrls = System.Windows.Controls;
using System.Windows.Media;

namespace LoLaSoft.Controls.BusinessCanvas
{
    using LoLaSoft.Extensions;

    public class Canvas
    {
        #region Private static methods

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

                var layout = GetLayout(fe);
                var itemscontrol = SysWinCtrls.ItemsControl.ItemsControlFromItemContainer(fe);
            }
        }

        private static void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var control = sender as DependencyObject;
            foreach (var item in control.GetChildren())
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

        private static bool VerifAndRetrieve(DependencyObject d, out SysWinCtrls.Canvas canvas, out Layout layout)
        {
            layout = null;
            canvas = null;

            if (d is FrameworkElement fe && fe.IsInitialized)
            {
                var parent = fe.Parent ?? fe.TemplatedParent;
                if (parent == null) return false;
                canvas = parent.GetFirstVisualChild<SysWinCtrls.Canvas>();
            }

            var itemscontrol = SysWinCtrls.ItemsControl.ItemsControlFromItemContainer(d);
            if (itemscontrol != null)
            {
                layout = GetLayout(itemscontrol);
                return (layout != null && canvas != null);
            }
            else
            {
                return false;
            }
        }

        #endregion

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
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnLayoutChanged)));

        private static void OnLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Layout layout)
            {
                // 1st, verify that layout is set on an ItemsControl 
                if (d is SysWinCtrls.ItemsControl itemscontrol)
                {
                    itemscontrol.SizeChanged += ItemsControl_SizeChanged;
                    itemscontrol.Loaded += (s,ea) => {
                        var canvas = (s as SysWinCtrls.ItemsControl).GetFirstVisualChild<SysWinCtrls.Canvas>();
                        if (canvas == null)
                        {
                            throw new InvalidOperationException("ItemsControl must have a Canvas as ItemsTemplate");
                        }
                    };
                    itemscontrol.Unloaded += (s, ea) => {
                        (s as SysWinCtrls.ItemsControl).Unloaded -= Itemscontrol_Unloaded;
                        (s as SysWinCtrls.ItemsControl).SizeChanged -= ItemsControl_SizeChanged;
                    };

                    // 2nd, verify that layout is correctly configured
                    if (!layout.IsConfigured)
                    {
                        if (layout.xLength == 0)
                            throw new System.Configuration.ConfigurationErrorsException($"{nameof(layout.xMin)} or {nameof(layout.xMin)} should be set");
                        if (layout.yLength == 0)
                            throw new System.Configuration.ConfigurationErrorsException($"{nameof(layout.yMin)} or {nameof(layout.yMin)} should be set");

                    }
                }
                else
                {
                    throw new InvalidOperationException("Layout property can only be set on a ItemsControl");
                }
            }
        }

        private static void Itemscontrol_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private static void Itemscontrol_Initialized(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            SysWinCtrls.Canvas canvas; Layout layout;
            if (!VerifAndRetrieve(d, out canvas, out layout)) return baseValue;

            var value_to_set = (double)baseValue;

            var ratio = canvas.ActualWidth / layout.xLength;

            value_to_set = (value_to_set - layout.xMin) * ratio;

            d.SetValue(SysWinCtrls.Canvas.LeftProperty, value_to_set);

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
            SysWinCtrls.Canvas canvas; Layout layout;
            if (!VerifAndRetrieve(d, out canvas, out layout)) return baseValue;

            var value_to_set = (double)baseValue;

            var ratio = canvas.ActualHeight / layout.yLength;

            value_to_set = (value_to_set - layout.yMin) * ratio;

            d.SetValue(SysWinCtrls.Canvas.BottomProperty, value_to_set);

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
            SysWinCtrls.Canvas canvas; Layout layout;
            if (!VerifAndRetrieve(d, out canvas, out layout)) return baseValue;

            var value_to_set = (double)baseValue;

            value_to_set = canvas.ActualWidth * value_to_set / layout.xLength;

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
            SysWinCtrls.Canvas canvas; Layout layout;
            if (!VerifAndRetrieve(d, out canvas, out layout)) return baseValue;

            var value_to_set = (double)baseValue;

            value_to_set = canvas.ActualHeight * value_to_set / layout.yLength;

            d.SetValue(System.Windows.FrameworkElement.HeightProperty, value_to_set);
            return baseValue;
        }

        #endregion
    }
}
