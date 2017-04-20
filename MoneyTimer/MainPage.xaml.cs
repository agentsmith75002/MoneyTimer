using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MoneyTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly DispatcherTimer _moneyTimer;
        private TimeSpan _spanTime = new TimeSpan();
        private bool _started = false;

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(400, 300);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            _moneyTimer = new DispatcherTimer();
            _moneyTimer.Interval = new TimeSpan(0, 0, 1); // 1s delay
            _moneyTimer.Tick += TimerProgressElement;
        }
        private void TimerProgressElement(object sender, object e)
        {
            _spanTime = _spanTime.Add(_moneyTimer.Interval);
            var cost = _spanTime.TotalHours * Convert.ToDouble(RateTxt.Text);
            MoneyTxt.Text = $"{cost:0.####} €";
            TimeText.Text = $"{_spanTime.Hours:0}:{_spanTime.Minutes:00}:{_spanTime.Seconds:00}";
        }

        private void StartStopBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(!_started)
            {
                _spanTime = new TimeSpan();
                _started = true;
            }

            if(!_moneyTimer.IsEnabled)
            {
                StartStopBtn.Content = "Stop";
                _moneyTimer.Start();
            }
            else
            {
                StartStopBtn.Content = "Start";
                _moneyTimer.Stop();
            }
        }

        private void ResetBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _moneyTimer.Stop();
            StartStopBtn.Content = "Start";
            _started = false;
            MoneyTxt.Text = "0 €";
            TimeText.Text = "0:00:00";
            RateTxt.Text = "";
        }
    }
}
