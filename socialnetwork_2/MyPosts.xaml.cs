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
    /// Interaction logic for MyPosts.xaml
    /// </summary>
    public partial class MyPosts : Window
    {
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase db = client.GetDatabase("users");
        static IMongoCollection<PostElements> collection_posts = db.GetCollection<PostElements>("posts");
        public MyPosts()
        {
            InitializeComponent();
            ReadAllMyPosts();
        }

        public void ReadAllMyPosts()
        {
            
            List <PostElements> list = collection_posts.AsQueryable().ToList<PostElements>();
            dgMyPosts.ItemsSource = list;
            PostElements myPost = (PostElements)dgMyPosts.Items.GetItemAt(0);   
            
        }

        public void sortPosts()
        {

        }

        private void BackToMainPageClick(object sender, RoutedEventArgs e)
        {
            GeneralWindow objGeneralWindow = new GeneralWindow();
            this.Visibility = Visibility.Hidden;
            objGeneralWindow.Show();
        }

        private void dgMyPosts_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
