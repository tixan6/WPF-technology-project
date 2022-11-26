using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace MyCourceApp.View
{
    /// <summary>
    /// Логика взаимодействия для news.xaml
    /// </summary>

    public partial class news : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataTable _items; // не используйте _items в других местах кода
        
        //DataTable
        public DataTable Items    // вместо этого используйте именно Items
        {
            get => _items;
            set
            {
                DataContext = this;
                _items = value;
                //MessageBox.Show(nameof(Items));
                PropertyChanged?.DynamicInvoke(new PropertyChangedEventArgs(nameof(Items)));
                //DynamicInvoke
            }

        }
        public news()
        {
            InitializeComponent();
            DataContext = this;
            LoadData();
            
        }

        private void LoadData()
        {
           

            MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=couseuiop");
            conn.Open();
            string cmd = "SELECT * FROM news"; // Из какой таблицы нужен вывод 
            MySqlCommand createCommand = new MySqlCommand(cmd, conn);
            createCommand.ExecuteNonQuery();

            MySqlDataAdapter dataReader = new MySqlDataAdapter(createCommand);
            DataTable dt = new DataTable("news"); // В скобках указываем название таблицы
            dataReader.Fill(dt);
            Items = dt; // Сам вывод 
            conn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Оценка принята");
        }


        //static DataTable ExecuteSql(string sql)
        //{
        //    DataTable dt = new DataTable();
        //    MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=couseuiop");
        //    using (conn)
        //    {
        //        conn.Open();

        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        MySqlDataReader read = cmd.ExecuteReader();

        //        using (read)
        //        {
        //            dt.Load(read);
        //        }
        //    }

        //    return dt;
        //}



        //static DataTable MyExecuteSql(string sql)
        //{
        //    DataTable dt = new DataTable();
        //    MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=couseuiop");

        //    using (conn)
        //    {
        //        conn.Open();

        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        MySqlDataReader read = cmd.ExecuteReader();

        //        using (read)
        //        {
        //            dt.Load(read);
        //        }
        //    }

        //    return dt;
        //}

    }
}
