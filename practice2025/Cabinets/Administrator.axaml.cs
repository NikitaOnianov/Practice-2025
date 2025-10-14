using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using practice2025.Entry;
using practice2025.Models;
using System.Linq;

namespace practice2025.Cabinets;

public partial class Administrator : Window
{
    User? User { get; set; }
    static MydatabaseContext db = new MydatabaseContext();
    public Administrator()
    {
        InitializeComponent();
    }
    public Administrator(User user)
    {
        InitializeComponent();
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
        new AddClient().Show();
    }

    private void DelClient(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void DelUser(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }
}
