using System.Windows;
using System.Windows.Media;
using ArkanoidGame.ViewModels.VMBase;

namespace ArkanoidGame.ViewModels
{
    class Ball : ViewModelBase
    {
        private SolidColorBrush _color;
        public Vector _velocity;
        public Vector _position;
        private int _width;
        private int _height;
        private int _angle;

        public Vector Velocity
        {
            get { return _velocity; }
            set { _velocity = value; base.OnPropertyChanged("Velocity"); }
        }

        public Vector Position
        {
            get { return _position; }
            set { _position = value; base.OnPropertyChanged("Position"); }
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

        public int Angle
        {
            get { return _angle; }
            set { _angle = value; base.OnPropertyChanged("Angle"); }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set { _color = value; base.OnPropertyChanged("Color"); }
        }

        public Ball()
        {
            Position = new Vector(100, 380);
            Width = 30;
            Height = 30;
            Color = new SolidColorBrush(Colors.Red);
            Angle = 45;
            //Velocity = new Vector(1,1);
        }

        public void ReflectHorizontal()
        {
            _velocity.Y = -(_velocity.Y);
        }

        public void ReflectVertical()
        {
            _velocity.X = -_velocity.X;
        }
    }
}
