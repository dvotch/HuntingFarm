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
    /// Логика взаимодействия для EventsWindow.xaml
    /// </summary>
    public partial class EventsWindow : Window
    {
        public EventsWindow()
        {
            InitializeComponent();
            ListBoxEvents.ItemsSource = HuntEntities.GetContext().Huntings.ToList();
        }

        private void btnAddOrEditEvent_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxEvents.SelectedItem != null)
            {
                AddOrEditHuntEventWindow addOrEditHuntEventWindow = new AddOrEditHuntEventWindow(ListBoxEvents.SelectedItem as Hunting);
                addOrEditHuntEventWindow.Owner = this;
                this.Hide();
                addOrEditHuntEventWindow.Show();
            }
            else
            {
                AddOrEditHuntEventWindow addOrEditHuntEventWindow = new AddOrEditHuntEventWindow();
                addOrEditHuntEventWindow.Show();
            }
        }
    }
}
