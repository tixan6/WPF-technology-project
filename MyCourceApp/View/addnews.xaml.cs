using Microsoft.Win32;
using MyCourceApp.Scrips;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCourceApp.View
{

    /// <summary>
    /// Логика взаимодействия для addnews.xaml
    /// </summary>
    public partial class addnews : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataTable _itemsMain; // не используйте _items в других местах кода

        public DataTable ItemsMain    // вместо этого используйте именно Items
        {
            get => _itemsMain;
            set
            {
                DataContext = this;
                _itemsMain = value;
                PropertyChanged?.DynamicInvoke(new PropertyChangedEventArgs(nameof(ItemsMain)));
            }

        }


        DataBase dataBase = new DataBase();
        public addnews()
        {
            InitializeComponent();
            DataContext = this;
            LoadData();
        }

        private void LoadData()
        {

            MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=couseuiop");
            conn.Open();
            string cmd = "SELECT * FROM news WHERE pass = '" + GetSet.passw + "'"; // Из какой таблицы нужен вывод                                                                  
            MySqlCommand createCommand = new MySqlCommand(cmd, conn);
            createCommand.ExecuteNonQuery();

            MySqlDataAdapter dataReader = new MySqlDataAdapter(createCommand);
            DataTable dt = new DataTable("news"); // В скобках указываем название таблицы
            dataReader.Fill(dt);
            ItemsMain = dt; // Сам вывод 
            conn.Close();
        }


        public string Photo = null;

        //Button create news

        //Метод для конвертации в bin фото
        public static byte[] GetPhoto(string filePath)
        {

            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String newsaddtext = "Статья добавлена";
            byte[] photo = GetPhoto(Photo);

            MySqlCommand command = new MySqlCommand("INSERT INTO `news` (`img`, `text`,`pass`) VALUES (@photo, @text, @pass)", dataBase.GetConnection());
            command.Parameters.Add("@photo", MySqlDbType.Blob).Value = photo;
            command.Parameters.Add("@text", MySqlDbType.VarChar).Value = txtnews.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = GetSet.passw;

            dataBase.openConBd();
            command.ExecuteNonQuery();
            newsAddText.Text = newsaddtext;


        }

        //Btn add photo
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Code
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files(*.*)|*.*";

            if (open.ShowDialog() == true)
            {
                try
                {

                    Photo = open.FileName;
                    Uri uri = new Uri(Photo);
                    imageAdd.Source = new BitmapImage(new Uri(Photo));
                    GetSet.photo = Photo;

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            AddPhoto.Opacity = 1;
        }

        private void AddPhoto_MouseLeave(object sender, MouseEventArgs e)
        {
            AddPhoto.Opacity = 0.6;
        }

        //Трансляция

        private void txtnews_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            txtPrim.Text = txtnews.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connStr = "server=localhost;port=3306;username=root;password=root;database=couseuiop";
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                try
                {
                    string sql = "DELETE FROM news WHERE pass = '" + GetSet.passw +"'";

                    connection.Open();

                    MySqlCommand command = new MySqlCommand(sql, connection);

                    command.ExecuteNonQuery();
                    YouHaveNot.Visibility = Visibility.Visible;
                }

                catch
                {
                    YouHaveNot.Visibility = Visibility.Visible;
                }
            }
        }
    }
}