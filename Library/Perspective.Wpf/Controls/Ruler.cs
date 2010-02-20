//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System;
using System.Windows;
using Perspective.Wpf;
using System.Windows.Media;
using System.Globalization;
using Perspective.Core.Wpf;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// A Ruler element, that supports centimeters and inches.
    /// </summary>
    public class Ruler : FrameworkElement
    {
        static Ruler()
        {
            HeightProperty.OverrideMetadata(
                typeof(Ruler),
                new FrameworkPropertyMetadata(40.0));
        }

        /// <summary>
        /// Gets or sets the length of the ruler.
        /// Default value is 10.0.
        /// </summary>
        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        /// <summary>
        /// Identifies the Length dependency property.
        /// </summary>
        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(
                "Length", 
                typeof(double), 
                typeof(Ruler), 
                new UIPropertyMetadata(10.0));

        /// <summary>
        /// Gets or sets the unit of the ruler.
        /// Default value is Unit.Cm.
        /// </summary>
        public Unit Unit
        {
            get { return (Unit)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }
                
        /// <summary>
        /// Identifies the Unit dependency property.
        /// </summary>
        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register(
                "Unit", 
                typeof(Unit), 
                typeof(Ruler), 
                new UIPropertyMetadata(Unit.Cm));

        private const double _segmentHeight = 20.0;
        private Pen _p = new Pen(Brushes.Black, 1.0);
        private Pen _pThin = new Pen(Brushes.Black, 0.5);
        private Pen _pBorder = new Pen(Brushes.Gray, 1.0);

        /// <summary>
        /// Participates in rendering operations.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            double xDest;
            if (Unit == Unit.Cm)
            {
                xDest = DipHelper.CmToDip(Length);
            }
            else
            {
                xDest = DipHelper.InchToDip(Length);
            }
            drawingContext.DrawRectangle(null, _pBorder, new Rect(new Point(0.0, 0.0), new Point(xDest, this.Height)));

            for (double dUnit = 0.0; dUnit <= Length; dUnit++)
            {
                double d;
                if (Unit == Unit.Cm)
                {
                    d = DipHelper.CmToDip(dUnit);
                    if (dUnit < Length)
                    {
                        for (int i = 1; i <= 9; i++)
                        {
                            if (i != 5)
                            {
                                double dMm = DipHelper.CmToDip(dUnit + 0.1 * i);
                                drawingContext.DrawLine(_pThin, new Point(dMm, this.Height), new Point(dMm, this.Height - _segmentHeight / 3.0));
                            }
                        }
                        double dMiddle = DipHelper.CmToDip(dUnit + 0.5);
                        drawingContext.DrawLine(_p, new Point(dMiddle, this.Height), new Point(dMiddle, this.Height - _segmentHeight * 2.0 / 3.0));
                    }
                }
                else
                {
                    d = DipHelper.InchToDip(dUnit);
                    if (dUnit < Length)
                    {
                        double dQuarter = DipHelper.InchToDip(dUnit + 0.25);
                        drawingContext.DrawLine(_pThin, new Point(dQuarter, this.Height), new Point(dQuarter, this.Height - _segmentHeight / 3.0));
                        double dMiddle = DipHelper.InchToDip(dUnit + 0.5);
                        drawingContext.DrawLine(_p, new Point(dMiddle, this.Height), new Point(dMiddle, this.Height - 0.5 * _segmentHeight * 2.0 / 3.0));
                        double d3Quarter = DipHelper.InchToDip(dUnit + 0.75);
                        drawingContext.DrawLine(_pThin, new Point(d3Quarter, this.Height), new Point(d3Quarter, this.Height - 0.25 * _segmentHeight / 3.0));
                    }
                }
                drawingContext.DrawLine(_p, new Point(d, this.Height), new Point(d, this.Height - _segmentHeight));

                if ((dUnit != 0.0) && (dUnit < Length))
                {
                    FormattedText ft = new FormattedText(
                        dUnit.ToString(CultureInfo.CurrentCulture),
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Arial"),
                        DipHelper.PtToDip(10.0),
                        Brushes.Black);
                    ft.SetFontWeight(FontWeights.Regular);
                    ft.TextAlignment = TextAlignment.Center;
                    drawingContext.DrawText(ft, new Point(d, this.Height - _segmentHeight - ft.Height));
                }
            }
        }

        /// <summary>
        /// Measures an instance during the first layout pass prior to arranging it.
        /// </summary>
        /// <param name="availableSize">A maximum Size to not exceed.</param>
        /// <returns>The maximum Size for the instance.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size desiredSize;
            if (Unit == Unit.Cm)
            {
                desiredSize = new Size(DipHelper.CmToDip(Length), this.Height);
            }
            else
            {
                desiredSize = new Size(DipHelper.InchToDip(Length), this.Height);
            }
            return desiredSize;
        }
    }
}
