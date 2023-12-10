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
        User _currentUser = null;   
        public RegistrationWindow()
        {
            InitializeComponent();
            ComboGender.ItemsSource = HuntEntities.GetContext().Genders.ToList();
        }

        public RegistrationWindow(User user)
        { 
            InitializeComponent();
            _currentUser = user;
            StackPanelRole.Visibility = Visibility.Visible;
            ComboGender.ItemsSource = HuntEntities.GetContext().Genders.ToList();
            ComboGender.SelectedIndex = Manager.CurrentUser.GenderId;
            TbName.Text = user.Name;
            TbSurname.Text = user.Surname;
            TbPatronymic.Text = user.Patronymic;
            TbEmail.Text = user.Email;
            TbExperience.Text = user.Experience.ToString();
            TbLogin.Text = user.Login;
            PasswordBox.Password = user.Password;
            PasswordBoxAgain.Password = user.Password;
            TbPassword.Text = user.Password;
            TbPasswordAgain.Text = user.Password;
            DatePickerBirthday.SelectedDate = user.Birthday;            
            ComboRole.ItemsSource = HuntEntities.GetContext().Roles.ToList();
            ComboRole.SelectedIndex = Manager.CurrentUser.RoleId - 1;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _errors = CheckFields();
            if (_errors.Length > 0)
            {
                MessageBox.Show(_errors.ToString());
                return;
            }
            try
            {
                string passowrd = (bool)CheckBoxShowPassword.IsChecked ? TbPassword.Text : PasswordBox.Password;
                if (_currentUser == null)
                {
                    User user = new User()
                    {
                        Name = TbName.Text,
                        Surname = TbSurname.Text,
                        Patronymic = TbPatronymic.Text,
                        Birthday = (DateTime)DatePickerBirthday.SelectedDate,
                        RoleId = 1,
                        GenderId = (ComboGender.SelectedItem as Gender).id,
                        Email = TbEmail.Text,
                        Password = passowrd,
                        Login = TbLogin.Text,
                        Experience = int.Parse(TbExperience.Text),
                        Image = null,
                    };
                    HuntEntities.GetContext().Users.Add(user);
                } else
                {
                    User user = HuntEntities.GetContext().Users.First(p => p.id == _currentUser.id);
                    user.Name = TbName.Text;
                    user.Surname = TbSurname.Text;
                    user.Patronymic = TbPatronymic.Text;
                    user.Birthday = (DateTime)DatePickerBirthday.SelectedDate;
                    user.RoleId = (ComboRole.SelectedItem as Role).id;
                    user.GenderId = (ComboGender.SelectedItem as Gender).id;
                    user.Email = TbEmail.Text;
                    user.Password = passowrd;
                    user.Login = TbLogin.Text;
                    user.Experience = int.Parse(TbExperience.Text);
                    user.Image = _currentUser.Image;
                }
                HuntEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно");

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckPassword(string password)
        {
            if (password.Length < 6) return false;
            if (password.ToLower() == password || password.ToUpper() == password) return false;
            int count = 0, spec = 0;
            for (int i = 0; i < password.Length;i++)
            {
                if (char.IsDigit(password[i])) count++;
                if (!char.IsLetterOrDigit(password[i])) spec++;
            }
            if(count == 0 || spec == 0) return false;
            return true;
        }

        private StringBuilder CheckFields()
        {
            StringBuilder sb = new StringBuilder();
            User login = null;
            if (_currentUser == null)
                login = HuntEntities.GetContext().Users.FirstOrDefault(p => p.Login == TbLogin.Text);
            if (string.IsNullOrWhiteSpace(TbSurname.Text)) sb.AppendLine("Введите фамилию");
            if (string.IsNullOrWhiteSpace(TbName.Text)) sb.AppendLine("Введите имя");
            if (!CheckBirthday(DatePickerBirthday.SelectedDate)) sb.AppendLine("Введите дату, либо вам меньше 18");
            if (ComboGender.SelectedIndex == -1) sb.AppendLine("Выберите пол");
            if ((string.IsNullOrWhiteSpace(PasswordBox.Password) || string.IsNullOrWhiteSpace(PasswordBoxAgain.Password) && CheckBoxShowPassword.IsChecked == false)) sb.AppendLine("Введите пароль");
            else if ((string.IsNullOrWhiteSpace(TbPassword.Text) || string.IsNullOrWhiteSpace(TbPasswordAgain.Text) && CheckBoxShowPassword.IsChecked == true)) sb.AppendLine("Введите пароль");
            else
            {
                if (CheckBoxShowPassword.IsChecked == true)
                {
                    if (TbPassword.Text != TbPasswordAgain.Text) sb.AppendLine("Пароли отличаются");
                    else if (!CheckPassword(TbPassword.Text)) sb.AppendLine("Пароль не соответствует требованиям:\n - не менее 6символов; " +
                        "\n - заглавные и строчные буквы;\n - не менее одного спецсимвола;\n - неменее одной цифры.");
                }
                else
                {
                    if(PasswordBox.Password != PasswordBoxAgain.Password) sb.AppendLine("Пароли отличаются");
                    else if (!CheckPassword(PasswordBox.Password)) sb.AppendLine("Пароль не соответствует требованиям:\n - не менее 6символов; " +
                        "\n - заглавные и строчные буквы;\n - не менее одного спецсимвола;\n - неменее одной цифры.");
                }
            }
            if (string.IsNullOrWhiteSpace(TbLogin.Text)) sb.AppendLine("Введите логин");
            if (login != null) sb.AppendLine("Пользователь с таким логином уже существует");
            return sb;
        }

        private bool CheckBirthday(DateTime? date)
        {
            if (date == null) return false;
            TimeSpan timeSpan = DateTime.Now - date ?? new TimeSpan();
            if (timeSpan.TotalDays / 365 < 18) return false;
            return true;
        }

        private void CheckBoxShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            TbPassword.Text = PasswordBox.Password;
            PasswordBox.Visibility = Visibility.Collapsed;
            TbPassword.Visibility = Visibility.Visible;

            TbPasswordAgain.Text = PasswordBoxAgain.Password;
            PasswordBoxAgain.Visibility = Visibility.Collapsed;
            TbPasswordAgain.Visibility = Visibility.Visible;
            
            
        }

        private void CheckBoxShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = TbPassword.Text;
            TbPassword.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;

            PasswordBoxAgain.Password = TbPasswordAgain.Text;
            TbPasswordAgain.Visibility = Visibility.Collapsed; 
            PasswordBoxAgain.Visibility = Visibility.Visible;
        }
    }
}
