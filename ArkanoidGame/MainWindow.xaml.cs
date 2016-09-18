using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using ArkanoidGame;
using ArkanoidGame.ViewModels;
using Rectangle = ArkanoidGame.Rectangle;

namespace ArkanoidGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _mainViewModel = new MainViewModel();
        private bool _clicked = false;
        public MainWindow()
        {
            try
            {
                DataContext = _mainViewModel;
                InitializeComponent();
                //Thread thread = new Thread(new ThreadStart(Update));
                //thread.Start();
                /*DoubleAnimation dblAnimX = new DoubleAnimation(1.0, 0.0, new Duration(TimeSpan.FromSeconds(0.5)));
                dblAnimX.SetValue(Storyboard.TargetProperty, this);

                Storyboard story = new Storyboard();
                Storyboard.SetTarget(dblAnimX, this);
                Storyboard.SetTargetProperty(dblAnimX, new PropertyPath("Opacity"));

                story.Children.Add(dblAnimX);
                story.Completed += (o, s) => { this.Close(); };
                story.Begin(this);*/
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        /*private void Update() {
            _mainViewModel.Rectangles = new FastObservableCollection<Rectangle>();
        }*/

        // Handles the zoom-out storyboard's completed event.  
        private void animationCompleted(object sender, EventArgs e)
        {
            _mainViewModel.Rectangles = new FastObservableCollection<Rectangle>();
           // Update();
           /* new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                _mainViewModel.Rectangles = new MTObservableCollection<Rectangle>();
            }).Start();*/
            /*if (_mainViewModel.Rectangles.Count > 200)
            {
                _mainViewModel.Rectangles.Clear();
            }*/
        }

        private void ItemsControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _clicked = false;
            //MessageBox.Show("Up");
        }

        private void ItemsControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
           // {
                //_mainViewModel.Paddles[0].Position = new Vector(e.GetPosition((IInputElement)sender).X, _mainViewModel.Paddles[0].Position.Y);
                _clicked = true;
           // }
        }

        private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_clicked)
            {
                Vector newPosition = new Vector();
                newPosition.Y = _mainViewModel.Paddles[0].Position.Y;
                newPosition.X = e.GetPosition((IInputElement)sender).X - _mainViewModel.Paddles[0].Width / 2;
                _mainViewModel.Paddles[0].Position = newPosition;
            }
        }

    }
}
