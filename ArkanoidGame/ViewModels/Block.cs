﻿using System.Windows;
using System.Windows.Media;
using ArkanoidGame.ViewModels.VMBase;

namespace ArkanoidGame.ViewModels
{
    class Block : ViewModelBase
    {
        private SolidColorBrush _color;
        public Vector _position;
        private int _width;
        private int _height;

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

        public SolidColorBrush Color
        {
            get { return _color; }
            set { _color = value; base.OnPropertyChanged("Color"); }
        }

        public Block(Vector position)
        {
            Position = position;
            Width = 105;
            Height = 20;
            Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ecc71"));
            
        }

    }
}
