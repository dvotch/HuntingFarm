using System;
using System.Windows;
using HuntingFarm.Pages;
using HuntingFarm.Models;
using HuntingFarm.Windows;

namespace HuntingFarm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            
        }
        void LoadData()
        {
            MainFrame.Navigate(new HuntingEventsPage());
            Manager.MainFrame = MainFrame;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?", "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel) e.Cancel = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistrationWindow registrationWindow = new RegistrationWindow();
                if (registrationWindow.ShowDialog() == true)
                {
                        btnRegistration.Visibility = Visibility.Hidden;
                        btnAutorization.Content = "Профиль";
                        btnAutorization.Click += HandleClickProfile;
                        btnAutorization.Click -= BtnAutorization_Click;
                        MainFrame.Navigate(new HuntingEventsPage());
                        Manager.MainFrame = MainFrame;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void BtnAutorization_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AutorizationWindow window = new AutorizationWindow();
                if (window.ShowDialog() == true)
                {
                    btnRegistration.Content = "Выйти";
                    btnRegistration.Click += HandleClickSignOut;
                    btnRegistration.Click -= BtnRegistration_Click;

                    btnAutorization.Content = "Профиль";
                    btnAutorization.Click += HandleClickProfile;
                    btnAutorization.Click -= BtnAutorization_Click;
                    MainFrame.Navigate(new HuntingEventsPage());
                    Manager.MainFrame = MainFrame;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void HandleClickProfile(object sender, RoutedEventArgs args)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
        }
        private void HandleClickSignOut(object sender, RoutedEventArgs args)
        {
            Manager.CurrentUser = null;
            btnAutorization.Content = "Авторизация";
            btnAutorization.Click -= HandleClickProfile;
            btnAutorization.Click += BtnAutorization_Click;

            btnRegistration.Content = "Регистрация";
            btnRegistration.Click -= HandleClickSignOut;
            btnRegistration.Click += BtnRegistration_Click;
            MainFrame.Navigate(new HuntingEventsPage());
            Manager.MainFrame = MainFrame;
        }
    }
}
