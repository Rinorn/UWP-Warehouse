using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ModelLibrary;
using WarehouseApplication.ViewModels;

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

            if (e.AddedItems[0] is Category cat) currentCategoryId = model.DeterminCategoryId(cat.CategoryName);

            model.SelectProducts(currentCategoryId);

        }
    }
}
