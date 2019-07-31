using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoLaSoft.Controls.BusinessCanvas
{
    /// <summary>
    /// Represents the business layout containing min and max properties of business referential
    /// </summary>
    public class Layout : DependencyObject
    {
        #region xMin

        /// <summary>
        /// Represents the maximum value on X axis
        /// </summary>
        public double xMin
        {
            get { return (double)GetValue(xMinProperty); }
            set { SetValue(xMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for xMin.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Property representing the maximum value on X axis
        /// </summary>
        public static readonly DependencyProperty xMinProperty =
            DependencyProperty.Register("xMin", typeof(double), typeof(Layout),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnxMinChanged), new CoerceValueCallback(OnxMinCoerce)));

        private static void OnxMinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Helpers.DebugWriteOrTrace(e);
        }

        private static object OnxMinCoerce(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        #endregion

        #region yMin

        /// <summary>
        /// Represents the maximum value on Y axis
        /// </summary>
        public double yMin
        {
            get { return (double)GetValue(yMinProperty); }
            set { SetValue(yMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for yMin.  This enables animation, styling, binding, etc...
        /// <summary>
        /// Property representing the maximum value on Y axis
        /// </summary>
        public static readonly DependencyProperty yMinProperty =
            DependencyProperty.Register("yMin", typeof(double), typeof(Layout),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnyMinChanged), new CoerceValueCallback(OnyMinCoerce)));

        private static void OnyMinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Helpers.DebugWriteOrTrace(e);
        }

        private static object OnyMinCoerce(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        #endregion

        #region xMax

        /// <summary>
        /// Represents the maximum value on X axis
        /// </summary>
        public double xMax
        {
            get { return (double)GetValue(xMaxProperty); }
            set { SetValue(xMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for xMax.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Property representing the maximum value on X axis
        /// </summary>
        public static readonly DependencyProperty xMaxProperty =
            DependencyProperty.Register("xMax", typeof(double), typeof(Layout),
                new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnxMaxChanged), new CoerceValueCallback(OnxMaxCoerce)));

        private static void OnxMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Helpers.DebugWriteOrTrace(e);
        }

        private static object OnxMaxCoerce(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        #endregion

        #region yMax

        /// <summary>
        /// Represents the maximum value on Y axis
        /// </summary>
        public double yMax
        {
            get { return (double)GetValue(yMaxProperty); }
            set { SetValue(yMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for yMax.  This enables animation, styling, binding, etc...
        /// <summary>
        /// Property representing the maximum value on Y axis
        /// </summary>
        public static readonly DependencyProperty yMaxProperty =
            DependencyProperty.Register("yMax", typeof(double), typeof(Layout),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnyMaxChanged), new CoerceValueCallback(OnyMaxCoerce)));

        private static void OnyMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Helpers.DebugWriteOrTrace(e);
        }

        private static object OnyMaxCoerce(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Represents the business distance between min and max on X axis
        /// </summary>
        public double xLength
        {
            get { return xMax - xMin; }
        }

        /// <summary>
        /// Represents the business distance between min and max on Y axis
        /// </summary>
        public double yLength
        {
            get { return yMax - yMin; }
        }

        /// <summary>
        /// Determines if layout is correctly configured (X and Y length different from zero)
        /// </summary>
        public bool IsConfigured
        {
            get
            {
                if (xLength == 0)
                    throw new System.Configuration.ConfigurationErrorsException($"{nameof(xMin)} or {nameof(xMax)} should be set");
                if (yLength == 0)
                    throw new System.Configuration.ConfigurationErrorsException($"{nameof(yMin)} or {nameof(yMax)} should be set");
                return xLength != 0 && yLength != 0;
            }
        }

        #endregion
    }
}
