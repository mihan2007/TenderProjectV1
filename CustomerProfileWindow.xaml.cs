using Accessibility;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TenderProject.Model.BuisnessDomain;

namespace TenderProject
{
    public partial class CustomerProfileWindow : Window
    {
        private TenderInfo _tenderInfo;
        public CustomerProfileWindow()
        {
            InitializeComponent();
        }

        public void Initialize(TenderInfo tenderInfo)
        {
            DataContext = tenderInfo;
            _tenderInfo = tenderInfo;
        }

        public void CustomerProfileWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _tenderInfo.Customer.Name = CustomerNameTextBox.Text;
            _tenderInfo.Customer.INN = INNTextBox.Text;
            _tenderInfo.Customer.KPP = KPPTextBox.Text;
            _tenderInfo.Customer.OGRN = OGRNTextBox.Text;
            _tenderInfo.Customer.PostAdress = PostAddressTextBox.Text;
            _tenderInfo.Customer.CustomerContactInfo = ContactsTextBox.Text;

        }
    }
}
