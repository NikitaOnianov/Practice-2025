using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Microsoft.EntityFrameworkCore;
using practice2025.Functions;
using practice2025.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace practice2025.Functions;

public partial class AddHistory : Window
{
    public AddHistory()
    {
        InitializeComponent();
        ObservableCollection<Client>? allClients;
        using (MydatabaseContext db = new MydatabaseContext())
        {
            allClients = new ObservableCollection<Client>(db.Clients.ToList());
            clients.ItemsSource = new ObservableCollection<Client>(allClients);
            diagnosis.ItemsSource = new ObservableCollection<Diagnosis>(db.Diagnoses.ToList());
        }
    }

    private void OnAddClientClick(object? sender, RoutedEventArgs e)
    {
        new AddClient().Show();
        Close();
    }

    private void addDiag(object? sender, RoutedEventArgs e)
    {
        new AddDiagnosis().Show();
        Close();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (clients.SelectedItem != null)
            {
                if (diagnosis.SelectedItem != null)
                {
                    using (MydatabaseContext db = new MydatabaseContext())
                    {
                        History history = new History()
                        {
                            HistoryDate = DateOnly.FromDateTime(DateTime.Now),
                            HistoryTime = TimeOnly.FromDateTime(DateTime.Now),
                            HistoryClient = (clients.SelectedItem as Client).ClientId,
                            HistoryDiagnosis = (diagnosis.SelectedItem as Diagnosis).DiagnosisId
                        };
                        db.Add(history);
                        db.SaveChanges();
                    }
                }
                else
                {
                    Mess.Foreground = Brushes.Red;
                    Mess.Text = "Выберите диагноз";
                }
            }
            else
            {
                Mess.Foreground = Brushes.Red;
                Mess.Text = "Выберите пациента";
            }
        }
        catch (Exception ex)
        {
            Mess.Foreground = Brushes.Red;
            Mess.Text = "Ошибка: " + ex.Message;
        }
    }

    private void Button_Click_1(object? sender, RoutedEventArgs e)
    {
        string? searchText = search.Text?.Trim();

        if (string.IsNullOrEmpty(searchText))
        {
            clients.SelectedIndex = -1;
            return;
        }

        try
        {
            using (var db = new MydatabaseContext())
            {
                // Получаем всех клиентов (с фильтрацией в памяти — надёжно для разных СУБД)
                var allClients = db.Clients.AsNoTracking().ToList();

                // Ищем по гибким критериям
                Client? client = FindClientFlexible(allClients, searchText);

                if (client != null)
                {
                    // Перезагружаем ItemsSource, чтобы ComboBox «увидел» клиента
                    clients.ItemsSource = allClients;
                    clients.SelectedItem = client;
                    clients.ScrollIntoView(client);
                }
                else
                {
                    clients.ItemsSource = allClients; // Показываем всех, если не нашли
                    clients.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
            // Логирование/оповещение (на практике используйте logger)
            Console.WriteLine($"Ошибка поиска: {ex.Message}");
            clients.SelectedIndex = -1;
        }
    }

    private Client? FindClientFlexible(List<Client> allClients, string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return null;

        var parts = searchText
            .Trim()
            .Split(new[] { ' ', '\t', '-', '.' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.Trim().ToLower())
            .Where(p => !string.IsNullOrEmpty(p))
            .ToList();

        if (!parts.Any())
            return null;

        foreach (var client in allClients)
        {
            // Проверяем паспортные данные (СНИЛС, полис, паспорт)
            bool matchesId = parts.Any(part =>
                (client.ClientSnils?.Contains(part, StringComparison.OrdinalIgnoreCase) == true) ||
                (client.ClientPolis?.Contains(part, StringComparison.OrdinalIgnoreCase) == true) ||
                (client.ClientPassport?.Contains(part, StringComparison.OrdinalIgnoreCase) == true));


            if (matchesId)
                return client;

            // Проверяем ФИО (имя, фамилия, отчество)
            bool matchesName = parts.Any(part =>
                (client.ClientName?.Contains(part, StringComparison.OrdinalIgnoreCase) == true) ||
                (client.ClientSurname?.Contains(part, StringComparison.OrdinalIgnoreCase) == true) ||
                (client.ClientPatronymic?.Contains(part, StringComparison.OrdinalIgnoreCase) == true));


            if (matchesName)
                return client;
        }

        return null;
    }
}