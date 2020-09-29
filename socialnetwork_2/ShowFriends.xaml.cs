
using MongoDB.Bson;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase db = client.GetDatabase("users");
        static IMongoCollection<UserElements> collection = db.GetCollection<UserElements>("users");
        private string loggedUserId;

        public Window1()
        {
            InitializeComponent();
            ReadAllDocuments();
        }

        public void ReadAllDocuments()
        {
            List<UserElements> list = collection.AsQueryable().ToList<UserElements>();
            dgUsers.ItemsSource = list;
            UserElements user = (UserElements)dgUsers.Items.GetItemAt(0);
            tbxId.Text = user.Id;
            tbxUsername.Text = user.userName;
            tbxPassword.Text = user.password;
            tbxFirstName.Text = user.first_name;
            tbxLastName.Text = user.last_name;
            tbxInterests.Clear();
            foreach(var interest in user.interests)
            {
                tbxInterests.AppendText(interest + " ");
            }
           
        }

        private void dgUsers_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UserElements user = (UserElements)dgUsers.SelectedItem;
            tbxId.Text = user.Id;
            tbxUsername.Text = user.userName;
            tbxPassword.Text = user.password;
            tbxFirstName.Text = user.first_name;
            tbxLastName.Text = user.last_name;
            tbxInterests.Clear();
            foreach (var interest in user.interests)
            {
                tbxInterests.AppendText(interest + " ");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserElements user = new UserElements(tbxId.Text, tbxUsername.Text, tbxPassword.Text, tbxFirstName.Text, tbxLastName.Text, tbxInterests.Text.Split(' ').ToList());
            collection.InsertOne(user);
            ReadAllDocuments();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var updateDefinition = Builders<UserElements>.Update.Set("Id", tbxId.Text)
                                                                .Set("username", tbxUsername.Text)
                                                                .Set("password", tbxPassword.Text)
                                                                .Set("first_name", tbxFirstName.Text)
                                                                .Set("last_name", tbxLastName.Text)
                                                                .Set("interests", tbxInterests.Text.Split(' ').ToList());
                                                                
            collection.UpdateOne(user => user.Id.ToString() == tbxId.Text, updateDefinition);
            ReadAllDocuments();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            collection.DeleteOne(user => user.Id.ToString() == tbxId.Text);
            ReadAllDocuments();
        }

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            GeneralWindow objGeneralWindow = new GeneralWindow(loggedUserId);
            this.Visibility = Visibility.Hidden;
            objGeneralWindow.Show();
        }
    }
}
