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
using System.Windows.Media.Media3D;
using System.Windows;

namespace Perspective.Wpf3D
{
    /// <summary>
    /// A class to manage by code the transformations and the current position of a Visual3D.
    /// </summary>
    /// <remarks>
    /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
    /// </remarks>
    public class RelativeTransformer
    {
        #region Position readonly attached dependency property

        /// <summary>
        /// Gets the Position attached dependency property.
        /// </summary>
        /// <remarks>This property is affeted only by the MoveTo and Translate methods. 
        /// It may differ from the real property if a rotation or a scaling has occured.
        /// </remarks>
        /// <param name="obj">A dependency object.</param>
        /// <returns>A Point3D object with the coordinates of the current position.</returns>
        public static Point3D GetPosition(DependencyObject obj)
        {
            return (Point3D)obj.GetValue(PositionProperty);
        }

        private static readonly DependencyPropertyKey PositionPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "Position", 
                typeof(Point3D), 
                typeof(RelativeTransformer), 
                new PropertyMetadata(new Point3D(0, 0, 0)));

        /// <summary>
        /// Identifies the Position attached dependency property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty =    
            PositionPropertyKey.DependencyProperty;

        #endregion  

        #region MatrixTransform readonly attached dependency property

        /// <summary>
        /// Gets the MatrixTransform attached dependency property.
        /// </summary>
        /// <param name="obj">A descendant dependency object.</param>
        /// <returns>The built MatrixTransform3D object.</returns>
        public static MatrixTransform3D GetMatrixTransform(DependencyObject obj)
        {
            return (MatrixTransform3D)obj.GetValue(MatrixTransformProperty);
        }

        private static readonly DependencyPropertyKey MatrixTransformPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "MatrixTransform", 
                typeof(MatrixTransform3D), 
                typeof(RelativeTransformer), 
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the MatrixTransform attached dependency property.
        /// </summary>
        public static readonly DependencyProperty MatrixTransformProperty =
            MatrixTransformPropertyKey.DependencyProperty;

        #endregion

        #region Matrix attached dependency property

        /// <summary>
        /// Gets the Matrix attached dependency property.
        /// </summary>
        /// <param name="obj">A descendant dependency object.</param>
        /// <returns>The built Matrix3D object.</returns>
        public static Matrix3D GetMatrix(DependencyObject obj)
        {
            return (Matrix3D)obj.GetValue(MatrixProperty);
        }

        private static readonly DependencyPropertyKey MatrixPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "Matrix", 
                typeof(Matrix3D), 
                typeof(RelativeTransformer), 
                new PropertyMetadata(
                    new Matrix3D()));

        /// <summary>
        /// Identifies the Matrix attached dependency property.
        /// </summary>
        public static readonly DependencyProperty MatrixProperty =
            MatrixPropertyKey.DependencyProperty;

        #endregion

        #region PreviousMatrixTransform private readonly attached dependency property

        private static readonly DependencyPropertyKey PreviousMatrixTransformPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "PreviousMatrixTransform", 
                typeof(MatrixTransform3D), 
                typeof(RelativeTransformer), 
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the PreviousMatrixTransform attached dependency property.
        /// </summary>
        private static readonly DependencyProperty PreviousMatrixTransformProperty =
            PreviousMatrixTransformPropertyKey.DependencyProperty;

        #endregion

        private void ApplyTransform(Visual3D visual)
        {
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty);

            MatrixTransform3D previousMatrixTransform = (MatrixTransform3D)visual.GetValue(MatrixTransformProperty);
            visual.SetValue(PreviousMatrixTransformPropertyKey, previousMatrixTransform);

            // new instance because MatrixTransform3D may be frozen and can't accept a new matrix
            MatrixTransform3D matrixTransform = new MatrixTransform3D(matrix);
            visual.SetValue(MatrixTransformPropertyKey, matrixTransform);
            
            if (visual.Transform != null)
            {
                if (visual.Transform is Transform3DGroup)
                {
                    Transform3DGroup transformGroup = (Transform3DGroup)visual.Transform;
                    if (!transformGroup.Children.Contains(previousMatrixTransform))
                    {
                        transformGroup.Children.Add(matrixTransform);
                    }
                    else
                    {
                        int previousIndex = transformGroup.Children.IndexOf(previousMatrixTransform);
                        transformGroup.Children[previousIndex] = matrixTransform;
                    }
                }
                else
                {
                    Transform3DGroup transformGroup = new Transform3DGroup();
                    transformGroup.Children.Add(visual.Transform);
                    transformGroup.Children.Add(matrixTransform);
                    visual.Transform = transformGroup;
                }
            }
            else
            {
                visual.Transform = matrixTransform;
            }
        }

        /// <summary>
        /// Scales a Visual3D object.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to scale.</param>
        /// <param name="v">A vector containing the x-, y- and z-axis scale factors.</param>
        public void Scale(Visual3D visual, Vector3D v)
        {
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty);
            matrix.ScaleAt(
                v,
                (Point3D)visual.GetValue(PositionProperty));
            visual.SetValue(MatrixPropertyKey, matrix);
            ApplyTransform(visual);
        }

        /// <summary>
        /// Scales a Visual3D object.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to scale.</param>
        /// <param name="scaleX">The x-axis scale factor.</param>
        /// <param name="scaleY">The y-axis scale factor.</param>
        /// <param name="scaleZ">The z-axis scale factor.</param>
        public void Scale(Visual3D visual, double scaleX, double scaleY, double scaleZ)
        {
            Scale(visual, new Vector3D(scaleX, scaleY, scaleZ));
        }

        /// <summary>
        /// Scales a Visual3D object, using the same factor for the x-, y- and z-axis.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to scale.</param>
        /// <param name="scale">The scale factor.</param>
        public void Scale(Visual3D visual, double scale)
        {
            Scale(visual, scale, scale, scale);
        }

        /// <summary>
        /// Translates a Visual3D object.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to scale.</param>
        /// <param name="v">A vector containing the x-, y- and z-axis offsets.</param>
        public void Translate(Visual3D visual, Vector3D v)
        {
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty);
            matrix.Translate(v);
            visual.SetValue(MatrixPropertyKey, matrix);

            ApplyTransform(visual);

            Point3D position = (Point3D)visual.GetValue(PositionProperty) + v;
            visual.SetValue(PositionPropertyKey, position);
        }

        /// <summary>
        /// Translates a Visual3D object. It updates the Position property.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to move.</param>
        /// <param name="offsetX">The X-axis value of the translation's offset.</param>
        /// <param name="offsetY">The Y-axis value of the translation's offset.</param>
        /// <param name="offsetZ">The Z-axis value of the translation's offset.</param>
        public void Translate(Visual3D visual, double offsetX, double offsetY, double offsetZ)
        {
            Translate(visual, new Vector3D(offsetX, offsetY, offsetZ));
        }

        /// <summary>
        /// Moves a Visual3D object to a new position. It updates the Position property.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to move.</param>
        /// <param name="p">A Point3D object designing the new position.</param>
        public void MoveTo(Visual3D visual, Point3D p)
        {
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty);
            matrix.OffsetX = p.X;
            matrix.OffsetY = p.Y;
            matrix.OffsetZ = p.Z;
            visual.SetValue(MatrixPropertyKey, matrix);

            ApplyTransform(visual);

            visual.SetValue(PositionPropertyKey, p);
        }

        /// <summary>
        /// Moves a Visual3D object to a new position. It updates the Position property.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to move.</param>
        /// <param name="x">The x-coordinate of the new position.</param>
        /// <param name="y">The y-coordinate of the new position.</param>
        /// <param name="z">The z-coordinate of the new position.</param>
        public void MoveTo(Visual3D visual, double x, double y, double z)
        {
            MoveTo(visual, new Point3D(x, y, z));
        }

        /// <summary>
        /// Rotates a Visual3D object around the x-axis.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to rotate.</param>
        /// <param name="angle">The angle of the x-axis rotation, in degrees.</param>
        public void RotateX(Visual3D visual, double angle)
        {
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty);
            Quaternion q = new Quaternion(new Vector3D(1, 0, 0) * matrix, angle);
            matrix.RotateAt(q, (Point3D)visual.GetValue(PositionProperty));
            visual.SetValue(MatrixPropertyKey, matrix);
            ApplyTransform(visual);
        }

        /// <summary>
        /// Rotates a Visual3D object around the y-axis.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to rotate.</param>
        /// <param name="angle">The angle of the y-axis rotation, in degrees.</param>
        public void RotateY(Visual3D visual, double angle)
        {
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty); 
            Quaternion q = new Quaternion(new Vector3D(0, 1, 0) * matrix, angle);
            matrix.RotateAt(q, (Point3D)visual.GetValue(PositionProperty));
            visual.SetValue(MatrixPropertyKey, matrix);
            ApplyTransform(visual);
        }

        /// <summary>
        /// Rotates a Visual3D object around the z-axis.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to rotate.</param>
        /// <param name="angle">The angle of the z-axis rotation, in degrees.</param>
        public void RotateZ(Visual3D visual, double angle)
        {
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty); 
            Quaternion q = new Quaternion(new Vector3D(0, 0, 1) * matrix, angle);
            matrix.RotateAt(q, (Point3D)visual.GetValue(PositionProperty));
            visual.SetValue(MatrixPropertyKey, matrix);
            ApplyTransform(visual);
        }

        /// <summary>
        /// Rotates a Visual3D object around the x-, y- and then z-axis.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to rotate.</param>
        /// <param name="angleX">The angle of the x-axis rotation, in degrees.</param>
        /// <param name="angleY">The angle of the y-axis rotation, in degrees.</param>
        /// <param name="angleZ">The angle of the z-axis rotation, in degrees.</param>
        public void RotateXYZ(Visual3D visual, double angleX, double angleY, double angleZ)
        {
            Point3D position = (Point3D)visual.GetValue(PositionProperty);
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty); 
            matrix.RotateAt(new Quaternion(new Vector3D(1, 0, 0) * matrix, angleX), position);
            matrix.RotateAt(new Quaternion(new Vector3D(0, 1, 0) * matrix, angleY), position);
            matrix.RotateAt(new Quaternion(new Vector3D(0, 0, 1) * matrix, angleZ), position);
            visual.SetValue(MatrixPropertyKey, matrix);
            ApplyTransform(visual);
        }

        /// <summary>
        /// Rotates a Visual3D object around the z-, y- and then x-axis.
        /// </summary>
        /// <remarks>
        /// The successive transformation operations (translation, scaling or rotation) are cumulative (the last one is relative to its previous one).
        /// </remarks>
        /// <param name="visual">A Visual3D object to rotate.</param>
        /// <param name="angleX">The angle of the x-axis rotation, in degrees.</param>
        /// <param name="angleY">The angle of the y-axis rotation, in degrees.</param>
        /// <param name="angleZ">The angle of the z-axis rotation, in degrees.</param>
        public void RotateZYX(Visual3D visual, double angleX, double angleY, double angleZ)
        {
            Point3D position = (Point3D)visual.GetValue(PositionProperty);
            Matrix3D matrix = (Matrix3D)visual.GetValue(MatrixProperty); 
            matrix.RotateAt(new Quaternion(new Vector3D(0, 0, 1) * matrix, angleZ), position);
            matrix.RotateAt(new Quaternion(new Vector3D(0, 1, 0) * matrix, angleY), position);
            matrix.RotateAt(new Quaternion(new Vector3D(1, 0, 0) * matrix, angleX), position);
            visual.SetValue(MatrixPropertyKey, matrix);
            ApplyTransform(visual);
        }
    }
}
