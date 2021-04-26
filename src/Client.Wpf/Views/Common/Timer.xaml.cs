using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Client.Wpf.Views.Common
{
    public partial class Timer : UserControl
    {
        public Timer()
        {
            InitializeComponent();
            SetClock();
            StartClock();
        }

        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
            => SetClock();

        private void SetClock()
            => Time.Text = DateTime.Now.ToString(@"HH:mm:ss");
    }
}
