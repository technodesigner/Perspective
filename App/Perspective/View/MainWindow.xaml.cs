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
        JumpList _jumpList;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();

            _jumpList = new JumpList();
            JumpList.SetJumpList(Application.Current, _jumpList);

        }

        private void frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // DoubleAnimation animation = new DoubleAnimation(1.0, TimeSpan.FromMilliseconds(200.0);
            // An EventTrigger can't be used because Navigated is not a routed event
            // Storyboard s = this.Resources["FrameOpacityStoryboard"] as Storyboard;
            // s.FillBehavior = FillBehavior.Stop;
            // s.Begin();

            // frame.Opacity = 1.0;
            if (frame.Visibility == Visibility.Collapsed)
            {
                frame.Visibility = Visibility.Visible;
            }

            if (frame.CanGoBack)
            {
                _jumpList.JumpItems.Clear();
                foreach (JournalEntry entry in frame.BackStack)
                {
                    JumpTask jumpTask = new JumpTask();
                    jumpTask.Title = entry.Name;
                    jumpTask.CustomCategory = "Perspective";
                    _jumpList.JumpItems.Add(jumpTask);
                }
                foreach (JournalEntry entry in frame.ForwardStack)
                {
                    JumpTask jumpTask = new JumpTask();
                    jumpTask.Title = entry.Name;
                    jumpTask.CustomCategory = "Perspective";
                    _jumpList.JumpItems.Add(jumpTask);
                }
                _jumpList.Apply();
            }
        }

        private void extensionListBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (frame.Visibility == Visibility.Visible)
            {
                frame.Visibility = Visibility.Collapsed;
            }

        }
    }
}
