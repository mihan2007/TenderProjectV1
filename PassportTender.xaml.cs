using System.Windows;

namespace TenderProject
{
    public partial class PassportTender : Window
    {
        public PassportTender()
        {
            InitializeComponent();
        }

        public void InitializeTenderInfo(MainWindow.TenderInfo tenderInfo)
        {
            DataContext = tenderInfo;
        }
    }
}

