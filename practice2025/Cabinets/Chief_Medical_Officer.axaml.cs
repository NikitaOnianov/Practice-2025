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
                try
                {
                    UserDTO userDTO = ListUsers.SelectedItem as UserDTO;
                    if (userDTO != null)
                    {
                        User user = db.Users.FirstOrDefault(i => i.UserId == userDTO.UserId);

                        if (user != null)
                        {
                            db.Users.Remove(user);
                            db.SaveChanges();
                            userDTOs.Remove(userDTO);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении пользователя: {ex.Message}");
                }
            }
        }
    }

    private void removeClient(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (ListClients.SelectedItem != null)
        {
            using (MydatabaseContext db = new MydatabaseContext())
            {
                try
                {
                    Client client = ListClients.SelectedItem as Client;
                    if (client != null)
                    {
                        db.Clients.Remove(client);
                        db.SaveChanges();

                        ListClients.ItemsSource = new ObservableCollection<Client>(db.Clients);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении клиента: {ex.Message}");
                }
            }
        }
    }


    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        using (MydatabaseContext db = new MydatabaseContext())
        {
            ListClients.ItemsSource = new ObservableCollection<Client>(db.Clients);
        }
    }
}