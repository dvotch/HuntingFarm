using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using HuntingFarm.Models;


namespace HuntingFarm.Windows
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        bool b = false;
        int count = 0;
        DispatcherTimer timer = new DispatcherTimer();
        int seconds = 0;
        string captcha = "";
        public AutorizationWindow()
        {
            InitializeComponent();
            LoadAndInitData();
        }

        void LoadAndInitData()
        {
            this.Height = 200;
            timer.Tick += timer_tick;
            RowCaptcha1.Height = new GridLength(0);
            RowCaptcha2.Height = new GridLength(0);
        }
        void timer_tick(object sender, EventArgs e)
        {
            seconds -= 1;
            TbTime.Text = $"Осталось {seconds} секунд до разблокировки";
            if (seconds == 0)
            {
                btnEnter.IsEnabled = true;
                TbTime.Text = "";
                timer.Stop();
            }
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<User> users = HuntEntities.GetContext().Users.ToList();
                User user = users.FirstOrDefault(p => p.Password == TbPassword.Password && p.Login == TbLogin.Text);
                if ((user != null && !b) || (user != null && b && TbCaptcha.Text == captcha)){
                    Manager.CurrentUser = user;
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                    count++;
                    if (count >= 3)
                    {
                        this.Height = 350;
                        ShowCaptcha();
                        timer.Interval = TimeSpan.FromSeconds(1);
                        btnEnter.IsEnabled = false;
                        seconds = 10;
                        TbTime.Text = $"Осталось {seconds} до разблокировки";
                        timer.Start();
                        RowCaptcha1.Height = new GridLength(34);
                        RowCaptcha2.Height = new GridLength(1, GridUnitType.Star);
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void BtnRenewCaptcha_Click(object sender, RoutedEventArgs e)
        {
            ShowCaptcha();
        }
        void ShowCaptcha ()
        {
            var tuple = MakeCaptcha.CreateImage(300, 100, 4);
            ImageCaptcha.Source = tuple.image;
            captcha = tuple.captcha;
        }
    }
}
