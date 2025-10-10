using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using practice2025.Entry;
using practice2025.Models;
using System.Linq;

namespace practice2025.Cabinets;

public partial class Chief_Medical_Officer : Window
{
    User User { get; set; }
    static MydatabaseContext db = new MydatabaseContext();
    public Chief_Medical_Officer()
    {
        InitializeComponent();
    }
    public Chief_Medical_Officer(User user)
    {
        InitializeComponent();
        this.User = user;
        User = user;
        Username.Text = user.UserName;
        Usertype.Text = ((db.UsersTypes.Where(it => it.UserTypeId == user.UserType).First()) as UsersType).UserTypeName;
    }
    private void Exit(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    private void addClient(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void DelClient(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void DelUser(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void heir(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }
}