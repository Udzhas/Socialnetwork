using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string loggedUserId;
        public MyPosts( string loggedUserId)
        {
            this.loggedUserId = loggedUserId;
            InitializeComponent();
            ReadAllMyPosts();
        }

        public void ReadAllMyPosts()
        {
            //List<PostElements> list1 = new List<PostElements>();
            List <PostElements> myPostData = collection_posts.AsQueryable().Where(e => e.user_id == loggedUserId).ToList();
            dgMyPosts.ItemsSource = myPostData;
            PostElements myPost = (PostElements)dgMyPosts.Items.GetItemAt(0);
            tbxMyPostId.Text = myPost.Id.ToString();
            tbxMyPostName.Text = myPost.name;
            tbxMyPostContent.Text = myPost.content;
            tbxMyPostDate.Text = myPost.date.ToString();
            tbxMyPostUserId.Text = myPost.user_id;
            tbxMyPostLikes.Text = myPost.likes.ToString();
            tbxMyPostComments.Clear();
            foreach (var comment in myPost.comments)
            {
                tbxMyPostComments.AppendText(comment + " ");
            }
        }

        private void BackToMainPageClick(object sender, RoutedEventArgs e)
        {
            GeneralWindow objGeneralWindow = new GeneralWindow(loggedUserId);
            this.Visibility = Visibility.Hidden;
            objGeneralWindow.Show();
        }

        private void dgMyPosts_MouseUp(object sender, MouseButtonEventArgs e)
        {
            List<PostElements> list = collection_posts.AsQueryable().ToList<PostElements>();
            PostElements myPost = (PostElements)dgMyPosts.SelectedItem;
                tbxMyPostId.Text = myPost.Id.ToString();
                tbxMyPostName.Text = myPost.name;
                tbxMyPostContent.Text = myPost.content;
                tbxMyPostDate.Text = myPost.date.ToString();
                tbxMyPostUserId.Text = myPost.user_id;
                tbxMyPostLikes.Text = myPost.likes.ToString();
                tbxMyPostComments.Clear();
            foreach (var comment in myPost.comments)
            {
                tbxMyPostComments.AppendText(comment + " ");
            }
        }

        private void CreatePost_CLick(object sender, RoutedEventArgs e)
        {
            try
            {
                PostElements myPost = new PostElements(tbxMyPostId.Text, tbxMyPostName.Text, tbxMyPostContent.Text, Convert.ToDateTime(tbxMyPostDate.Text), Convert.ToInt32(tbxMyPostLikes.Text), tbxMyPostComments.Text.Split(' ').ToList(), tbxMyPostUserId.Text);
                collection_posts.InsertOne(myPost);
                ReadAllMyPosts();
            }
            catch
            {
                MessageBox.Show("No difference detected! Please change post id at least!");
            }

        }
        private void UpdatePost_Click(object sender, RoutedEventArgs e)
        {
            var updateDefinition = Builders<PostElements>.Update.Set("Id", tbxMyPostId.Text)
                                                                .Set("name", tbxMyPostName.Text)
                                                                .Set("content", tbxMyPostContent.Text)
                                                                .Set("date", Convert.ToDateTime(tbxMyPostDate.Text))
                                                                .Set("user_id", tbxMyPostUserId.Text)
                                                                .Set("likes", Convert.ToInt32(tbxMyPostLikes.Text))
                                                                .Set("comments", tbxMyPostContent.Text.Split(' ').ToList());
            collection_posts.UpdateOne(MyPosts => MyPosts.Id == tbxMyPostName.Text, updateDefinition);
            ReadAllMyPosts();

        }

        private void DeletePost_Click(object sender, RoutedEventArgs e)
        {
            collection_posts.DeleteOne(myPosts => myPosts.Id == tbxMyPostName.Text);
            ReadAllMyPosts();
        }

        
    }
}
