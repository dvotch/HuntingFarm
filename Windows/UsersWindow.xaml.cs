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

namespace HuntingFarm.Windows
{
    /// <summary>
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            ListBoxUsers.ItemsSource = HuntEntities.GetContext().Users.ToList();
        }

        private void btnAddOrEditUsers_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxUsers.SelectedItem != null)
            {
                RegistrationWindow registrationWindow = new RegistrationWindow(ListBoxUsers.SelectedItem as User);
                registrationWindow.Owner = this;
                this.Hide();
                registrationWindow.Show();
            }
            else
            {
                RegistrationWindow registrationWindow = new RegistrationWindow();
                registrationWindow.Show();
            }
        }
    }
}
