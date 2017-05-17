using System.Drawing;

namespace YOBAGame
{
    public static class ImageParser
    {
        public static Bitmap[] ParsePicture(string ImageFilename)
        {
            var src = Image.FromFile(ImageFilename) as Bitmap;

            var countImages = 2 * src.Height / 24; // ну вот здесь крч нужно знать их количество 
            var width = src.Width / 2;            //либо размер каждого битмапа во всех файлах должен быть одинаковым
            var height = 2 * src.Height / countImages;

            var pictures = new Bitmap[countImages];
            var upperLeft = new Point(0, 0);
            var x = 0;
            var y = 0;
            var cropSize = new Size(width, height);
            
            for (var i = 0; i < countImages; i++)
            {
                var target = new Bitmap(width, height);
                var g = Graphics.FromImage(target);
                g.DrawImage(src, new Rectangle(0, 0, width, height), new Rectangle(upperLeft, cropSize),
                    GraphicsUnit.Pixel);
                g.Dispose();
                pictures[i] = target;
                x += height;
                if (x == src.Height)
                {
                    x = 0;
                    y += width;
                }
                upperLeft = new Point(x, y);
            }
            return pictures;
        }
    }
}