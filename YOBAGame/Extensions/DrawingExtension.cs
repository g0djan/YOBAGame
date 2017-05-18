using System;
using System.Drawing;
using Archimedes.Geometry;

namespace YOBAGame.Extensions
{
    public static class DrawingExtension
    {
        public static Point Sub(this Point p1, Point p2)
        {
            return new Point(p1.X -p2.X, p1.Y - p2.Y);
        }

        public static Bitmap ScaleImage(this Bitmap img, double sx, double sy)
        {
            var bmp = new Bitmap(img.Width, img.Height);
            var g = Graphics.FromImage(bmp);
            g.ScaleTransform((float)sx, (float)sy);
            g.DrawImage(img, 0, 0);
            g.Dispose();
            return bmp;
        }

        public static Point RotatePoint(this Point point, double angle)
        {
            var sin = Math.Abs(Math.Sin(angle));
            var cos = Math.Abs(Math.Cos(angle));
            var x1 = point.X * cos - point.Y * sin;
            var y1 = point.X * sin + point.Y * cos;
            return new Point((int)x1, (int)y1);
        }

        public static Bitmap RotateImage(this Bitmap img, double angle)
        {
            return RotateImg(img, -(float)(angle * 180 / Math.PI), Color.Transparent);
        }

        public static Bitmap RotateImg(Bitmap bmp, float angle, Color bkColor)
        {
            angle = angle % 360;
            if (angle > 180)
                angle -= 360;

            System.Drawing.Imaging.PixelFormat pf = default(System.Drawing.Imaging.PixelFormat);
            if (bkColor == Color.Transparent)
            {
                pf = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            }
            else
            {
                pf = bmp.PixelFormat;
            }

            float sin = (float)Math.Abs(Math.Sin(angle * Math.PI / 180.0)); // this function takes radians
            float cos = (float)Math.Abs(Math.Cos(angle * Math.PI / 180.0)); // this one too
            float newImgWidth = sin * bmp.Height + cos * bmp.Width;
            float newImgHeight = sin * bmp.Width + cos * bmp.Height;
            float originX = 0f;
            float originY = 0f;

            if (angle > 0)
            {
                if (angle <= 90)
                    originX = sin * bmp.Height;
                else
                {
                    originX = newImgWidth;
                    originY = newImgHeight - sin * bmp.Width;
                }
            }
            else
            {
                if (angle >= -90)
                    originY = sin * bmp.Width;
                else
                {
                    originX = newImgWidth - sin * bmp.Height;
                    originY = newImgHeight;
                }
            }

            Bitmap newImg = new Bitmap((int)newImgWidth, (int)newImgHeight, pf);
            Graphics g = Graphics.FromImage(newImg);
            g.Clear(bkColor);
            g.TranslateTransform(originX, originY); // offset the origin to our calculated values
            g.RotateTransform(angle); // set up rotate
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(bmp, 0, 0); // draw the image at 0, 0
            g.Dispose();

            return newImg;
        }
    }
}
