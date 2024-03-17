using System;
using System.Windows;
using TenderProject.Model.BuisnessDomain;

namespace TenderProject
{
    public partial class PassportTender : Window
    {
 
        private TenderInfo _tenderInfo; // создаем локальную переменную типа TenderInfo 
        
        public event Action<TenderInfo> TenderChanged; // создаем событие 

        public PassportTender()
        {
            InitializeComponent(); // инициализируем паспорт тендера 
            Closing += PassportTender_Closing;
        }

        public void Initialize(TenderInfo tenderInfo) // инициализируем паспорт тендера
        {
            
            DataContext = tenderInfo; // указываем контекст, тобы можно было биндить данные в визуальную форму
            _tenderInfo = tenderInfo; //

        }


        private void PassportTender_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

                MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
               
                if (result == MessageBoxResult.Yes)
                {
                    TenderChanged.Invoke(_tenderInfo);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;  
                }  
        }

    }
}

