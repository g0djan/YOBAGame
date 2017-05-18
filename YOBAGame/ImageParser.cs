using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace YOBAGame
{
    public class ImageParser
    {
        public static List<Tuple<Bitmap, Point>[]> ParsePicture(string ImageFilename, int partsCount)
        {
            var src = Image.FromFile(ImageFilename) as Bitmap;
            var annotation = ImageFilename.Substring(0, ImageFilename.Length - 3) + "annotation";
            var data = File.ReadAllLines(annotation);
            var countImages = data.Length / 3;
            int x, y, width, height;
            double rotateX, rotateY, centrX, centrY;
            Point upperLeft, removalPoint;
            Size cropSize;

            var imageParts = new List<Tuple<Bitmap, Point>[]>();
            for (var partNumber = 0; partNumber < partsCount; partNumber++)
            {
                var partSize = countImages / partsCount;
                var imagePart = new Tuple<Bitmap, Point>[partSize];
                for (var picNumer = 0; picNumer < partSize; picNumer++)
                {
                    var s = new string[3][];
                    var imageIndex = partNumber * partSize + picNumer;
                    for (var j = 0; j < 3; j++)
                        s[j] = data[3 * imageIndex + j].Split();
                    x = int.Parse(s[0][0]);
                    y = int.Parse(s[0][1]);
                    width = int.Parse(s[1][0]);
                    height = int.Parse(s[1][1]);
                    rotateX = double.Parse(s[2][0], CultureInfo.InvariantCulture);
                    rotateY = double.Parse(s[2][1], CultureInfo.InvariantCulture);
                    centrX = width / 2;
                    centrY = height / 2;
                    upperLeft = new Point(x, y);
                    cropSize = new Size(width, height);
                    removalPoint = new Point((int)(rotateX - centrX), (int)(rotateY - centrY));
                    var target = new Bitmap(width, height);
                    var g = Graphics.FromImage(target);
                    g.DrawImage(src, new Rectangle(0, 0, width, height), new Rectangle(upperLeft, cropSize),
                        GraphicsUnit.Pixel);
                    g.Dispose();
                    imagePart[picNumer] = Tuple.Create(target, removalPoint);
                }
                imageParts.Add(imagePart);
            }
            return imageParts;
        }
    }
}