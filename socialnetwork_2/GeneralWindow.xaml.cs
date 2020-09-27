using MongoDB.Driver;
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
using System.Windows.Shapes;

namespace socialnetwork_2
{
    /// <summary>
    /// Interaction logic for GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        public GeneralWindow()
        {
            InitializeComponent();
        }

        private void ShowFriends_Click(object sender, RoutedEventArgs e)
        {
            Window1 objShowFriends = new Window1();
            this.Visibility = Visibility.Hidden;
            objShowFriends.Show();
        }

        private void ShowPosts_Click(object sender, RoutedEventArgs e)
        {
            ShowPosts objShowPosts = new ShowPosts();
            this.Visibility = Visibility.Hidden;
            objShowPosts.Show();
        }
    }
}
