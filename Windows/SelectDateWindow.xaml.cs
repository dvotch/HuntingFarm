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
using HuntingFarm.Models;
using HuntingFarm.Pages;

namespace HuntingFarm.Windows
{
    /// <summary>
    /// Логика взаимодействия для SelectDateWindow.xaml
    /// </summary>
    public partial class SelectDateWindow : Window
    {
        private int _huntId;
        public SelectDateWindow(int HuntId)
        {
            InitializeComponent();
            _huntId = HuntId;
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
            {
                AccountingEvent hunting = new AccountingEvent()
                {
                    HuntId = _huntId,
                    ClientId = Manager.CurrentUser.id,
                    Date = (DateTime)datePicker.SelectedDate,
                };
                HuntEntities.GetContext().AccountingEvents.Add(hunting);
                HuntEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно");
                Close();
            } else
            {
                MessageBox.Show("Необходимо выбрать дату");
            }
        }
    }
}
