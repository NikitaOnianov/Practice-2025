using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using practice2025.Cabinets;
using practice2025.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace practice2025.Entry;

public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
        using (MydatabaseContext db = new MydatabaseContext())
        {
            ObservableCollection<UsersType>? type = null;
            ObservableCollection<User>? users = new ObservableCollection<User>(
                db.Users.Where(it => it.UserType == 2)
                );
            if (users.Count != 0)
            {
                type = new ObservableCollection<UsersType>(db.UsersTypes.Where(it => it.UserTypeId != 2));
            }
            else
            {
                type = new ObservableCollection<UsersType>(db.UsersTypes);
            }
            ObservableCollection<User>? users2 = new ObservableCollection<User>(
                db.Users.Where(it => it.UserType == 3)
                );
            if (users2.Count != 0)
            {
                type = new ObservableCollection<UsersType>(type.Where(it => it.UserTypeId != 3));
            }

            ListPosition.ItemsSource = type; // получаем итоговый список должностей
            ListPosition.SelectedIndex = 0;
        }
    }

    private void auth(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    private void reg(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            string? name = Name.Text;
            string? login = Login.Text;
            string? password = Password.Text;
            int? i = (ListPosition.SelectedItem as UsersType).UserTypeId;

            using (MydatabaseContext db = new MydatabaseContext())
            {
                if (name != null && login != null && password != null)
                {
                    User user = new User()
                    {
                        UserName = name,
                        UserLogin = login,
                        UserPassword = password,
                        UserType = (int)i
                    };
                    db.Add(user);
                    db.SaveChanges();

                    new Registration().Show();
                    Close();
                }
                else
                {
                    Mess.Text = "Введите все данные";
                }
            }
        }
        catch 
        {
            Mess.Text = "Произошла ошибка";
        }
    }
}
