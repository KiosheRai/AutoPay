﻿using System;
using System.Collections.Generic;
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

namespace AutoPay
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }


        private void Button_Main(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Button_Drivers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Drivers.xaml", UriKind.Relative));
        }

        private void Button_Mark(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void Button_Caclulate(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {

        }
    }
}
