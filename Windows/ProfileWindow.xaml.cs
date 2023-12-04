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
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
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

        private void BtnSignUpOnEvent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
