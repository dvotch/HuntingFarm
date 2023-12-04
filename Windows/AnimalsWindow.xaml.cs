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
    /// Логика взаимодействия для AnimalsWindow.xaml
    /// </summary>
    public partial class AnimalsWindow : Window
    {
        public AnimalsWindow()
        {
            InitializeComponent();
            ListBoxAnimals.ItemsSource = HuntEntities.GetContext().Animals.OrderBy(P => P.Name).ToList();
        }

        private void btnAddOrEditAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAnimals.SelectedItem != null)
            {
                AddAnimalWindow addAnimalWindow = new AddAnimalWindow(ListBoxAnimals.SelectedItem as Animal);
                addAnimalWindow.Owner = this;
                this.Hide();
                addAnimalWindow.Show();
            } else
            {
                AddAnimalWindow addAnimalWindow = new AddAnimalWindow();
                addAnimalWindow.Show();
            }
        }
    }
}
