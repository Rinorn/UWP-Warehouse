using ModelLibrary;
using WarehouseApplication.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarehouseApplication.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductView : Page
    {
        public ProductView()
        {
            this.InitializeComponent();
        }

        public int currentCategoryId;
        private void OnSelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductViewModel model = ViewModel;

            if (e.AddedItems[0] is Category cat)
                currentCategoryId = model.DeterminCategoryId(cat.CategoryName);

            model.SelectProducts(currentCategoryId);

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button clickeButton = (Button)sender;
            if (clickeButton.Name.Equals("CustProductBtn"))
            {
                Frame.Navigate(typeof(Views.ProductView));
            }
            else if (clickeButton.Name.Equals("CustCustomerBtn"))
            {
                Frame.Navigate(typeof(Views.CustomerView));
            }
        }
    }
}
