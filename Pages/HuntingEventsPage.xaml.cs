
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HuntingFarm.Models;
using HuntingFarm.Windows;

namespace HuntingFarm.Pages
{
    /// <summary>
    /// Логика взаимодействия для HuntingEventsPage.xaml
    /// </summary>
    public partial class HuntingEventsPage : Page
    {
        int _itemcount = 0;
        public HuntingEventsPage()
        {
            InitializeComponent();
            LoadAndInitData();
            
        }


        void LoadAndInitData()
        {
            ListBoxHuntings.ItemsSource = HuntEntities.GetContext().Huntings.OrderBy(p => p.Name).ToList();
            _itemcount = ListBoxHuntings.Items.Count;
            List<Animal> animals = HuntEntities.GetContext().Animals.OrderBy(p => p.Name).ToList();
            animals.Insert(0, new Animal
            {
                Name = "Все животные",
                Description = "Любое",
            });

            List<House> houses = HuntEntities.GetContext().Houses.OrderBy(p => p.Name).ToList();
            houses.Insert(0, new House
            {
                Name = "Все дома",
                Description = "Любое",
            });

            ComboHouse.ItemsSource = houses;
            ComboHouse.SelectedIndex = 0;
            ComboAnimal.ItemsSource = animals;
            ComboAnimal.SelectedIndex = 0;
            TextBlockInfo.Text = $"Результат запроса: {_itemcount} записей из {_itemcount}";
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                HuntEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListBoxHuntings.ItemsSource = HuntEntities.GetContext().Huntings.OrderBy(p => p.Name).ToList();
                if (Manager.CurrentUser != null && Manager.CurrentUser.RoleId == 1) btnSignUpEvent.Visibility = Visibility.Visible;
            }
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            var currentHuntings = HuntEntities.GetContext().Huntings.OrderBy(p => p.Name).ToList();
            if (ComboAnimal.SelectedIndex > 0) currentHuntings = currentHuntings.Where(p => p.AnimalId == (ComboAnimal.SelectedItem as Animal).id).ToList();
            if (ComboHouse.SelectedIndex > 0) currentHuntings = currentHuntings.Where(p => p.HouseId == (ComboHouse.SelectedItem as House).id).ToList();
            currentHuntings = currentHuntings.Where(p => p.Name.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            ListBoxHuntings.ItemsSource = currentHuntings;
            TextBlockInfo.Text = $"Результат запроса: {currentHuntings.Count()} записей из {_itemcount}";

        }

        private void ComboAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void ComboHouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void btnSignUpEvent_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxHuntings.SelectedIndex == -1)
            {
                MessageBox.Show("Мероприятие не выбрано");
                return;
            }
            try
            {
                SelectDateWindow selectDateWindow = new SelectDateWindow((ListBoxHuntings.SelectedItem as Hunting).id);
                selectDateWindow.Show();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
