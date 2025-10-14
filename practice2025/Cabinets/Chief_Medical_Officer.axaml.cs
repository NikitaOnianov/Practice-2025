using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using practice2025.Entry;
using practice2025.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace practice2025.Cabinets;

public partial class Chief_Medical_Officer : Window
{
    User User { get; set; }
    ObservableCollection<UserDTO> userDTOs { get; set; }
    public Chief_Medical_Officer()
    {
        InitializeComponent();
    }
    public Chief_Medical_Officer(User user)
    {
        using (MydatabaseContext db = new MydatabaseContext())
        {
            InitializeComponent();
            this.User = user;
            User = user;
            Username.Text = user.UserName;
            Usertype.Text = ((db.UsersTypes.Where(it => it.UserTypeId == user.UserType).First()) as UsersType).UserTypeName;
            ListClients.ItemsSource = new ObservableCollection<Client>(db.Clients);

            userDTOs = new ObservableCollection<UserDTO>(db.Users.Include(i=>i.UserTypeNavigation).Select(i=> new UserDTO()
            {
                UserId = i.UserId,
                UserLogin = i.UserLogin,
                UserName = i.UserName,
                UserType = i.UserTypeNavigation.UserTypeName
            }).ToList());
            UserDTO userdto = userDTOs.Where(i=>i.UserId == user.UserId).First();
            userDTOs.Remove(userdto);



            ListUsers.ItemsSource = userDTOs;
        }
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

    private void DelUser(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (ListUsers.SelectedItem != null)
        {
            using (MydatabaseContext db = new MydatabaseContext())
            {
                UserDTO UserDTO = ListUsers.SelectedItem as UserDTO;
                User user = db.Users.Where(i => i.UserId == UserDTO.UserId).First();
                db.Users.Remove(user);
                userDTOs.Remove(UserDTO);
            }
        }
    }

    private void heir(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        using (MydatabaseContext db = new MydatabaseContext())
        {
            ListClients.ItemsSource = new ObservableCollection<Client>(db.Clients);
        }
    }
}