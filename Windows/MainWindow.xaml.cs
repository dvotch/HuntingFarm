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


            }
            catch
            {

            }
        }

        private void BtnAutorization_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AutorizationWindow window = new AutorizationWindow();
                if (window.ShowDialog() == true)
                {
                    int role = Manager.CurrentUser.RoleId;
                    if (role == 1)
                    {
                        ClientWindow clientWindow = new ClientWindow();
                        clientWindow.Owner = this;
                        this.Hide();
                        clientWindow.Show();
                    }
                    if (role == 2)
                    {
                        OrganizerWindow organizerWindow = new OrganizerWindow();
                        organizerWindow.Owner = this;
                        this.Hide();
                        organizerWindow.Show();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
