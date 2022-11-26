using MyCourceApp.Scrips;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для _Kyrs.xaml
    /// </summary>
    public partial class _Kyrs : Page
    {
        string _ENGF = @"D:\Study\C#\Курсовая Жени\MyCourceApp\MyCourceApp\Images\Eng.png";
        string _EURF = @"D:\Study\C#\Курсовая Жени\MyCourceApp\MyCourceApp\Images\Rus.png";
        string _RUSF = @"D:\Study\C#\Курсовая Жени\MyCourceApp\MyCourceApp\Images\EUR.png";
        
        public _Kyrs()
        {
            
            InitializeComponent();
            ENGF.ImageSource = new BitmapImage(new Uri(_ENGF));
            EURF.ImageSource = new BitmapImage(new Uri(_RUSF));
            RUBF.ImageSource = new BitmapImage(new Uri(_EURF));

            BuyD.Text = GetSet.BuyD;
            SellD.Text = GetSet.SellD;
            SellE.Text = GetSet.SellE;
            BuyE.Text = GetSet.BuyE;
            BuyR.Text = GetSet.BuyR;
            SellR.Text = GetSet.SellR;

        }
    }
}
