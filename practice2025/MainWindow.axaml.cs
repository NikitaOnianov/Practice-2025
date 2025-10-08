using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using practice2025.Models;
using Tmds.DBus.Protocol;

namespace practice2025;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }



    private void TogglePasswordVisibility(object? sender, RoutedEventArgs e)
    {
        PasswordBox.PasswordChar = PasswordBox.PasswordChar == '*' ? '\0' : '*';
    }

    private void AuthorizeButton(object? sender, RoutedEventArgs e)
    {
        using var context = new MydatabaseContext();
        var user = context.Users.FirstOrDefault(it => it.UserLogin == LoginBox.Text && it.UserPassword == PasswordBox.Text);

        if (user != null)
        {
            var functionWindow = new UserWindow(user);
            {
                DataContext = user;
            }
             ;
            functionWindow.ShowDialog(this);
        }
        else
        {
            ErrorBlock.Text = "Неверный пароль";
        }
    }
}