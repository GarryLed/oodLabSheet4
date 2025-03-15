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
            
        }
    } 
    // enum for stocklevel (for populating the stock level listbox) 
    public enum StockLevel { Low, Normal, Overstocked };
}
