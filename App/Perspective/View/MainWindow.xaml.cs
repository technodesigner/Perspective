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
using System.Windows.Shapes;
using Perspective.ViewModel;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shell;

namespace Perspective.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private JumpList _jumpList;
        private bool _frameVisible = false;
        private Storyboard _frameShowStoryboard = new Storyboard();
        private Storyboard _frameHideStoryboard = new Storyboard();
        private Duration _duration = new Duration(TimeSpan.FromMilliseconds(600));
        //private IEasingFunction _easeIn = new PowerEase();
        //private IEasingFunction _easeOut = new PowerEase();
        //private IEasingFunction _easeIn = new SineEase();
        //private IEasingFunction _easeOut = new SineEase();
        private IEasingFunction _easeIn = new ExponentialEase();
        private IEasingFunction _easeOut = new ExponentialEase();

        private MainViewModel _mainViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            //Uri uri = new Uri("pack://application:,,,/Perspective;component/Perspective.ico", UriKind.Absolute);
            //this.Icon = BitmapFrame.Create(uri); 

            this.DataContext = _mainViewModel;

            const string category = "Perspective";
            _jumpList = new JumpList();
            JumpList.SetJumpList(Application.Current, _jumpList);

            // _jumpList.JumpItemsRejected += new EventHandler<JumpItemsRejectedEventArgs>(_jumpList_JumpItemsRejected);

            _jumpList.ShowFrequentCategory = true;
            _jumpList.ShowRecentCategory = true;
            //JumpPath jumpath = new JumpPath();
            //jumpath.Path = @"test.per";
            //_jumpList.JumpItems.Add(jumpath);

            JumpTask jumpTask = new JumpTask();
            jumpTask.Title = "Perspective";
            jumpTask.CustomCategory = category;
            jumpTask.ApplicationPath = "http://perspective.codeplex.com";
            jumpTask.IconResourcePath = @"C:\Program Files\Internet Explorer\iexplore.exe";
            jumpTask.IconResourceIndex = 0;
            _jumpList.JumpItems.Add(jumpTask);

            jumpTask = new JumpTask();
            jumpTask.Title = "Perspective FX";
            jumpTask.CustomCategory = category;
            jumpTask.ApplicationPath = "http://perspectivefx.codeplex.com";
            jumpTask.IconResourcePath = @"C:\Program Files\Internet Explorer\iexplore.exe";
            jumpTask.IconResourceIndex = 0;
            _jumpList.JumpItems.Add(jumpTask);

            jumpTask = new JumpTask();
            jumpTask.Title = "odewit.net";
            jumpTask.CustomCategory = category;
            jumpTask.ApplicationPath = "http://www.odewit.net";
            jumpTask.IconResourcePath = @"C:\Program Files\Internet Explorer\iexplore.exe";
            jumpTask.IconResourceIndex = 0;

            _jumpList.JumpItems.Add(jumpTask);
            _jumpList.Apply();

            //(_easeIn as PowerEase).Power = 5;
            //(_easeOut as PowerEase).Power = 5;
            //(_easeOut as PowerEase).EasingMode = EasingMode.EaseOut;
            //(_easeOut as SineEase).EasingMode = EasingMode.EaseOut;
            (_easeOut as ExponentialEase).EasingMode = EasingMode.EaseOut;
        }

        //void _jumpList_JumpItemsRejected(object sender, JumpItemsRejectedEventArgs e)
        //{
        //    MessageBox.Show(e.RejectionReasons[0].ToString());
        //}

        private void frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ShowFrameWithAnimation();
            this.TaskbarItemInfo.Overlay = this.Resources["NotificationImage"] as ImageSource;

            // TaskbarItemInfo.

            //const string category = "Perspective";
            //if (frame.CanGoBack)
            //{
            //    _jumpList.JumpItems.Clear();
            //    foreach (JournalEntry entry in frame.BackStack)
            //    {
            //        JumpTask jumpTask = new JumpTask();
            //        jumpTask.Title = entry.Name;
            //        jumpTask.CustomCategory = category;
            //        _jumpList.JumpItems.Add(jumpTask);
            //    }
            //    foreach (JournalEntry entry in frame.ForwardStack)
            //    {
            //        JumpTask jumpTask = new JumpTask();
            //        jumpTask.Title = entry.Name;
            //        jumpTask.CustomCategory = category;
            //        _jumpList.JumpItems.Add(jumpTask);
            //    }
            //    _jumpList.Apply();
            //}
        }

        private void extensionListBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            HideFrameWithAnimation();
        }

        private void detailGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Thickness thickness = new Thickness(detailGrid.ActualWidth, 0.0, 0.0, 0.0);
            frame.Margin = thickness;
            frame.Width = detailGrid.ActualWidth;

            ThicknessAnimation frameShowAnimation = new ThicknessAnimation()
            {
                To = new Thickness(0.0, 0.0, 0.0, 0.0),
                Duration = _duration,
                EasingFunction = _easeIn
            };
            Storyboard.SetTarget(frameShowAnimation, frame);
            Storyboard.SetTargetProperty(frameShowAnimation, new PropertyPath(Frame.MarginProperty));
            _frameShowStoryboard.Children.Clear();
            _frameShowStoryboard.Children.Add(frameShowAnimation);

            ThicknessAnimation frameHideAnimation = new ThicknessAnimation()
            {
                To = thickness,
                Duration = _duration,
                EasingFunction = _easeOut
            };
            Storyboard.SetTarget(frameHideAnimation, frame);
            Storyboard.SetTargetProperty(frameHideAnimation, new PropertyPath(Frame.MarginProperty));
            _frameHideStoryboard.Children.Clear();
            _frameHideStoryboard.Children.Add(frameHideAnimation);
        }

        private void ShowFrameWithAnimation()
        {
            if (!_frameVisible)
            {
                _frameVisible = true;
                frame.Visibility = System.Windows.Visibility.Visible;
                _frameShowStoryboard.Begin();
            }
        }

        private void HideFrameWithAnimation()
        {
            if (_frameVisible)
            {
                _frameHideStoryboard.Begin();
                // frame is collapsed so the current page can't get the focus
                frame.Visibility = System.Windows.Visibility.Collapsed;
                _frameVisible = false;
            }
        }
    }
}
