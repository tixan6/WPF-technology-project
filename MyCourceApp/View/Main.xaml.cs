using Microsoft.Win32;
using MyCourceApp.Scrips;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyCourceApp.View
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        BrushConverter bc = new BrushConverter();
        
        
        public string PhotoAvatar;

       

        public Main()
        {
            InitializeComponent();
        }

        //main Min
        private void mainClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void mainClose_MouseEnter(object sender, MouseEventArgs e)
        {
            mainClose.Opacity = 1;
        }

        private void mainClose_MouseLeave(object sender, MouseEventArgs e)
        {
            mainClose.Opacity = 0.6;
        }
        //main Min
        private void mainMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void mainMin_MouseEnter(object sender, MouseEventArgs e)
        {
            mainMin.Opacity = 1;
        }

        private void mainMin_MouseLeave(object sender, MouseEventArgs e)
        {
            mainMin.Opacity = 0.6;
        }

        //Ativity

        public void ENGL()
        {

            
            //Login
            MainWindow mainWindow = new MainWindow();
            mainWindow.txtSign.Text = "Login";
            mainWindow.bnt_logn.Text = "Login";
            mainWindow.bnt_logn.Text = "Registration";
            mainWindow.NET.Text = "Visit our site Uiopia.com";
            mainWindow.TextUnderLogo.Text = "Time to be aware";
            //Main
            Main main = new Main();
            main.btnNews.Text = "News";
            main.Add_news.Text = "Add news";
            main.Settings.Text = "Settings";


        }

        private void Window_Activated(object sender, EventArgs e)
        {           

            try
            {
                

                string connStr = "server=localhost;port=3306;username=root;password=root;database=couseuiop";
                MySqlConnection conn = new MySqlConnection(connStr);
                MainWindow main = new MainWindow();
                conn.Open();

                //'" + main.password + "'
                string sqlPhoto = "SELECT PathAvatar FROM users WHERE password = '" + GetSet.passw + "'";
                string sql = "SELECT fullname FROM users WHERE password = '" + GetSet.passw + "'";
                string sqlphone = "SELECT phone FROM users WHERE password = '" + GetSet.passw + "'";
                MySqlCommand command = new MySqlCommand(sql, conn);

                string name = command.ExecuteScalar().ToString();
                mainNamePanel.Text = name;

                MySqlCommand commandPhoto = new MySqlCommand(sqlPhoto, conn);
                string PhotoAva = commandPhoto.ExecuteScalar().ToString();
                AvatarName.ImageSource = new BitmapImage(new Uri(PhotoAva));

                MySqlCommand commandPhone = new MySqlCommand(sqlphone, conn);
                string namePhone = commandPhone.ExecuteScalar().ToString();
                PhoneMain.Text = namePhone;
                conn.Close();
            }
            catch (NullReferenceException)
            {

                MessageBox.Show("Ваш аккаунт удален!!!");
            }

            

            

        }

        //Button create news
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //OpenMenuCreateNews
            //тут
            AddNews.Content = new addnews();
            AddNews.Visibility = Visibility.Visible;
            News.Visibility = Visibility.Hidden;
            KyrS.Visibility = Visibility.Hidden;
            ssettings.Visibility = Visibility.Hidden;


        }

        //Наведение
        private void addNews_MouseEnter(object sender, MouseEventArgs e)
        {


            addNews.Background = (Brush)bc.ConvertFrom("#FF213866");

        }

        private void addNews_MouseLeave(object sender, MouseEventArgs e)
        {
            addNews.Background = (Brush)bc.ConvertFrom("#FF09192E");
        }


        //News Открыть новости
        private void StackPanel_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //тут

            News.Visibility = Visibility.Visible;
            AddNews.Visibility = Visibility.Hidden;
            KyrS.Visibility = Visibility.Hidden;
            ssettings.Visibility = Visibility.Hidden;
            News.Content = new news();
        }
        private void newsShow_MouseEnter(object sender, MouseEventArgs e)
        {
            newsShow.Background = (Brush)bc.ConvertFrom("#FF213866");
        }
        private void newsShow_MouseLeave(object sender, MouseEventArgs e)
        {
            newsShow.Background = (Brush)bc.ConvertFrom("#FF09192E");
        }

        //Sett
        private void StackPanel_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            News.Visibility = Visibility.Hidden;
            AddNews.Visibility = Visibility.Hidden;
            KyrS.Visibility = Visibility.Hidden;
            ssettings.Visibility = Visibility.Visible;
            ssettings.Content = new settings();
             
        }

        private void sett_MouseEnter(object sender, MouseEventArgs e)
        {
            sett.Background = (Brush)bc.ConvertFrom("#FF213866");
        }

        private void sett_MouseLeave(object sender, MouseEventArgs e)
        {
            sett.Background = (Brush)bc.ConvertFrom("#FF09192E");
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        //Иконка фото логина
        private void imgPhoto_MouseEnter(object sender, MouseEventArgs e)
        {
            imgPhoto.Opacity = 0.9;
        }



        //BTN output from main
        private void StackPanel_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void imgPhoto_MouseLeave_1(object sender, MouseEventArgs e)
        {
            imgPhoto.Opacity = 0.7;
        }

        private void Leav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Leav_MouseEnter(object sender, MouseEventArgs e)
        {

            //Нажать
            Leav.Background = (Brush)bc.ConvertFrom("#FF213866");
        }

        private void Leav_MouseLeave(object sender, MouseEventArgs e)
        {
            //Убрать
            Leav.Background = (Brush)bc.ConvertFrom("#FF09192E");
        }

        private void Kyrs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Нажать на курс
            KyrS.Content = new _Kyrs();
            AddNews.Visibility = Visibility.Hidden;
            News.Visibility = Visibility.Hidden;
            KyrS.Visibility = Visibility.Visible;
            ssettings.Visibility = Visibility.Hidden;
        }

        private void Kyrs_MouseEnter(object sender, MouseEventArgs e)
        {
            Kyrs.Background = (Brush)bc.ConvertFrom("#FF213866");
        }

        private void Kyrs_MouseLeave(object sender, MouseEventArgs e)
        {
            //Убрать
            Kyrs.Background = (Brush)bc.ConvertFrom("#FF09192E");
        }
    }
}
