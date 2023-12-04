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
using HuntingFarm.Models;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace HuntingFarm.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddAnimalPage.xaml
    /// </summary>
    public partial class AddAnimalWindow : Window
    {
        private string _photoName = null, _filePath = null, _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\Animals\",
            _currentDirectoryNull = Directory.GetCurrentDirectory() + @"\Images\";
        Animal _currentAnimal;
        public AddAnimalWindow()
        {
            InitializeComponent();
            ComboDifficulty.ItemsSource = HuntEntities.GetContext().Difficulties.OrderBy(p => p.Name).ToList();
        }

        public AddAnimalWindow(Animal animal)
        {
            InitializeComponent();
            
            var items = HuntEntities.GetContext().Difficulties.OrderBy(p => p.Name).ToList();
            int index = -1;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].id == animal.DifficultyId) index = i;
            }
            ComboDifficulty.ItemsSource = items;
            ComboDifficulty.SelectedIndex = index;
            _currentAnimal = animal;
            string file = "";
            if (_currentAnimal.Image == null) file = _currentDirectoryNull + "default.png";
            else file = _currentDirectory +  _currentAnimal.Image;
            ImagePreview.Source = new BitmapImage(new Uri(file));
            TbName.Text = _currentAnimal.Name;
            TbDescription.Text = _currentAnimal.Description;
            ComboDifficulty.SelectedIndex = index;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _errors = CheckFields();
            if (_errors.Length > 0)
            {
                MessageBox.Show(_errors.ToString());
                return;
            }
            try
            {
                if (_currentAnimal == null)
                {
                    string photo = _photoName == null ? null : ChangePhotoName(_currentDirectory);
                    string dest = _currentDirectory + photo;
                    if (_photoName != null) File.Copy(_filePath, dest);
                    _currentAnimal = new Animal()
                    {
                        Name = TbName.Text,
                        Description = TbDescription.Text,
                        DifficultyId = (ComboDifficulty.SelectedItem as Difficulty).id,
                        Image = photo,
                    };
                    HuntEntities.GetContext().Animals.Add(_currentAnimal);
                }
                else
                {
                    Animal animal = HuntEntities.GetContext().Animals.First(p => p.id == _currentAnimal.id);
                    animal.Name = TbName.Text;
                    animal.Description = TbDescription.Text;
                    animal.DifficultyId = (ComboDifficulty.SelectedItem as Difficulty).id;
                    animal.Image = _photoName;
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

        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Выбор фото";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF (*.gif)|*.gif";
                if(op.ShowDialog() == true)
                {
                    ImagePreview.Source = new BitmapImage( new Uri(op.FileName) );
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _filePath = null;
            }
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

        private StringBuilder CheckFields()
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TbName.Text)) sb.AppendLine("Введите название животного");
            if (string.IsNullOrWhiteSpace(TbDescription.Text)) sb.AppendLine("Введите описание");
            if (ComboDifficulty.SelectedIndex == -1) sb.AppendLine("Выберите уровень сложности");
            return sb;
        }
    }
}
