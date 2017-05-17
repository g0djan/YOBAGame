using System.Drawing;
using System.IO;

namespace YOBAGame
{
    public static class ImageParser
    {
        public static Bitmap[] ParsePicture(string ImageFilename)
        {
            var src = Image.FromFile(ImageFilename) as Bitmap;


            var annotation = ImageFilename.Substring(0, ImageFilename.Length - 3) + "annotation";
            var data = File.ReadAllLines(annotation);
            var countImages = data.Length / 3; 
            int width;
            int height;

            var pictures = new Bitmap[countImages];
            Point upperLeft;
            int x;
            int y;
            Size cropSize;
            
            var s = new string[3][];
            for (var i = 0; i < countImages; i++)
            {
                for (var j = 0; j < 3; j++)
                    s[j] = data[3 * i + j].Split();
                x = int.Parse(s[0][0]);
                y = int.Parse(s[0][1]);
                width = int.Parse(s[1][0]);
                height = int.Parse(s[1][1]);
                upperLeft = new Point(x, y);
                cropSize = new Size(width, height);
                var target = new Bitmap(width, height);
                var g = Graphics.FromImage(target);
                g.DrawImage(src, new Rectangle(0, 0, width, height), new Rectangle(upperLeft, cropSize),
                    GraphicsUnit.Pixel);
                g.Dispose();
                pictures[i] = target;
            }
            return pictures;
        }
    }
}