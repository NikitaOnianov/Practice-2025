using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using practice2025.Models;
using practice2025.Entry;
using System.Linq;

namespace practice2025.Cabinets
{
    public partial class UserWindow : Window
    {
        private readonly User User;
        static MydatabaseContext db = new MydatabaseContext();
        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(User user) : this()
        {
            User = user;
            InitializeUserData();
        }

        private void InitializeUserData()
        {
            NameTextBlock.Text = User.UserName;
            TypeTextBlock.Text = ((db.UsersTypes.Where(it => it.UserTypeId == User.UserType).First()) as UsersType).UserTypeName;

        }


        private void CloseAndReturnToMain()
        {
            new MainWindow().Show();
            Close();
        }

        private void OnAddClientClick(object? sender, RoutedEventArgs e)
        {
            new AddClient().ShowDialog(this);
        }

        private void OnLogoutClick(object? sender, RoutedEventArgs e)
        {
            CloseAndReturnToMain();
        }
    }
}
