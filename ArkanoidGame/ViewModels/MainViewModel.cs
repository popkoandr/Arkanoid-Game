using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ArkanoidGame.ViewModels.VMBase;

namespace ArkanoidGame.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        ObservableCollection<Ball> _balls = new ObservableCollection<Ball>();
        ObservableCollection<Paddle> _paddles = new ObservableCollection<Paddle>();
        FastObservableCollection<Rectangle> _rects = new FastObservableCollection<Rectangle>();
        FastObservableCollection<Bonus> _bonuses = new FastObservableCollection<Bonus>();
        ObservableCollection<Block> _blocks = new ObservableCollection<Block>();

        private int _playerLifes;
        private int _playerBallSpeed;
        private int _playerPaddleWidth;

        const int WIDTH = 1024;
        const int HEIGHT = 630;
        private float _fps;
        private float _gameTime;
        private float _speed;
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public MainViewModel()
        {
            this.Initialize();
        }

        #region Fields


        public int PlayerLifes
        {
            get { return _playerLifes; }
            set
            {
                if (value == _playerLifes)
                    return;

                _playerLifes = value;

                OnPropertyChanged("PlayerLifes");
            }
        }

        public int PlayerBallSpeed
        {
            get { return _playerBallSpeed; }
            set
            {
                if (value == _playerBallSpeed)
                    return;

                _playerBallSpeed = value;

                OnPropertyChanged("PlayerBallSpeed");
            }
        }

        public int PlayerPaddleWidth
        {
            get { return _playerPaddleWidth; }
            set
            {
                if (value == _playerPaddleWidth)
                    return;

                _playerPaddleWidth = value;

                OnPropertyChanged("PlayerPaddleWidth");
            }
        }

        public ObservableCollection<Ball> Balls
        {
            get { return _balls; }
            set
            {
                if (value == _balls)
                    return;

                _balls = value;

                OnPropertyChanged("Balls");
            }
        }

        public ObservableCollection<Paddle> Paddles
        {
            get { return _paddles; }
            set
            {
                if (value == _paddles)
                    return;

                _paddles = value;

                OnPropertyChanged("Paddles");
            }
        }

        public ObservableCollection<Block> Blocks
        {
            get { return _blocks; }
            set
            {
                if (value == _blocks)
                    return;

                _blocks = value;

                OnPropertyChanged("Blocks");
            }
        }

        public FastObservableCollection<Rectangle> Rectangles
        {
            get { return _rects; }
            set
            {
                if (value == _rects)
                    return;

                _rects = value;

                OnPropertyChanged("Rectangles");
            }
        }

        public FastObservableCollection<Bonus> Bonuses
        {
            get { return _bonuses; }
            set
            {
                if (value == _bonuses)
                    return;

                _bonuses = value;

                OnPropertyChanged("Bonuses");
            }
        }

        #endregion 

        #region Commands

        public ICommand RightKeyCommand
        {
            get { return Commands.GetOrCreateCommand(() => RightKeyCommand, MovePaddleRight); }
        }

        //public ICommand MouseUpCommand
        //{
        //    get { return Commands.GetOrCreateCommand(() => MouseUpCommand, param => MouseUp((MouseEventArgs) param)); }
        //}

        public ICommand LeftKeyCommand
        {
            get { return Commands.GetOrCreateCommand(() => LeftKeyCommand, MovePaddleLeft, () => true); }
        }


        #endregion

        //Происходит при отпускании кнопки мыши, когда указатель мыши находится на элементе управления.
        //private void MouseUp(MouseEventArgs e)
        //{
        //    // MessageBox.Show(e.GetPosition((IInputElement)e.Source).ToString());
        //    Paddles[0].Position = new Vector(e.GetPosition((IInputElement)e.Source).X, Paddles[0].Position.Y);
        //}

        private void Initialize()
        {
            _fps = 400;
            _gameTime = 1 / _fps;
            _speed = 1000;
            PlayerLifes = 3;
            PlayerBallSpeed = (int)_speed;
            Balls.Add(new Ball());
            Paddles.Add(new Paddle());
            PlayerPaddleWidth = _paddles[0].Width;

            Balls[0]._velocity.X = _speed * _gameTime * Math.Cos(Balls[0].Angle * Math.PI / 180);
            Balls[0]._velocity.Y = _speed * _gameTime * Math.Sin(Balls[0].Angle * Math.PI / 180);

            //generate blocks
            GenerateBlocks();
            _timer.Tick += UpdateGame;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            _timer.Start();
        }

        private void Restart()
        {
            Balls.Clear();
            Blocks.Clear();
            Paddles.Clear();
            Bonuses.Clear();
            _speed = 1200;
            PlayerLifes = 3;
            PlayerBallSpeed = (int)_speed;
            Balls.Add(new Ball());
            Paddles.Add(new Paddle());
            PlayerPaddleWidth = _paddles[0].Width;

            Balls[0]._velocity.X = _speed * _gameTime * Math.Cos(Balls[0].Angle * Math.PI / 180);
            Balls[0]._velocity.Y = _speed * _gameTime * Math.Sin(Balls[0].Angle * Math.PI / 180);


            GenerateBlocks();
            _timer.Start();
        }

        private void GenerateBlocks()
        {
            for (int b = 10; b < HEIGHT / 2; b += 30)
            {
                for (int i = 30; i < WIDTH - 100; i += 120)
                {
                    Blocks.Add(new Block(new Vector(i, b)));
                }
            }
        }

        private void UpdateGame(object sender, EventArgs e)
        {
            // Будущее положение мяча, нужно для предотвращения "залипания" мяча на поверхности обьекта
            /* Rectangle nextRect = new Rectangle((int)(Balls[0].Position.X + Balls[0].Velocity.X),
               (int)(Balls[0].Position.Y + Balls[0].Velocity.Y),
               Balls[0].Width, Balls[0].Height);*/

            /*Ball nextBall = new Ball();
            nextBall.Position = new Vector((Balls[0].Position.X + Balls[0].Velocity.X), (Balls[0].Position.Y + Balls[0].Velocity.Y));*/

            //Balls[0]._velocity.X = _speed* _gameTime * Math.Cos(Balls[0].Angle*Math.PI/180);
            //Balls[0]._velocity.Y = _speed * _gameTime * Math.Sin(Balls[0].Angle * Math.PI / 180);

            CheckWin();

            CheckTopAndBottom();
            CheckLeftAndRight();
            CheckCollision();

            CheckBonusBounce();
            MoveBonuses();

            Balls[0].Position += Balls[0].Velocity;
        }
        private void CheckWin()
        {
            if (Blocks.Count == 0)
            {
                _timer.Stop();
                MessageBox.Show("You Won!");
                Restart();
                _timer.Start();
            }
        }

        private void GenerateBonus(Vector position)
        {
            Random rnd = new Random();
            int res = rnd.Next(0, 5);
            int choose = 1;
            if (res == choose)
            {
                //int x = rnd.Next(5,WIDTH-50);
                //int y = rnd.Next(0,HEIGHT/2);
                int bonusType = rnd.Next(1, 4);
                Bonuses.Add(new Bonus(new Vector((position.X) + 105 / 2, (position.Y) + 20 / 2), bonusType));
            }
        }
        private void MoveBonuses()
        {
            if (Bonuses.Count > 0)
            {
                for (int i = 0; i < Bonuses.Count; i++)
                {
                    Bonuses[i].Position = new Vector(Bonuses[i].Position.X, Bonuses[i].Position.Y + 3);
                }
            }
        }

        private void CheckBonusBounce()
        {
            if (Bonuses.Count > 0)
            {
                for (int i = 0; i < Bonuses.Count; i++)
                {
                    if (isIntersect(Bonuses[i], Paddles[0]))
                    {
                        switch (Bonuses[i].BonusType)
                        {
                            case 1:
                                Paddles[0].Width += 10;
                                Paddles[0].Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#e74c3c"));
                                PlayerPaddleWidth = Paddles[0].Width;
                                break;

                            case 2:
                                Balls[0].Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#d35400"));
                                _speed += 40;
                                PlayerBallSpeed = (int)_speed;
                                break;

                            case 3:
                                Balls[0].Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#95a5a6"));
                                _speed -= 40;
                                PlayerBallSpeed = (int)_speed;
                                break;

                            case 4:
                                if (Paddles[0].Width > 50)
                                {
                                    Paddles[0].Width -= 10;
                                    PlayerPaddleWidth = Paddles[0].Width;
                                }
                                Paddles[0].Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#34495e"));
                                break;
                        }
                        Bonuses.RemoveAt(i);
                    }
                    else
                    {
                        if (Bonuses[i].Position.Y > HEIGHT)
                        {
                            Bonuses.RemoveAt(i);
                        }
                    }
                }
            }
        }
        private void CheckTopAndBottom()
        {
            if ((Balls[0].Position.Y + Balls[0].Velocity.Y) < 0)
            {
                Balls[0].ReflectHorizontal();
            }

            if ((Balls[0].Position.Y + Balls[0].Velocity.Y) > HEIGHT)
            {
                Balls[0].ReflectHorizontal();
                if (_playerLifes == 1)
                {
                    _timer.Stop();
                    MessageBox.Show("Game Over!");
                    Restart();
                    _timer.Start();
                }
                else
                {
                    PlayerLifes -= 1;
                }
            }
        }
        private void CheckLeftAndRight()
        {
            if ((Balls[0].Position.X + Balls[0].Velocity.X) < 0)
            {
                Balls[0].ReflectVertical();
            }

            if ((Balls[0].Position.X + Balls[0].Velocity.X) > WIDTH - 50)
            {
                Balls[0].ReflectVertical();
            }
        }
        private void CheckCollision()
        {
            if (isIntersect(Balls[0], Paddles[0]))
            {
                float ballpos = (float)(Balls[0].Width + Balls[0].Position.X - Paddles[0].Position.X - 1);
                float ballmax = (float)(Balls[0].Width + Paddles[0].Width - 2);
                float factor = (float)(ballpos / ballmax);
                float angle = (float)((135 - factor * (135 - 45)) * Math.PI / 180);
                Balls[0]._velocity.Y = -(_speed) * _gameTime * Math.Sin(angle);
                Balls[0]._velocity.X = (_speed) * _gameTime * Math.Cos(angle);
            }

            for (int i = 0; i < Blocks.Count; i++)
            {
                if (isIntersect(Balls[0], Blocks[i]))
                {
                    GenerateBonus(Blocks[i].Position);
                    Balls[0].ReflectHorizontal();
                    //_rects.Clear();
                    for (int b = (int)Blocks[i].Position.Y; b < (int)(Blocks[i].Position.Y + Blocks[i].Height); b += 9)
                    {
                        for (int c = (int)Blocks[i].Position.X; c < (int)(Blocks[i].Position.X + Blocks[i].Width); c += 9)
                        {
                            _rects.Add(new Rectangle(new Vector(c, b)));
                        }
                    }
                    Blocks.RemoveAt(i);
                }
            }
        }

        private void MovePaddleRight()
        {
            if (_paddles[0]._position.X + 15 <= WIDTH - _paddles[0].Width)
            {
                Paddles[0].MoveRight(20);
            }
            else
            {
                Paddles[0].MoveRight((int)(WIDTH - (_paddles[0].Width + _paddles[0]._position.X)));
            }
        }
        private void MovePaddleLeft()
        {
            if (_paddles[0]._position.X - 20 >= 0)
                Paddles[0].MoveLeft(20);
            else
                Paddles[0].MoveLeft((int)_paddles[0]._position.X);
        }

        private bool isIntersect(Vector p1, Vector p2, Vector p3, Vector p4)
        {
            return (p1.X < p3.X + p4.X &&
                   p1.X + p2.X > p3.X &&
                   p1.Y < p3.Y + p4.Y &&
                   p2.Y + p1.Y > p3.Y);
        }
        private bool isIntersect(Ball ball, Block paddle)
        {
            return (ball.Position.X + ball.Velocity.X < paddle.Position.X + paddle.Width &&
                   ball.Position.X + ball.Velocity.X + ball.Width > paddle.Position.X &&
                   ball.Position.Y + ball.Velocity.Y < paddle.Position.Y + paddle.Height &&
                   ball.Height + ball.Position.Y + ball.Velocity.Y > paddle.Position.Y);
        }
        private bool isIntersect(Ball ball, Paddle paddle)
        {
            return (ball.Position.X < paddle.Position.X + paddle.Width &&
                   ball.Position.X + ball.Width > paddle.Position.X &&
                   ball.Position.Y < paddle.Position.Y + paddle.Height &&
                   ball.Height + ball.Position.Y > paddle.Position.Y);
        }
        private bool isIntersect(Bonus ball, Paddle paddle)
        {
            return (ball.Position.X < paddle.Position.X + paddle.Width &&
                   ball.Position.X + ball.Width > paddle.Position.X &&
                   ball.Position.Y < paddle.Position.Y + paddle.Height &&
                   ball.Height + ball.Position.Y > paddle.Position.Y);
        }

    }
}
