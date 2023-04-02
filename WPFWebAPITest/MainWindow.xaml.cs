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
using WPFWebAPITest.Data;

namespace WPFWebAPITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Refresh()
        {
            dgUsers.ItemsSource = await NetManager.Get<List<Users>>("api/Users/GetAllUsers");
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}