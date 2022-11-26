using Microsoft.Win32;
using MyCourceApp.Scrips;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCourceApp.View
{
    /// <summary>
    /// Логика взаимодействия для settings.xaml
    /// </summary>
    public partial class settings : Page
    {
        BrushConverter bc = new BrushConverter();
        public string Photo { get; private set; }

        DataBase dataBase = new DataBase();


        public void Sss() {

            try
            {
                string connStr = "server=localhost;port=3306;username=root;password=root;database=couseuiop";
                MySqlConnection conn = new MySqlConnection(connStr);

                conn.Open();
                //'" + main.password + "'
                string sql = "SELECT fullname FROM users WHERE password = '" + GetSet.passw + "'";
                string sqlphone = "SELECT phone FROM users WHERE password = '" + GetSet.passw + "'";

                MySqlCommand command = new MySqlCommand(sql, conn);

                string name = command.ExecuteScalar().ToString();
                FIOSett.Text = name;


                MySqlCommand commandPhone = new MySqlCommand(sqlphone, conn);
                string namePhone = commandPhone.ExecuteScalar().ToString();
                PhoneSett.Text = namePhone;


                string sqlPhoto = "SELECT PathAvatar FROM users WHERE password = '" + GetSet.passw + "'";
                MySqlCommand commandPhoto = new MySqlCommand(sqlPhoto, conn);
                string PhotoAva = commandPhoto.ExecuteScalar().ToString();
                avatarSett.ImageSource = new BitmapImage(new Uri(PhotoAva));

                conn.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка!!!");
            }
            
        }

      


       public settings()
        {
            InitializeComponent();
            Sss();

             

        }


        //BTN SEND
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            MySqlCommand command = new MySqlCommand("UPDATE users SET Message = '" + Message.Text + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());
            dataBase.openConBd();
            command.ExecuteNonQuery();
            dataBase.CloseConBd();

            if (Message.Text != "")
            {
                Text_Next_to_send.Text = "Вы успешно отправили сообщение";
                Text_Next_to_send.Foreground = (Brush)bc.ConvertFrom("#FF4DF64F");
                VSznak.Width = 20;
                VSznak.Height = 20;
            }
            
        }


        private static byte[] GetPhoto(string filePath)
        {

            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;

        }

        //
        private void AvatarSett_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files(*.*)|*.*";

            if (open.ShowDialog() == true)
            {
                try
                {

                    Photo = open.FileName;
                    Uri uri = new Uri(Photo);
                    avatarSett.ImageSource = new BitmapImage(new Uri(Photo));
                    GetSet.photo = uri.ToString();
                    MySqlCommand command = new MySqlCommand("UPDATE users SET PathAvatar = '" + GetSet.photo + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());
                    dataBase.openConBd();
                    command.ExecuteNonQuery();
                    dataBase.CloseConBd();

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void AvatarSett_MouseEnter(object sender, MouseEventArgs e)
        {
            AvatarSett.Opacity = 1;
        }

        private void AvatarSett_MouseLeave(object sender, MouseEventArgs e)
        {
            AvatarSett.Opacity = 0.8;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            SendMsg.Opacity = 1;
        }

        private void SendMsg_MouseLeave(object sender, MouseEventArgs e)
        {
            SendMsg.Opacity = 0.8;
        }


        //Remove Akk
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            string Del = "Заявка на удаление";
            MySqlCommand command = new MySqlCommand("UPDATE users SET DeleteAkk = '" + Del + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());
            dataBase.openConBd();
            command.ExecuteNonQuery();
            dataBase.CloseConBd();

            Error.Visibility = Visibility.Visible;



        }

        private void Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Вернуть


            string Del = "Без требований";
            MySqlCommand command = new MySqlCommand("UPDATE users SET DeleteAkk = '" + Del + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());
            dataBase.openConBd();
            command.ExecuteNonQuery();
            dataBase.CloseConBd();
            Error.Visibility = Visibility.Hidden;
        }

        private void Cancel_MouseEnter(object sender, MouseEventArgs e)
        {
            Cancel.Opacity = 1;
        }

        private void Cancel_MouseLeave(object sender, MouseEventArgs e)
        {
            Cancel.Opacity = 0.6;
        }

        private void RemoveAk_MouseEnter(object sender, MouseEventArgs e)
        {
            RemoveAk.Opacity = 1;
        }

        private void RemoveAk_MouseLeave(object sender, MouseEventArgs e)
        {
            RemoveAk.Opacity  = 0.8;
        }

        private void er() {

            string TextActiv = "Заявка на изменение";
            MySqlCommand commanda = new MySqlCommand("UPDATE users SET Renames = '" + TextActiv + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());
            dataBase.openConBd();
            commanda.ExecuteNonQuery();
            dataBase.CloseConBd();
        }
        private void BTNRENAME_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool proverkaPhone = false;

            string TextActiv = "Заявка на изменение";

            string connStr = "server=localhost;port=3306;username=root;password=root;database=couseuiop";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            string info = "SELECT Renames FROM users WHERE password = '" + GetSet.passw + "'";
            MySqlCommand commandPhone = new MySqlCommand(info, conn);
            string _info = commandPhone.ExecuteScalar().ToString();
            conn.Close();
            


            string pattPhone = @"(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})";
            if (Regex.IsMatch(RenamePhone.Text, pattPhone))
            {
                proverkaPhone = true;
            }
            else
            {
                proverkaPhone = false;
            }

            if (proverkaPhone == true && RenameSet.Text.Length > 2 && RenamePhone.Text.Length > 2 && RenamePass.Text.Length > 2 && _info == "Активно")
            {
                MySqlCommand command = new MySqlCommand("UPDATE users SET fullname = '" + RenameSet.Text + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());
                MySqlCommand commandone = new MySqlCommand("UPDATE users SET phone = '" + RenamePhone.Text + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());
                MySqlCommand commandtwo = new MySqlCommand("UPDATE users SET password = '" + RenamePass.Text + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());
                MySqlCommand commanda = new MySqlCommand("UPDATE users SET Renames = '" + TextActiv + "' WHERE password = '" + GetSet.passw + "' ", dataBase.GetConnection());

                dataBase.openConBd();
                commanda.ExecuteNonQuery();
                command.ExecuteNonQuery();
                commandone.ExecuteNonQuery();
                commandtwo.ExecuteNonQuery();

                dataBase.CloseConBd();

                TextIf.Foreground = (Brush)bc.ConvertFrom("#FFBEFF88");
                TextIf.Text = "Вы успешно изменили данные";

            }
            else {
                TextIf.Foreground = (Brush)bc.ConvertFrom("#FFFF4949");
                TextIf.Text = "Введите корректно данные";

                if (_info == TextActiv)
                {
                    TextIf.Foreground = (Brush)bc.ConvertFrom("#FFFF4949");
                    TextIf.Text = "Вы оставили заявку на изменение данных";
                }
                else
                {
                    TextIf.Foreground = (Brush)bc.ConvertFrom("#FFFF4949");
                    TextIf.Text = "Введите корректно данные";
                }
               
            }

        }

    }
}
