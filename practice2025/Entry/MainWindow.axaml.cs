using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using practice2025.Cabinets;
using practice2025.Models;
using Tmds.DBus.Protocol;

namespace practice2025.Entry;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ImagePath.Source = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + @"Assets\logo.png");
    }

    private void registration(object? sender, RoutedEventArgs e)
    {
        new Registration().Show();
        Close();
    }

    private void TogglePasswordVisibility(object? sender, RoutedEventArgs e)
    {
        PasswordTextBox.PasswordChar = PasswordTextBox.PasswordChar == '*' ? '\0' : '*';
    }

    private void AuthorizeButton(object? sender, RoutedEventArgs e)
    {
        using var context = new MydatabaseContext();
        var user = context.Users.FirstOrDefault(it => it.UserLogin == LoginTextBox.Text && it.UserPassword == PasswordTextBox.Text);

        if (user != null)
        {
            if (user.UserType == 1)
            {
                new UserWindow(user).Show();
                Close();
            }
            else if (user.UserType == 2)
            {
                new Administrator(user).Show();
                Close();
            }
            else if (user.UserType == 3)
            {
                new Chief_Medical_Officer(user).Show();
                Close();
            }
            else
            {
                new Dockor(user).Show();
                Close();
            }
        }
        else
        {
            ErrorBlock.Text = "Пользователь не найден";
        }
    }
}
