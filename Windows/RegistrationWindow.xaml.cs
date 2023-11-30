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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            ComboGender.ItemsSource = HuntEntities.GetContext().Genders.ToList();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User()
                {
                    Name = TbName.Text,
                    Surname = TbSurname.Text,
                    Patronymic = TbPatronymic.Text,
                    Birthday = DataPickerBirthday.SelectedDate,
                    RoleId = 1,
                    GenderId = (ComboGender.SelectedItem as Gender).id,
                    Email = TbEmail.Text,
                    Password = TbPassword.Text,
                    Login = TbLogin.Text,
                    Experience = int.Parse(TbExperience.Text)
                };

                HuntEntities.GetContext().Users.Add(user);
                HuntEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно");

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
