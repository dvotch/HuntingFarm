using HuntingFarm.Models;
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
using System.Windows.Shapes;

namespace HuntingFarm.Windows
{
    /// <summary>
    /// Логика взаимодействия для AccountingsClientPage.xaml
    /// </summary>
    public partial class AccountingsClientWindow : Window
    {
        public AccountingsClientWindow()
        {
            InitializeComponent();
            ListBoxEvents.ItemsSource = HuntEntities.GetContext().AccountingEvents.Where(p => p.ClientId == Manager.CurrentUser.id).ToList();
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxEvents.SelectedItem != null)
            {
                MessageBoxResult x = MessageBox.Show("Вы уверены, что хотите отписаться от мероприятия?", "Отписаться", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if(x == MessageBoxResult.OK)
                {
                    HuntEntities.GetContext().AccountingEvents.Remove(ListBoxEvents.SelectedItem as AccountingEvent);
                    MessageBox.Show("Успешно удалено");
                }
                HuntEntities.GetContext().SaveChanges();
                Close();
            } else
            {
                MessageBox.Show("Мероприятие не выбрано");
            }
        }
    }
}
