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
    /// Логика взаимодействия для AddOrEditHuntEventWindow.xaml
    /// </summary>
    public partial class AddOrEditHuntEventWindow : Window
    {

        Hunting _currentHunting = null;
        public AddOrEditHuntEventWindow()
        {
            InitializeComponent();
            ComboAnimal.ItemsSource = HuntEntities.GetContext().Animals.ToList();
            ComboHouse.ItemsSource = HuntEntities.GetContext().Houses.ToList();
            ComboAdmin.ItemsSource = HuntEntities.GetContext().Users.Where(p => p.RoleId == 2).ToList();
        }
        public AddOrEditHuntEventWindow(Hunting hunting)
        {
            InitializeComponent();
            _currentHunting = hunting;
            List<Animal> animals = HuntEntities.GetContext().Animals.ToList();
            List<House> houses = HuntEntities.GetContext().Houses.ToList();
            List<User> users = HuntEntities.GetContext().Users.Where(p => p.RoleId == 2).ToList();

            int index = -1;
            for (int i = 0; i < animals.Count; i++)
                if (animals[i].id == _currentHunting.AnimalId) index = i;

            ComboAnimal.ItemsSource = animals;
            ComboAnimal.SelectedIndex = index;

            index = -1;
            for (int i = 0; i < houses.Count; i++)
                if (houses[i].id == _currentHunting.HouseId) index = i;

            ComboHouse.ItemsSource = houses;
            ComboHouse.SelectedIndex = index;

            index = -1;
            for (int i = 0; i < users.Count; i++)
                if (users[i].id == _currentHunting.AdminId) index = i;

            ComboAdmin.ItemsSource = users;
            ComboAdmin.SelectedIndex = index;
            
            TbName.Text = _currentHunting.Name;
            TbCost.Text = _currentHunting.Cost.ToString();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _errors = CheckFields();
            if (!decimal.TryParse(TbCost.Text, out decimal cost)) _errors.AppendLine("Некоректная цена");
            if (_errors.Length > 0 )
            {
                MessageBox.Show(_errors.ToString());
                return;
            }
            try
            {
                if (_currentHunting == null)
                {
                    
                    _currentHunting = new Hunting()
                    {
                        Name = TbName.Text,
                        AnimalId = (ComboAnimal.SelectedItem as Animal).id,
                        HouseId= (ComboHouse.SelectedItem as House).id,
                        AdminId = (ComboAdmin.SelectedItem as User).id,
                        Cost =  cost,
                    };
                    HuntEntities.GetContext().Huntings.Add(_currentHunting);
                }
                else
                {
                    Hunting hunting = HuntEntities.GetContext().Huntings.First(p => p.id == _currentHunting.id);
                    hunting.Name = TbName.Text;
                    hunting.AnimalId = (ComboAnimal.SelectedItem as Animal).id;
                    hunting.HouseId = (ComboHouse.SelectedItem as House).id;
                    hunting.AdminId = (ComboAdmin.SelectedItem as User).id;
                    hunting.Cost = decimal.Parse(TbCost.Text);
                }
                HuntEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно добавлено");
                this.Close();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private StringBuilder CheckFields()
        {
            StringBuilder sb = new StringBuilder();
            if (ComboAnimal.SelectedIndex == -1) sb.AppendLine("Выберите животного");
            if (ComboHouse.SelectedIndex == -1) sb.AppendLine("Выберите дом");
            if (ComboAdmin.SelectedIndex == -1) sb.AppendLine("Выберите администратора");
            return sb;
        }
    }
}
