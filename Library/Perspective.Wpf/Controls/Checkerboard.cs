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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// Draws a checkerboard.
    /// </summary>
    public class Checkerboard : FrameworkElement
    {
        /// <summary>
        /// Gets or sets the checkboard row count.
        /// The default value is 8.
        /// </summary>
        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        /// <summary>
        /// Identifies the RowCount dependency property.
        /// </summary>
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register(
                "RowCount", 
                typeof(int), 
                typeof(Checkerboard), 
                new FrameworkPropertyMetadata(8));

        /// <summary>
        /// Gets or sets the checkboard column count.
        /// The default value is 8.
        /// </summary>
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        /// <summary>
        /// Identifies the ColumnCount dependency property.
        /// </summary>
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register(
                "ColumnCount", 
                typeof(int), 
                typeof(Checkerboard), 
                new FrameworkPropertyMetadata(8));


        /// <summary>
        /// Gets or sets the checkboard cell side length, in DIP.
        /// The default value is 25.
        /// </summary>
        public double CellLength
        {
            get { return (double)GetValue(CellLengthProperty); }
            set { SetValue(CellLengthProperty, value); }
        }

        /// <summary>
        /// Identifies the CellLength dependency property.
        /// </summary>
        public static readonly DependencyProperty CellLengthProperty =
            DependencyProperty.Register(
                "CellLength", 
                typeof(double), 
                typeof(Checkerboard), 
                new FrameworkPropertyMetadata(25.0));

        /// <summary>
        /// Gets or sets the checkboard light cell brush.
        /// The default value is Brushes.Beige.
        /// </summary>
        public Brush LightFill
        {
            get { return (Brush)GetValue(LightFillProperty); }
            set { SetValue(LightFillProperty, value); }
        }

        /// <summary>
        /// Identifies the LightFill dependency property.
        /// </summary>
        public static readonly DependencyProperty LightFillProperty =
            DependencyProperty.Register(
                "LightFill", 
                typeof(Brush), 
                typeof(Checkerboard), 
                new UIPropertyMetadata(Brushes.Beige));

        /// <summary>
        /// Gets or sets the checkboard dark cell brush.
        /// The default value is Brushes.Black.
        /// </summary>
        public Brush DarkFill
        {
            get { return (Brush)GetValue(DarkFillProperty); }
            set { SetValue(DarkFillProperty, value); }
        }

        /// <summary>
        /// Identifies the DarkFill dependency property.
        /// </summary>
        public static readonly DependencyProperty DarkFillProperty =
            DependencyProperty.Register(
                "DarkFill", 
                typeof(Brush), 
                typeof(Checkerboard), 
                new UIPropertyMetadata(
                    Brushes.Black, 
                    DarkFillPropertyChanged));

        private Pen _darkPen = new Pen(Brushes.Black, 1.0);

        /// <summary>
        /// Callback called when the DarkFill property has changed.
        /// </summary>
        /// <param name="d">Sender object</param>
        /// <param name="e">Callback arguments</param>
        private static void DarkFillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Checkerboard board = (d as Checkerboard);
            board._darkPen = new Pen(board.DarkFill, 1.0);
        }
        
        /// <summary>
        /// Participates in rendering operations.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // Border
            Rect r = new Rect(0, 0, ColumnCount * CellLength, RowCount * CellLength);
            drawingContext.DrawRectangle(LightFill, _darkPen, r);

            StreamGeometry darkGeometry = new StreamGeometry();
            // StreamGeometry lightGeometry = new StreamGeometry();
            using (StreamGeometryContext darkContext = darkGeometry.Open())
            {
                // using (StreamGeometryContext lightContext = lightGeometry.Open())
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        if (((i % 2 == 0) && (j % 2 == 0))
                            || ((i % 2 == 1) && (j % 2 == 1)))
                        {
                            CreateFigure(i * CellLength, j * CellLength, darkContext);
                        }
                        //else
                        //{
                        //    CreateFigure(i * CellLength, j * CellLength, lightContext);
                        //}
                    }
                }
            }
            darkGeometry.Freeze();
            drawingContext.DrawGeometry(DarkFill, null, darkGeometry);
        }

        private void CreateFigure(double i, double j, StreamGeometryContext context)
        {
            context.BeginFigure(new Point(i, j), true, true);
            context.LineTo(new Point(i + CellLength, j), true, false);
            context.LineTo(new Point(i + CellLength, j + CellLength), true, false);
            context.LineTo(new Point(i, j + CellLength), true, false);
        }

        /// <summary>
        /// Measures the size in layout required for child elements and determines a size for the element.
        /// </summary>
        /// <param name="availableSize">The available size that this element can give to child elements.</param>
        /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(ColumnCount * CellLength, RowCount * CellLength);
        }
    }
}
