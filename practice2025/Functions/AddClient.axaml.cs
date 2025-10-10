using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using practice2025.Models;
using practice2025.Cabinets;

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
            string.IsNullOrWhiteSpace(ClientBirthdayBox.Text) ||
            string.IsNullOrWhiteSpace(ClientPolisBox.Text))
        {
            UserNotAdd.Text = "Пожалуйста, заполните все поля!";
            return;
        }

        try
        {
            CorrectInput(); // Вызов проверки ввода

            var NewClient = new Client
            {
                ClientName = ClientNameBox.Text.Trim(),
                ClientSurname = ClientSurnameBox.Text.Trim(),
                ClientPatronymic = ClientPatronymicBox.Text.Trim(),
                ClientSnils = ClientSnilsBox.Text.Trim(),
                ClientPassport = ClientPassportBox.Text.Trim(),
                ClientBirthday = DateOnly.Parse(ClientBirthdayBox.Text),
                ClientPolis = ClientPolisBox.Text.Trim(),
            };

            context.Clients.Add(NewClient);
            context.SaveChanges();

            UserAdd.Text = "Клиент успешно добавлен!";
            ClearFields();
        }
        catch (Exception ex)
        {
            UserNotAdd.Text = $"Ошибка: {ex.Message} | StackTrace: {ex.StackTrace}";
        }
    }


    private void ClearFields()
    {
        ClientNameBox.Text = "";
        ClientSurnameBox.Text = "";
        ClientPatronymicBox.Text = "";
        ClientSnilsBox.Text = "";
        ClientPassportBox.Text = "";
        ClientBirthdayBox.Text = "";
        ClientPolisBox.Text = "";
    }

    private void CorrectInput()
    {

        if (ClientPassportBox.Text.Length != 10 || !ClientPassportBox.Text.All(char.IsDigit))
        {
            throw new ArgumentException("Паспорт должен содержать ровно 10 цифр");
        }

        if (!DateOnly.TryParse(ClientBirthdayBox.Text, out _))
        {
            throw new ArgumentException("Некорректный формат даты рождения");
        }
    }

    private void BackOnUser(object? sender, RoutedEventArgs e)
    {
        new UserWindow().ShowDialog(this);
    }

}