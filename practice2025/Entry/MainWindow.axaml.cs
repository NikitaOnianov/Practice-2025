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
        try
        {
            using (MydatabaseContext db = new MydatabaseContext())
            {
                User? user = db.Users.FirstOrDefault(it => it.UserLogin == LoginTextBox.Text && it.UserPassword == PasswordTextBox.Text);

                if (user != null)
                {
                    int id = user.UserType;
                    switch (id)
                    {
                        case 1: new Administrator(user).Show(); Close(this); break;
                        case 2 or 55: new Chief_Medical_Officer(user).Show(); Close(this); break;
                        case 62: new UserWindow(user).Show(); Close(this); break;
                        case int n when (n >= 35 && n <= 44): new Doctor(user).Show(); Close(this); break;
                        default: break;
                    }
                }
                else
                {
                    ErrorBlock.Text = "Пользователь не найден";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorBlock.Text = "Ошибка" + ex.Message;
        }
        
    }
}
