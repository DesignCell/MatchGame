using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tentsTimer;
        int matchesFound;
        double ticksSec = 10;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1/ticksSec);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tentsTimer++;
            tbTimer.Text = (tentsTimer / ticksSec).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                tbTimer.Text = tbTimer.Text + " Play Again?";
            }
        }

        private void SetUpGame()
        {
            List<string> setEmojis_opt = new List<string>()
            {
                "🐱","🐱",
                "😼","😼",
                "😹","😹",
                "🙀","🙀",
                "😻","😻",
                "😺","😺",
                "😸","😸",
                "😽","😽"
            };
            List<string> setEmojis = new List<string>()
            {
                "🌸","🌸",
                "🌹","🌹",
                "🌺","🌺",
                "🌷","🌷",
                "🌼","🌼",
                "💮","💮",
                "🏵","🏵",
                "🌻","🌻"
            };
            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "tbTimer") 
                    continue;
                int index = random.Next(setEmojis.Count);
                string nextEmoji = setEmojis[index];
                textBlock.Text = nextEmoji;
                textBlock.Visibility = Visibility.Visible;
                setEmojis.RemoveAt(index);

            }

            timer.Start();
            matchesFound = 0;
            tentsTimer = 0;
        }

        TextBlock lastClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = true;
                lastClicked = textBlock;
            }
            else if (textBlock.Text == lastClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchesFound++;
            }
            else
            {
                lastClicked.Visibility = Visibility.Visible;
                findingMatch = false;

            }
        }

        private void tbTimer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
                SetUpGame();
        }
    }
}
