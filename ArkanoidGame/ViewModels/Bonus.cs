using System.Windows;
using System.Windows.Media;
using ArkanoidGame.ViewModels.VMBase;

namespace ArkanoidGame.ViewModels
{
    class Bonus : ViewModelBase
    {
        private SolidColorBrush _color;
        public Vector _position;
        private int _width;
        private int _height;

        public int BonusType { get; set; }

        public Vector Position
        {
            get { return _position; }
            set { _position = value; base.OnPropertyChanged("Position"); }
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

        public Bonus(Vector position, int bonusType)
        {
            Position = position;
            Width = 10;
            Height = 10;
            BonusType = bonusType;
            Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9b59b6"));
        }
    }
}
