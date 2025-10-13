using Avalonia.Controls;
using Avalonia.Interactivity;
using practice2025.Entry;
using practice2025.Functions;
using practice2025.Models;
using System;
using System.Linq;

namespace practice2025.Cabinets
{
    public partial class UserWindow : Window
    {
        private readonly User User;
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
            using (MydatabaseContext db = new MydatabaseContext())
            {
                NameTextBlock.Text = User.UserName;
                TypeTextBlock.Text = ((db.UsersTypes.Where(it => it.UserTypeId == User.UserType).First()) as UsersType).UserTypeName;
            }
        }


        private void CloseAndReturnToMain()
        {
            new MainWindow().Show();
            Close();
        }

        private void OnLogoutClick(object? sender, RoutedEventArgs e)
        {
            CloseAndReturnToMain();
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            new AddHistory().Show();
        }
    }
}
