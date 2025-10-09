using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using practice2025.Models;
using practice2025.Entry;

namespace practice2025
{
    public partial class UserWindow : Window
    {
        private readonly User _user;

        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(User user) : this()
        {
            _user = user;
            InitializeUserData();
        }

        private void InitializeUserData()
        {
            NameTextBlock.Text = _user.UserName;
            TypeTextBlock.Text = _user.UserType == 2 ? "Администратор" : "Оператор ЭВМ";

            AddClientButton.IsVisible = _user.UserType == 2; // Кнопка добавления клиента доступна только для администраторов
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
