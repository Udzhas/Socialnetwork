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
    /// Interaction logic for ShowPosts.xaml
    /// </summary>
    public partial class ShowPosts : Window 
    { 
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase db = client.GetDatabase("users");
        static IMongoCollection<UserElements> collection = db.GetCollection<UserElements>("posts");
    
        public ShowPosts()
        {
            InitializeComponent();
        }

        public void ReadAllPosts()
        {
            
        }

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            GeneralWindow objGeneralWindow = new GeneralWindow();
            this.Visibility = Visibility.Hidden;
            objGeneralWindow.Show();
        }
    }
}
