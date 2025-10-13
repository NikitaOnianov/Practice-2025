using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using practice2025.Functions;
using practice2025.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace practice2025.Functions;

public partial class AddHistory : Window
{
    public AddHistory()
    {
        InitializeComponent();
        using (MydatabaseContext db = new MydatabaseContext())
        {
            clients.ItemsSource = new ObservableCollection<Client>(db.Clients.ToList());
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
        catch (Exception ex)
        {
            Mess.Foreground = Brushes.Red;
            Mess.Text = "Ошибка: " + ex.Message;
        }
    }
}