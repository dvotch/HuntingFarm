using HuntingFarm.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddOrEditHouseWindow.xaml
    /// </summary>
    public partial class AddOrEditHouseWindow : Window
    {
        private string _photoName = null, _filePath = null, _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\Houses\",
            _currentDirectoryNull = Directory.GetCurrentDirectory() + @"\Images\";
        House _currentHouse;
        public AddOrEditHouseWindow()
        {
            InitializeComponent();
        }
        public AddOrEditHouseWindow(House house)
        {
            InitializeComponent();
            _currentHouse = house;
            string file = "";
            if (_currentHouse.Image == null) file = _currentDirectoryNull + "default.png";
            else file = _currentDirectory + _currentHouse.Image;
            ImagePreview.Source = new BitmapImage(new Uri(file));
            TbName.Text = _currentHouse.Name;
            TbDescription.Text = _currentHouse.Description;
        }

        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Выбор фото";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF (*.gif)|*.gif";
                if (op.ShowDialog() == true)
                {
                    ImagePreview.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }
            catch (Exception ex)
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
                string photo = _photoName == null ? null : ChangePhotoName(_currentDirectory);
                if (_currentHouse == null)
                {
                    
                    string dest = _currentDirectory + photo;
                    if (_photoName != null) File.Copy(_filePath, dest);
                    _currentHouse = new House()
                    {
                        Name = TbName.Text,
                        Description = TbDescription.Text,
                        Image = photo,
                    };
                    HuntEntities.GetContext().Houses.Add(_currentHouse);
                }
                else
                {
                    House house = HuntEntities.GetContext().Houses.First(p => p.id == _currentHouse.id);
                    house.Name = TbName.Text;
                    house.Description = TbDescription.Text;
                    house.Image = _photoName;
                }
                HuntEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно добавлено");
                this.Close();
            }
            catch (Exception ex)
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
            if (string.IsNullOrWhiteSpace(TbName.Text)) sb.AppendLine("Введите название животного");
            if (string.IsNullOrWhiteSpace(TbDescription.Text)) sb.AppendLine("Введите описание");
            return sb;
        }
    }
}
