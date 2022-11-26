using MyCourceApp.Scrips;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        BrushConverter bc = new BrushConverter();
        DataBase dataBase = new DataBase();

        public RegisterForm()
        {
            InitializeComponent();
        }
        //Circle Red Reg
        private void CircleRedReg_DragEnter(object sender, DragEventArgs e)
        {
            this.Close();
        }

        private void CircleRedReg_DragLeave(object sender, DragEventArgs e)
        {
            CircleRedReg.Opacity = 0.6;
        }

        //Circle Red Reg
        private void CircleRedReg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CircleRedReg_MouseEnter(object sender, MouseEventArgs e)
        {
            CircleRedReg.Opacity = 1;
        }

        private void CircleRedReg_MouseLeave(object sender, MouseEventArgs e)
        {
            CircleRedReg.Opacity = 0.6;
        }

        //Circle Green Reg
        private void CircleGreenReg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CircleGreenReg_MouseEnter(object sender, MouseEventArgs e)
        {
            CircleGreenReg.Opacity = 1;
        }

        private void CircleGreenReg_MouseLeave(object sender, MouseEventArgs e)
        {
            CircleGreenReg.Opacity = 0.6;
        }

        //Button Back
        private void BackReg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BackReg_MouseEnter(object sender, MouseEventArgs e)
        {

            BackReg.Foreground = (Brush)bc.ConvertFrom("#52cbb7");
        }

        private void BackReg_MouseLeave(object sender, MouseEventArgs e)
        {
            BackReg.Foreground = (Brush)bc.ConvertFrom("#FFC6D0F0");
        }

        //Registation acc
        private void BtnRegSignUpBG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //REG
            string fullName = FullName.Text;
            string email = Email.Text;
            string phone = Phone.Text;
            string passwordOne = PasswordOne.Text;
            string passwordTwo = PasswordTwo.Text;
            bool proverkaEmail = false;
            bool proverkaPhone = false;
            GetSet.phone = phone; 
            //Email

            string pattEmail = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
            if (Regex.IsMatch(email, pattEmail))
            {
                proverkaEmail = true;
            }
            else
            {
                proverkaEmail = false;
            }
            //Phone

            string pattPhone = @"(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})";
            if (Regex.IsMatch(phone, pattPhone))
            {
                proverkaPhone = true;
            }
            else
            {
                proverkaPhone = false;
            }

            if (proverkaEmail == true && proverkaPhone == true && passwordOne == passwordTwo && fullName != "")
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`fullname`, `email`, `phone`, `password`,`PathAvatar`,`Message`, `DeleteAkk`,`Language`, `Renames`) VALUES (@fullname, @email, @phone, @password, @pathAvatar, @Message, @DeleteAkk, @Language, @Renames)", dataBase.GetConnection());

                command.Parameters.Add("@fullname", MySqlDbType.VarChar).Value = fullName;
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = Email.Text;
                command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = Phone.Text;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = PasswordOne.Text;
                command.Parameters.Add("@pathAvatar", MySqlDbType.Text).Value = @"D:\Study\C#\Курсовая\MyCourceApp\MyCourceApp\Images\BgCircle.png";
                command.Parameters.Add("@Message", MySqlDbType.Text).Value = "Нет сообщений";
                command.Parameters.Add("@DeleteAkk", MySqlDbType.Text).Value = "Без требований";
                command.Parameters.Add("@Language", MySqlDbType.Text).Value = "RUS";
                command.Parameters.Add("@Renames", MySqlDbType.Text).Value = "Активно";
                
        

                dataBase.openConBd();

                if (command.ExecuteNonQuery() == 1)
                {
                    //Потом вернуть пользователя в меня логина
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    //Понять причину
                    string checkNet = " Проверьте соединение со сетью";
                    ErrorTxt.Text = checkNet;
                }

                dataBase.CloseConBd();

            }

            else 
            {
                ErrorTxt.Text = "Введите коректно данные";
                LineRegOne.Background = (Brush)bc.ConvertFrom("#FFF43030");
                LineRegOne.Opacity = 1;
                LineRegTwo.Background = (Brush)bc.ConvertFrom("#FFF43030");
                LineRegTwo.Opacity = 1;
                LineRegThree.Background = (Brush)bc.ConvertFrom("#FFF43030");
                LineRegThree.Opacity = 1;
                LineRegFour.Background = (Brush)bc.ConvertFrom("#FFF43030");
                LineRegFour.Opacity = 1;
            }

        }

        private void BtnRegSignUpBG_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnRegSignUpBG.Background = (Brush)bc.ConvertFrom("#FF163259");
        }

        private void BtnRegSignUpBG_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnRegSignUpBG.Background = (Brush)bc.ConvertFrom("#FF0A192F");
        }

        private void FullName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //A lock input in the TextBox numbers 
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }

    }
}
