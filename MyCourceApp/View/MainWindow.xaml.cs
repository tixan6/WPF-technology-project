using MyCourceApp.Scrips;
using MyCourceApp.View;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCourceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Email;
        public string password;

        Main main1 = new Main();
        BrushConverter bc = new BrushConverter();
        DataBase dataBase = new DataBase();
        DataTable table = new DataTable();
        MySqlDataAdapter adapter = new MySqlDataAdapter();


        public MainWindow()
        {
            InitializeComponent();
            
        }
        //Circle Red Login
        private void CircleRedLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CircleRedLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            CircleRedLogin.Opacity = 1;
        }

        private void CircleRedLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            CircleRedLogin.Opacity = 0.6;
        }
        //Circle Green Login
        private void CircleGreenLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CircleGreenLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            CircleGreenLogin.Opacity = 1;
        }

        private void CircleGreenLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            CircleGreenLogin.Opacity = 0.6;
        }


        //Click on register button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Close();
        }

        //Btn sign in
        private void btnSignIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ВХОД

            try
            {
                Email = EmailLog.Text;
                password = passwordLog.Text;

                MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `Email` = @uL AND `password`= @uP", dataBase.GetConnection());

                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = Email;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = password;


                adapter.SelectCommand = command;
                adapter.Fill(table);

                GetSet.passw = password;

                if (table.Rows.Count > 0)
                {
                    Main main = new Main();
                    main.Show();
                    this.Close();

                    Parse parse = new Parse();

                    string buyDol = parse.pares(@"<span>За\s1\sUSD<\/span><\/div><div\sclass=""price""><span>([0-9]+\.[0-9]+)<\/span><p>покупка<\/p><\/div><div\sclass=""price""><span>", @"([0-9]+\.[0-9]+)");
                    GetSet.BuyD = buyDol;
                    string sellDol = parse.pares(@"<span>3.6000<\/span><p>покупка<\/p><\/div><div class=""price""><span>3.8900<\/span><p>продажа<\/p>", @"([0-9]+\.[0-9]+)");
                    GetSet.SellD = sellDol;

                    string sellEur = parse.pares(@"alt=""EUR""><span>За\s1\sEUR<\/span><\/div><div class=""price""><span>([0-9]+\.[0-9]+)<\/span><p>покупка<\/p><\/div><div class=""price""><span>([0-9]+\.[0-9]+)\<", @"([0-9]+\.[0-9]+)");
                    GetSet.SellE = sellEur;

                    string byEur = parse.pares(@"alt=""EUR""><span>За\s1\sEUR<\/span><\/div><div class=""price""><span>([0-9]+\.[0-9]+)<\/span><p>покупка<\/p><\/div><div class=""price""><span>", @"([0-9]+\.[0-9]+)");
                    GetSet.BuyE = byEur;

                    string sellR = parse.pares(@"alt=""RUB""><span>За\s100\sRUB</span></div><div\sclass=""price""><span>([0-9]+\.[0-9]+)</span><p>покупка</p></div><div class=""price""><span>([0-9]+\.[0-9]+)<", @"([0-9]+\.[0-9]+)");
                    GetSet.SellR = sellR;

                    string byRub = parse.pares(@"alt=""RUB""><span>За\s100\sRUB</span></div><div\sclass=""price""><span>([0-9]+\.[0-9]+)</span><p>покупка</p></div><div class=""price""><span>", @"([0-9]+\.[0-9]+)");
                    GetSet.BuyR = byRub;

                }
                else
                {
                    ErrorLog.Text = "Пароль или логин неверный";
                    ErrorLog.Foreground = (Brush)bc.ConvertFrom("#FFFF4444");
                    lineLogError.Background = (Brush)bc.ConvertFrom("#FFFF4444");
                    lineLogError.Opacity = 1;
                }

            }
            catch (Exception)
            {
                ErrorLog.Text = "Проверьте соединение ";
                ErrorLog.Foreground = (Brush)bc.ConvertFrom("#FFF9D652");
                lineLogError.Opacity = 1;
            }
            
 
        }

        private void btnSignIn_MouseEnter(object sender, MouseEventArgs e)
        {
            btnSignIn.Background = (Brush)bc.ConvertFrom("#FF163259");
        }

        private void btnSignIn_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSignIn.Background = (Brush)bc.ConvertFrom("#FF0A192F");
        }

       
    }
}
