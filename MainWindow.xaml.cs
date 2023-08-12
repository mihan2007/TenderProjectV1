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

namespace TenderProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TenderList.ItemsSource = new[] { new { Subject = "бинты", Customer = "Мэрия Мухсранска", ExpirationDate = "10/09/2023", Law ="44ФЗ", Link = "https:/zakupki"},
            new { Subject = "гондоны", Customer = "Мэрия УстьПиздюйск", ExpirationDate = "11/09/2023", Law ="223ФЗ", Link = "https:/zakupki"}};
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PassportTender passportTender = new PassportTender();
            passportTender.Show();

        }

        private void TenderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PassportTender passportTender = new PassportTender();
            passportTender.Show();
        }
    }
}
