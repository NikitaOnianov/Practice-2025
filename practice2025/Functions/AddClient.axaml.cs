using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using practice2025.Cabinets;
using practice2025.Models;
using System;
using System.Linq;

namespace practice2025;

public partial class AddClient : Window
{
    public AddClient()
    {
        InitializeComponent();
    }

    private void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        using var context = new MydatabaseContext();

        if (string.IsNullOrWhiteSpace(ClientNameBox.Text) ||
            string.IsNullOrWhiteSpace(ClientSurnameBox.Text) ||
            string.IsNullOrWhiteSpace(ClientPatronymicBox.Text) ||
            string.IsNullOrWhiteSpace(ClientSnilsBox.Text) ||
            string.IsNullOrWhiteSpace(ClientPassportBox.Text) ||
            ClientBirthday.SelectedDate == null ||
            string.IsNullOrWhiteSpace(ClientPolisBox.Text) ||
            GenderComboBox.SelectedItem == null)
        {
            mess.Foreground = Brushes.Red;
            mess.Text = "Пожалуйста, заполните все поля!";
            return;
        }

        try
        {
            CorrectInput(); // Вызов проверки ввода
            bool gender = true;
            if (GenderComboBox.SelectedIndex == 0) // узнаём пол пользователя
            {
                gender = true;
            }
            else
            {
                gender = false;
            }
            var NewClient = new Client
            {
                ClientName = ClientNameBox.Text.Trim(),
                ClientSurname = ClientSurnameBox.Text.Trim(),
                ClientPatronymic = ClientPatronymicBox.Text.Trim(),
                ClientSnils = ClientSnilsBox.Text.Trim(),
                ClientPassport = ClientPassportBox.Text.Trim(),
                ClientBirthday = DateOnly.Parse(ClientBirthday.SelectedDate.Value.Date.ToString()),
                ClientPolis = ClientPolisBox.Text.Trim(),
                ClientIsMan = gender
            };

            context.Clients.Add(NewClient);
            context.SaveChanges();

            mess.Foreground = Brushes.Green;
            mess.Text = "Клиент успешно добавлен!";
            ClearFields();
        }
        catch (Exception ex)
        {
            mess.Foreground = Brushes.Red;
            mess.Text = $"Ошибка: {ex.Message} | StackTrace: {ex.StackTrace}";
        }
    }


    private void ClearFields()
    {
        ClientNameBox.Text = "";
        ClientSurnameBox.Text = "";
        ClientPatronymicBox.Text = "";
        ClientSnilsBox.Text = "";
        ClientPassportBox.Text = "";
        ClientPolisBox.Text = "";
    }

    private void CorrectInput()
    {

        if (ClientPassportBox.Text.Length != 10 || !ClientPassportBox.Text.All(char.IsDigit))
        {
            throw new ArgumentException("Паспорт должен содержать ровно 10 цифр");
        }
        if (ClientPolisBox.Text.Length != 16 || !ClientPolisBox.Text.All(char.IsDigit))
        {
            throw new ArgumentException("Полюс должен содержать ровно 16 цифр");
        }
    }
}
