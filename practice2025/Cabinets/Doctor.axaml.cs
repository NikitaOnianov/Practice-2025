using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using practice2025.Entry;
using practice2025.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace practice2025.Cabinets;

public partial class Doctor : Window
{
    User User { get; set; }
    static MydatabaseContext db = new MydatabaseContext();
    public Doctor()
    {
        InitializeComponent();
    }
    public Doctor(User user)
    {
        InitializeComponent();
        this.User = user;
        User = user;
        Username.Text = user.UserName;
        Usertype.Text = ((db.UsersTypes.Where(it => it.UserTypeId == user.UserType).First()) as UsersType).UserTypeName;

        ListClients.ItemsSource = Allpatient();
    }
    ObservableCollection<patient_turnoutDTO> Allpatient()
    {
        
        return new ObservableCollection<patient_turnoutDTO>();
    }

    private void Exit(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    private void SaveChage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void update(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ListClients.ItemsSource = Allpatient();
    }
}
