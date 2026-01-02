using Microsoft.VisualBasic;
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

namespace PE2.CycleRentApp.Prb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, decimal> prijzen = new Dictionary<string, decimal>();

        private Dictionary<string, List<string>> categorieData = new Dictionary<string, List<string>>();

        private Dictionary<string, Dictionary<string, int>> verhuringen = new Dictionary<string, Dictionary<string, int>>();

        private string huidigeCategorie = "";

        private List<string> historiek = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void VulGegevens()
        {
            // 1. lijst met prijzen per artikel
            prijzen.Add("Stadsfiets", 10m);
            prijzen.Add("Mountainbike", 15m);
            prijzen.Add("Racefiets", 20m);
            prijzen.Add("Elektrische fiets", 25m);

            prijzen.Add("kinderfiets klein", 7m);
            prijzen.Add("kinderfiets groot", 9m);

            prijzen.Add("fietshelm", 3m);
            prijzen.Add("Kinderzitje", 4m);
            prijzen.Add("Fietstas", 5m);

            prijzen.Add("Regenjas", 6m);
            prijzen.Add("slot", 2m);

            // 2. Categorieën met bijbehorende artikelen
            categorieData.Add("Fietsen", new List<string> { "Stadsfiets", "Mountainbike", "Racefiets", "Elektrische fiets" });
            categorieData.Add("Kinderfietsen", new List<string> { "kinderfiets klein", "kinderfiets groot" });
            categorieData.Add("Accessoires", new List<string> { "fietshelm", "Kinderzitje", "Fietstas" });
            categorieData.Add("Extra's", new List<string> { "Regenjas", "slot" });

        }
        private void InitialiseerStandplaatsen()
        {
            // Voor elke stad maken we een lege Dictionary aan om aantallen bij te houden
            string[] steden = { "Standplaats Brugge", "Standplaats Kortrijk", "Standplaats Gent", "Standplaats Brussel", "Standplaats Leuven" };

            foreach (var stad in steden)
            {
                // Hier maken we de geneste collectie: Een Dictionary IN de Dictionary
                verhuringen.Add(stad, new Dictionary<string, int>());
            }
        }

        // --- EVENTS ---

        // 1. Standplaats kiezen
        private void Locatie_Changed(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.IsChecked == true)
            {
                huidigeCategorie = rb.Content.ToString();

                // Reset de combobox
                cmbProducts.Visibility = Visibility.Hidden;
                cmbProducts.ItemsSource = null;

                ToonOverzicht();
                UpdateGUI();
            }
        }

        // 2. Categorie knoppen
        private void Categorie_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(huidigeCategorie)) return;

            Button btn = sender as Button;
            string categorie = btn.Content.ToString();

            // Haal de lijst met namen op uit de Dictionary
            if (categorieData.ContainsKey(categorie))
            {
                List<string> productenInCategorie = categorieData[categorie];

                cmbProducts.ItemsSource = productenInCategorie;
                cmbProducts.Visibility = Visibility.Visible;

                if (productenInCategorie.Count > 0) cmbProducts.SelectedIndex = 0;
            }
        }

        // 3. Toevoegen (+)
        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProducts.SelectedItem == null || string.IsNullOrEmpty(huidigeCategorie)) return;

            string gekozenProduct = cmbProducts.SelectedItem.ToString();

            // Haal het "winkelmandje" van de huidige stad op
            Dictionary<string, int> mandje = verhuringen[huidigeCategorie];

            if (mandje.ContainsKey(gekozenProduct))
            {
                // Bestaat al? Eentje erbij tellen
                mandje[gekozenProduct] = mandje[gekozenProduct] + 1;
            }
            else
            {
                // Bestaat niet? Toevoegen met aantal 1
                mandje.Add(gekozenProduct, 1);
            }

            ToonOverzicht();
            UpdateGUI();
        }

        // 4. Verwijderen (-)
        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProducts.SelectedItem == null || string.IsNullOrEmpty(huidigeCategorie)) return;

            string gekozenProduct = cmbProducts.SelectedItem.ToString();
            Dictionary<string, int> mandje = verhuringen[huidigeCategorie];

            // We kunnen alleen verminderen als het in het mandje zit
            if (mandje.ContainsKey(gekozenProduct))
            {
                int huidigAantal = mandje[gekozenProduct];
                if (huidigAantal > 1)
                {
                    mandje[gekozenProduct] = huidigAantal - 1;
                }
                else
                {
                    // Als het 1 was, en we doen min 1, dan moet hij uit de lijst
                    mandje.Remove(gekozenProduct);
                }

                ToonOverzicht();
                UpdateGUI();
            }
        }

        // 5. Leegmaken
        private void BtnLeegmaken_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(huidigeCategorie)) return;

            MessageBoxResult result = MessageBox.Show($"Wil je {huidigeCategorie} leegmaken?", "Bevestiging", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // Haal het mandje op en maak het leeg (.Clear)
                verhuringen[huidigeCategorie].Clear();
                ToonOverzicht();
                UpdateGUI();
            }
        }

        // 6. Ticket maken
        private void BtnTicket_Click(object sender, RoutedEventArgs e)
        {
            string ticket = tbkTotal.Text;
            MessageBox.Show(ticket, "Ticket");

            // Voeg toe aan historiek
            historiek.Add($"TIJD: {DateTime.Now}\n{ticket}\n==================");

            // Maak de huidige stad leeg na betalen
            verhuringen[huidigeCategorie].Clear();
            ToonOverzicht();
            UpdateGUI();
        }

        // 7. Historiek tonen
        private void BtnHistoriek_Click(object sender, RoutedEventArgs e)
        {
            if (historiek.Count == 0)
            {
                MessageBox.Show("Nog geen historiek.");
            }
            else
            {
                // Plak alle strings in de lijst aan elkaar
                string totaal = string.Join("\n\n", historiek);
                MessageBox.Show(totaal, "Historiek");
            }
        }

        // --- HULPFUNCTIES ---

        private void ToonOverzicht()
        {
            if (string.IsNullOrEmpty(huidigeCategorie)) return;

            Dictionary<string, int> mandje = verhuringen[huidigeCategorie];
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Bestelling: {huidigeCategorie}");
            sb.AppendLine("--------------------------------");

            decimal totaalBedrag = 0;

            // We lopen door elke entry in de dictionary van de stad
            // 'item.Key' is de naam van het product (bv "Stadsfiets")
            // 'item.Value' is het aantal (bv 2)
            foreach (var item in mandje)
            {
                string naam = item.Key;
                int aantal = item.Value;
                decimal eenheidsPrijs = prijzen[naam]; // Prijs opzoeken in de andere lijst
                decimal regelTotaal = aantal * eenheidsPrijs;

                sb.AppendLine($"{aantal} x {naam} (€{eenheidsPrijs}) = €{regelTotaal}");
                totaalBedrag += regelTotaal;
            }

            sb.AppendLine("--------------------------------");
            sb.AppendLine($"Totaal: €{totaalBedrag}");

            tbkTotal.Text = sb.ToString();
        }

        private void UpdateGUI()
        {
            bool locatieGekozen = !string.IsNullOrEmpty(huidigeCategorie);

            btnBikes.IsEnabled = locatieGekozen;
            btnKids.IsEnabled = locatieGekozen;
            btnAcc.IsEnabled = locatieGekozen;
            btnExtra.IsEnabled = locatieGekozen;

            // Check of er iets in het mandje van de huidige stad zit
            bool heeftItems = false;
            if (locatieGekozen)
            {
                if (verhuringen[huidigeCategorie].Count > 0)
                {
                    heeftItems = true;
                }
            }

            btnClear.IsEnabled = heeftItems;
            btnTicket.IsEnabled = heeftItems;
            btnShowHistory.IsEnabled = historiek.Count > 0;
        }
    }
}

 