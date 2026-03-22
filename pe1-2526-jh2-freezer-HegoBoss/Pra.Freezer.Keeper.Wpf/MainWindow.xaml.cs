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
using Pra.Freezer.Keeper.Core;

namespace Pra.Freezer.Keeper.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /// laten we aan UI beginnen en hopen dat werkt haha 
    public partial class MainWindow : Window
    {
        private FreezerService _freezerService;
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _freezerService = new FreezerService(true);

            DisableEditMode();
            UpdateList();


        }
        //eerste test als me code werkt tot nu toe niks gebroken oef
        private void UpdateList()
        {
            // Haal de waarden uit de filters
            string filterName = txtFilter.Text;
            DateTime? filterDate = dtpFilter.SelectedDate;

            // Vraag de gefilterde lijst op
            lstProducts.ItemsSource = _freezerService.Filter(filterName, filterDate);
            lstProducts.Items.Refresh();
        }

        private void DisableEditMode()
        {
            // ier zorg dat groupbox aanstaat maar niet aanpasbaar
            grpProductDetails.IsEnabled = true;
            txtName.IsReadOnly = true;
            txtQuantity.IsReadOnly = true;
            txtMaxStorage.IsReadOnly = true;
            dtpFreezerDate.IsEnabled = false;

            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            btnAddProduct.IsEnabled = true;

            // en ier moet iets geselecteerd 
            btnUseProduct.IsEnabled = lstProducts.SelectedItem != null;
        }

        private void EnableEditMode()
        {
            grpProductDetails.IsEnabled = true;
            txtName.IsReadOnly = false;
            txtQuantity.IsReadOnly = false;
            txtMaxStorage.IsReadOnly = false;
            dtpFreezerDate.IsEnabled = true;

            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            btnAddProduct.IsEnabled = false;
            btnUseProduct.IsEnabled = false;
        }

        // ------------ eventhandlers ----------------

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            {
                lstProducts.SelectedItem = null;
                EnableEditMode();

                txtName.Text = "";
                txtQuantity.Text = "1";
                txtMaxStorage.Text = "1";
                dtpFreezerDate.SelectedDate = DateTime.Now;

                txtName.Focus();
            }
        }

        private void btnUseProduct_Click(object sender, RoutedEventArgs e)
        {
            if (lstProducts.SelectedItem is Product selectedProduct)
            {
                bool hasItemsLeft = selectedProduct.UseItem();

                if (!hasItemsLeft)
                {
                    _freezerService.RemoveProduct(selectedProduct);
                    MessageBox.Show("Dit was de laatste portie. Het product is uit de voorraad verwijderd.", "Laatste portie", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                UpdateList();

                if (hasItemsLeft)
                {
                    lstProducts.SelectedItem = selectedProduct;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DisableEditMode();
            lstProducts.SelectedItem = null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                int.TryParse(txtQuantity.Text, out int quantity);
                int.TryParse(txtMaxStorage.Text, out int months);
                DateTime date = dtpFreezerDate.SelectedDate ?? DateTime.Now;

                Product newProduct = new Product(name, months, date, quantity);
                _freezerService.AddProduct(newProduct);

                UpdateList();
                lstProducts.SelectedItem = newProduct;
                DisableEditMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fout bij opslaan", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstProducts.SelectedItem is Product selectedProduct)
            {
                txtName.Text = selectedProduct.IsName;
                txtQuantity.Text = selectedProduct.IsQuantity.ToString();
                txtMaxStorage.Text = selectedProduct.IsMaxStorageMonths.ToString();
                dtpFreezerDate.SelectedDate = selectedProduct.IsFreezerDate;

                DisableEditMode();
            }
            else
            {
                txtName.Text = "";
                txtQuantity.Text = "";
                txtMaxStorage.Text = "";
                dtpFreezerDate.SelectedDate = null;
                btnUseProduct.IsEnabled = false;
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void dtpFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void imgClearFilter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtFilter.Text = "";
            dtpFilter.SelectedDate = null;
            UpdateList();
        }

        // dit is in laaste commit normaal, hierna in paar uur zal nog eens me code overlopen met fris hoofd mss details fixxen maar de code werkt 
        //ben verschoten hoe goed het is gelukt in vergeleking van pe1 van prb waar echt zwaargesukkeld heb
        //nu alleen hopen dat ik niet te veel conventie vergeten te gebruiken heb 
    }
}