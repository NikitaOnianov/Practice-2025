using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using practice2025.Entry;
using practice2025.Functions;
using practice2025.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace practice2025.Cabinets
{
    public partial class UserWindow : Window
    {
        private readonly User User;
        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(User user) : this()
        {
            User = user;
            InitializeUserData();
            ListClients.ItemsSource = Allpatient();
        }

        private void InitializeUserData()
        {
            using (MydatabaseContext db = new MydatabaseContext())
            {
                NameTextBlock.Text = User.UserName;
                TypeTextBlock.Text = ((db.UsersTypes.Where(it => it.UserTypeId == User.UserType).First()) as UsersType).UserTypeName;
            }
        }

        ObservableCollection<patient_turnoutDTO> Allpatient()
        {
            using (MydatabaseContext db = new MydatabaseContext())
            {
                var a = db.Histories
               .Include(i => i.HistoryDiagnosisNavigation)
               .Include(i => i.HistoryClientNavigation)
               //.Where(i => i.HistoryDate == DateOnly.FromDateTime(DateTime.Now))
               .AsEnumerable() // Переключаем на клиентскую сторону для сложных вычислений
               .OrderByDescending(i => i.HistoryDate.ToDateTime(i.HistoryTime))
               .Select(i => new patient_turnoutDTO
               {
                   HistoryDate = i.HistoryDate,
                   HistoryTime = i.HistoryTime,
                   Client = i.HistoryClientNavigation.ClientSurname + " " + i.HistoryClientNavigation.ClientName + " " + i.HistoryClientNavigation.ClientPatronymic,
                   Diagnosis = i.HistoryDiagnosisNavigation.DiagnosisName,
                   HistoryClient = i.HistoryClient,
               }).ToList();
                return new ObservableCollection<patient_turnoutDTO>(a);
            }
        }

        private void CloseAndReturnToMain()
        {
            new MainWindow().Show();
            Close();
        }

        private void OnLogoutClick(object? sender, RoutedEventArgs e)
        {
            CloseAndReturnToMain();
        }

        private void Button_Click(object? sender, RoutedEventArgs e)
        {
            new AddHistory().Show();
        }

        private void Button_Click_1(object? sender, RoutedEventArgs e)
        {
            ListClients.ItemsSource = Allpatient();
        }

        private void Button_Click_2(object? sender, RoutedEventArgs e)
        {
            new AddClient().Show();
        }
    }
}
