using HuntingFarm.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HuntingFarm.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private string _photoName = null, _filePath = null, _currentDirectoryClient = Directory.GetCurrentDirectory() + @"\Images\Clients\", _currentDirectoryAdministrator = Directory.GetCurrentDirectory() + @"\Images\Administrators\",
            _currentDirectoryNull = Directory.GetCurrentDirectory() + @"\Images\";
        public ProfileWindow()
        {
            InitializeComponent();
            InitialWindow();
        }

        void InitialWindow()
        {
            TbTime.Text = getTimeText();
            TbName.Text = getUserName();
            ImagePhoto.Source = new BitmapImage(new Uri(Manager.CurrentUser.GetPhoto, UriKind.Absolute));
            if (Manager.CurrentUser.RoleId == 1)
            {
                StackPanelUser.Visibility = Visibility.Visible;
            }
            if (Manager.CurrentUser.RoleId == 2)
            {
                StackPanelAdmin.Visibility = Visibility.Visible;
            }
        }

        private string getTimeText()
        {
            DateTime timeNow = DateTime.Now;
            int hour = timeNow.Hour;
            int minute = timeNow.Minute;

            if (hour >= 9 && hour < 11 || hour == 11 && minute == 0) return  "Доброе утро";
            if (hour >= 11 && hour < 18 || hour == 18 && minute == 0) return  "Добрый день";
            if (hour >= 18 && hour < 24 || hour == 24 && minute == 0) return "Доброй вечер";
            return "Доброй ночи";
        }

        private string getUserName()
        {
            if (Manager.CurrentUser.GenderId == 1) return "Mr " + Manager.CurrentUser.Name + Manager.CurrentUser.Patronymic + " " + Manager.CurrentUser.Surname;
            return "Ms " + Manager.CurrentUser.Name + Manager.CurrentUser.Patronymic + Manager.CurrentUser.Surname;
        }

        private void BtnAddHuntEvents_Click(object sender, RoutedEventArgs e)
        {
            EventsWindow eventsWindow = new EventsWindow();
            eventsWindow.Owner = this;
            this.Hide();
            eventsWindow.Show();
        }

        private void BtnAddAnimal_Click(object sender, RoutedEventArgs e)
        {
            AnimalsWindow animalsWindow = new AnimalsWindow();
            animalsWindow.Owner = this;
            this.Hide();
            animalsWindow.Show();
        }

        private void BtnAddHouse_Click(object sender, RoutedEventArgs e)
        {
            HousesWindow housesWindow = new HousesWindow();
            housesWindow.Owner = this;
            this.Hide();
            housesWindow.Show();
        }

        private void btnEditCurrentUser_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(Manager.CurrentUser);
            registrationWindow.Owner = this;
            registrationWindow.Show();
        }

        private void BtnMyEvents_Click(object sender, RoutedEventArgs e)
        {
            AccountingsClientWindow accountingsClientWindow = new AccountingsClientWindow();
            accountingsClientWindow.Owner = this;
            accountingsClientWindow.Show();
        }

        private void BtnAddRole_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow usersWindow = new UsersWindow();
            usersWindow.Show();
            usersWindow.Owner = this;
            this.Hide();
        }

        private void btnUploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Выбор фото";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF (*.gif)|*.gif";
                if (op.ShowDialog() == true)
                {
                    ImagePhoto.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
                string diretory = Manager.CurrentUser.RoleId == 2 ? _currentDirectoryAdministrator : _currentDirectoryClient;
                string photo = _photoName == null ? null : ChangePhotoName(diretory);
                string dest = diretory + photo;
                if (_photoName != null) File.Copy(_filePath, dest);
                User user = HuntEntities.GetContext().Users.First(p => p.id == Manager.CurrentUser.id);
                user.Image = _photoName;
                HuntEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно добавлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _filePath = null;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ImagePhoto.Source = new BitmapImage(new Uri(Manager.CurrentUser.GetPhoto, UriKind.Absolute));
        }
        string ChangePhotoName(string _currentDirectory)
        {
            string x = _currentDirectory + _photoName;
            string photoName = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoName;
                }
                photoName = i.ToString() + photoName;
            }
            _photoName = photoName;
            return photoName;
        }
    }


}
