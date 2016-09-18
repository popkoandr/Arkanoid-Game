using System.Windows;
using System.Windows.Media;
using ArkanoidGame.ViewModels.VMBase;

namespace ArkanoidGame.ViewModels
{
    class Paddle : ViewModelBase
    {
        private SolidColorBrush _color;
        public Vector _position;
        private int _width;
        private int _height;

        public Paddle()
        {
            Position = new Vector(300, 620);
            Width = 100;
            Height = 20;
            Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2980b9"));
        }
        public Vector Position
        {
            get { return _position; }
            set { _position = value; OnPropertyChanged("Position"); }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; base.OnPropertyChanged("Width"); }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; base.OnPropertyChanged("Height"); }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set { _color = value; base.OnPropertyChanged("Color"); }
        }

        

        public void MoveRight(int lenght)
        {

            Position = new Vector(_position.X += lenght, _position.Y);
        }

        public void MoveLeft(int lenght)
        {
            Position = new Vector(_position.X -= lenght, _position.Y);
        }

    }
}
