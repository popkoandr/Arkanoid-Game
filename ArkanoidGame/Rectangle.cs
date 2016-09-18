using System.Windows;
using System.Windows.Media;

namespace ArkanoidGame
{
    class Rectangle
    {
        private SolidColorBrush _color;
        private Vector _position;
        private int _width;
        private int _height;

        public Rectangle(Vector position)
        {
            Position = position;
            Width = 25;
            Height = 5;
            Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#220000"));
        }
        public Vector Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value;}
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set { _color = value; }
        }

       

    }
}
