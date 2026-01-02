using System.Globalization;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prb.Pe1.Jumps.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grdStats.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// TxtChanger methode maakt dat de txtboxen gebruikbaar of niet bruikbaar worden en ook grijs en niet grijs.
        /// </summary>
        void TxtChanger()
        {
            if (txtJump1.IsEnabled == false)
            {
                txtJump1.IsEnabled = true;
                txtJump2.IsEnabled = true;
                txtJump3.IsEnabled = true;
            }
            else if (txtJump1.IsEnabled == true)
            {
                txtJump1.IsEnabled = false;
                txtJump2.IsEnabled = false;
                txtJump3.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Some black magic is going on over here.");
            }
        }

        /// <summary>
        /// VisibilityChanger methode maakt dat de grid met statistieken zichtbaar of onzichtbaar wordt en de knop om de statistieken te tonen ook onzichtbaar of zichtbaar wordt.
        /// </summary>
        void VisibilityChanger()
        {
            if (grdStats.Visibility == Visibility.Hidden)
            {
                grdStats.Visibility = Visibility.Visible;
                btnShowStats.Visibility = Visibility.Hidden;
            }
            else if (grdStats.Visibility == Visibility.Visible)
            {
                grdStats.Visibility = Visibility.Hidden;
                btnShowStats.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Some black magic is going on over here.");
            }
        }

        /// <summary>
        /// TxtReset methode maakt de txtboxen en het label leeg.
        /// </summary>
        void TxtReset()
        {
            txtJump1.Text = "";
            txtJump2.Text = "";
            txtJump3.Text = "";
            lblMessage.Content = "";
        }

        /// <summary>
        /// paintGold methode maakt de achtergrond van het label en de txtbox goudkleurig.
        /// </summary>
        void PaintGold()
        {
            /*source: https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.brushes?view=windowsdesktop-9.0 */
            lblMessage.Background = Brushes.Gold;
            txtPR.Background = Brushes.Gold;
            // Brushes heb ik op gezocht op de google sinds ik het niet vond in documentatie van het vak en vorige oefeningen (08/11/2025
            // nevermind ik vond het uiteindelijk toch in de oefening selectie - login" (09/11/2025)
        }

        /// <summary>
        /// PaintWhite methode maakt de achtergrond van het label en de txtbox witkleurig.
        /// </summary>
        void PaintWhite()
        {
            lblMessage.Background = Brushes.White;
            txtPR.Background = Brushes.White;
        }

        private void BtnShowStats_Click(object sender, RoutedEventArgs e)
        {
            int firstJump = int.Parse(txtJump1.Text);
            int secondJump = int.Parse(txtJump2.Text);
            int thirdjump = int.Parse(txtJump3.Text);
            
            VisibilityChanger();

            TxtChanger();

            int bestJump = 0;

            if (firstJump >= secondJump && firstJump >= thirdjump)
            {
                bestJump = firstJump;
            }
            else if (secondJump >= firstJump && secondJump >= thirdjump)
            {
                bestJump = secondJump;
            }
            else
            {
                bestJump = thirdjump;
            }


            lblBestJump.Content = bestJump;

            int averageJump = (firstJump + secondJump + thirdjump) / 3;
            lblAverageJump.Content = averageJump;

            int pr = int.Parse(txtPR.Text);
            if (bestJump > pr)
            {
                txtPR.Text = bestJump.ToString();
                PaintGold();

            }

            decimal firstJumpM = firstJump / 100m;
            decimal secondJumpM = secondJump / 100m;
            decimal thirdjumpM = thirdjump / 100m;
            decimal bestJumpM = bestJump / 100m;
            decimal averageJumpM = averageJump / 100m;

            lblMessage.Content = $"sprongen: {firstJumpM:F2}m | {secondJumpM:F2}m | {thirdjumpM:F2}m\n\nBeste sprong: {bestJumpM:F2} m\nGemiddelde sprong: {averageJumpM:F2} m";
         }

        private void BtnNewPerformance_Click(object sender, RoutedEventArgs e)
        {
            
            string newPrestatie = $"{DateTime.Now}\n{lblMessage.Content}\n--------------------------------------";
            lstHistory.Items.Insert(0, newPrestatie);

            VisibilityChanger();

            TxtChanger();

            TxtReset();
            
            PaintWhite();

            txtJump1.Focus();
        }

    }
}