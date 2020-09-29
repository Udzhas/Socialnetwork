using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
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


namespace socialnetwork_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {

        public string loggedUserId;
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase db = client.GetDatabase("users");
        static IMongoCollection<UserElements> collection = db.GetCollection<UserElements>("users");

        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserElements user = new UserElements();
            List<UserElements> userData = collection.AsQueryable().Where(e => e.userName == tbxUserName.Text).ToList();
            if(userData[0].password == tbxPassword.Text)
            {
                loggedUserId = userData[0].Id;
                GeneralWindow objGeneralWindow = new GeneralWindow(loggedUserId);
                this.Visibility = Visibility.Hidden;
                objGeneralWindow.Show();
            }
            else
            {
                MessageBox.Show("Wrong user name or password! Please try again.");
            }
        }
    }
}
