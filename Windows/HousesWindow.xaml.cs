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
    /// Логика взаимодействия для HousesWindow.xaml
    /// </summary>
    public partial class HousesWindow : Window
    {
        public HousesWindow()
        {
            InitializeComponent();
            ListBoxHouses.ItemsSource = HuntEntities.GetContext().Houses.ToList();
        }

        private void btnAddOrEditHouse_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxHouses.SelectedItem != null)
            {
                AddOrEditHouseWindow addOrEditHouse = new AddOrEditHouseWindow(ListBoxHouses.SelectedItem as House);
                addOrEditHouse.Owner = this;
                this.Hide();
                addOrEditHouse.Show();
            }
            else
            {
                AddOrEditHouseWindow addOrEditHouse = new AddOrEditHouseWindow();
                addOrEditHouse.Show();
            }
        }
    }
}
