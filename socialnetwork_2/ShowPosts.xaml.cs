using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace socialnetwork_2
{
    /// <summary>
    /// Interaction logic for ShowPosts.xaml
    /// </summary>
    public partial class ShowPosts : Window 
    { 
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase db = client.GetDatabase("users");
        static IMongoCollection<PostElements> collection_posts = db.GetCollection<PostElements>("posts");
    
        public ShowPosts()
        {
            InitializeComponent();
            ReadAllPosts();
        }

        public static void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Descending)
        {
            var column = dataGrid.Columns[columnIndex];

            // Clear current sort descriptions
            dataGrid.Items.SortDescriptions.Clear();

            // Add the new sort description
            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));

            // Apply sort
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;

            // Refresh items to display sort
            dataGrid.Items.Refresh();
        }

        public void ReadAllPosts()
        {
            List<PostElements> list = collection_posts.AsQueryable().ToList<PostElements>();
            dgPosts.ItemsSource = list;
            PostElements post = (PostElements)dgPosts.Items.GetItemAt(0);
            tbxPostId.Text = post.Id.ToString();
            tbxPostName.Text = post.name;
            tbxPostContent.Text = post.content;
            tbxPostdate.Text = post.date.ToString();
            tbxPostComment.Clear();
            foreach(var comment in post.comments)
            {
                tbxPostComment.AppendText(comment + " ");
            }
            tbxPostlikes.Text = post.likes.ToString();
            tbxPostUser.Text = post.user_id;
            

        }

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            GeneralWindow objGeneralWindow = new GeneralWindow();
            this.Visibility = Visibility.Hidden;
            objGeneralWindow.Show();
        }

        private void btnLeaveComment_Click(object sender, RoutedEventArgs e)
        {
            var leaveComment = Builders<PostElements>.Update.Set("comments", tbxPostComment.Text.Split(' ').ToList().Append(tbxPostLeaveComment.Text));
            collection_posts.UpdateOne(post => post.Id == ObjectId.Parse(tbxPostId.Text), leaveComment);
            ReadAllPosts();
        }

        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            var addLike = Builders<PostElements>.Update.Set("likes", tbxPostlikes.Text = (Convert.ToInt32(tbxPostlikes.Text) + 1).ToString());
            collection_posts.UpdateOne(post => post.Id == ObjectId.Parse(tbxPostId.Text), addLike);
            ReadAllPosts();
            
        }

        private void dgPosts_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PostElements post = (PostElements)dgPosts.SelectedItem;
            tbxPostId.Text = post.Id.ToString();
            tbxPostName.Text = post.name;
            tbxPostContent.Text = post.content;
            tbxPostdate.Text = post.date.ToString();
            tbxPostComment.Clear();
            foreach (var comment in post.comments)
            {
                tbxPostComment.AppendText(comment + " ");
            }
            tbxPostlikes.Text = post.likes.ToString();
            tbxPostUser.Text = post.user_id;
            SortDataGrid(dgPosts, 3, ListSortDirection.Descending);
        }
    }
}
