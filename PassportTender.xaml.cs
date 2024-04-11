using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using TenderProject.Model.BuisnessDomain;
using System.Windows.Media;

namespace TenderProject
{
    public partial class PassportTender : Window
    {
 
        private TenderInfo _tenderInfo; // создаем локальную переменную типа TenderInfo 

        private TenderInfo _sourceTenderInfo;
        
        public event Action<TenderInfo> TenderChanged; // создаем событие 

        public PassportTender()
        {
            InitializeComponent(); // инициализируем паспорт тендера 
            Closing += PassportTender_Closing;
        }

        public void Initialize(TenderInfo tenderInfo) // инициализируем паспорт тендера
        {         
            _sourceTenderInfo = tenderInfo; //
            _tenderInfo = _sourceTenderInfo.Copy();
            DataContext = _tenderInfo; // указываем контекст, тобы можно было биндить данные в визуальную форму            
        }

        public void OpenCustomerProfileWindow(object sender, MouseButtonEventArgs e)
        {
            CustomerProfileWindow customerProfileWindow = new CustomerProfileWindow();
            customerProfileWindow.Initialize(_tenderInfo);
            customerProfileWindow.Show();
        }


        private void PassportTender_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
            // сенить фокус , закончить редактирование в текущем поле 
            RemoveFocusFromTextBoxes();
            if (_sourceTenderInfo.Equals(_tenderInfo))
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                _sourceTenderInfo.Reinit(_tenderInfo);
                TenderChanged.Invoke(_tenderInfo);
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void RemoveFocusFromTextBoxes()
        {
            // Получаем все дочерние элементы окна
            var children = GetAllChildren(this);

            // Перебираем дочерние элементы и проверяем, является ли каждый из них TextBox
            foreach (var child in children)
            {
                if (child is TextBox textBox)
                {
                    // Если элемент является TextBox, снимаем с него фокус
                    textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
        }

        private static System.Collections.Generic.IEnumerable<DependencyObject> GetAllChildren(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                yield return child;
                foreach (var grandChild in GetAllChildren(child))
                    yield return grandChild;
            }
        }

    }
}

