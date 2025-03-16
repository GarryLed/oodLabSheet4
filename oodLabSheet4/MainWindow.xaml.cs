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

namespace oodLabSheet4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

        }

        // private main window loaded method 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // populate the stock level listbox upon startup 
            lbxStock.ItemsSource = Enum.GetNames(typeof(StockLevel));

            // populate the suppliers listbox 
            var query1 = from s in db.Suppliers
                         orderby s.CompanyName
                         select new 
                         {
                             SupplierName = s.CompanyName,
                             SupplierID = s.SupplierID,
                             Country = s.Country
                         };

            lbxSuppliers.ItemsSource = query1.ToList();

            // populate the countries listbox (using the data from query1)
            var query2 = query1
                .OrderBy(s => s.Country)
                .Select(s => s.Country);


            var countries = query2.ToList();

            lbxCountries.ItemsSource = countries.Distinct();

            
        }

        // selected changed for lbx stock 
        private void lbxStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // get the stock level that is selected 
            var query = from p in db.Products
                        where p.UnitsInStock < 50
                        orderby p.ProductName
                        select p.ProductName;

            string selected = lbxStock.SelectedItem as string;

            switch (selected)
            {
                case "Low":
                    break;
                case "Normal":
                    query = from p in db.Products
                            where p.UnitsInStock >= 50 && p.UnitsInStock <= 100
                            orderby p.ProductName
                            select p.ProductName;
                    break;
                case "Overstocked":
                    query = from p in db.Products
                            where p.UnitsInStock > 100
                            orderby p.ProductName
                            select p.ProductName;
                    break;

            }
        }

        // selection changed for lbx suppliers 
        private void lbxSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // selection changed for lbx countries 
        private void lbxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    // enum for stocklevel (for populating the stock level listbox) 
    public enum StockLevel { Low, Normal, Overstocked };
}
