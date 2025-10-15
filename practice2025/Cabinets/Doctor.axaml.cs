using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Microsoft.EntityFrameworkCore;
using practice2025.Entry;
using practice2025.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace practice2025.Cabinets;

public partial class Doctor : Window
{
    User User { get; set; }
    int idMedicalDepartment;
    public Doctor()
    {
        InitializeComponent();
    }
    public Doctor(User user)
    {
        InitializeComponent();
        using (MydatabaseContext db = new MydatabaseContext())
        {
            this.User = user;
            User = user;
            Username.Text = user.UserName;
            Usertype.Text = ((db.UsersTypes.Where(it => it.UserTypeId == user.UserType).First()) as UsersType).UserTypeName;

            int idType = user.UserType;
            idMedicalDepartment = (int)(db.UsersTypes.Where(it => it.UserTypeId == idType).First()).UsersTypeMedicalDepartments;
        }

            ListClients.ItemsSource = Allpatient(idMedicalDepartment);
    }
    ObservableCollection<patient_turnoutDTO> Allpatient(int id)
    {
        using (MydatabaseContext db = new MydatabaseContext())
        {
            var a = db.Histories
           .Include(i => i.HistoryDiagnosisNavigation)
           .Include(i => i.HistoryClientNavigation)
           .Where(i => i.HistoryDiagnosisNavigation.DiagnosisMedicalDepartmentNavigation.MedicalDepartmentId == id)
           //.Where(i => i.HistoryDate == DateOnly.FromDateTime(DateTime.Now))
           .AsEnumerable() // Переключаем на клиентскую сторону для сложных вычислений
           .OrderBy(i => i.HistoryDate.ToDateTime(i.HistoryTime))
           .Select(i => new patient_turnoutDTO
           {
               HistoryDate = i.HistoryDate,
               HistoryTime = i.HistoryTime,
               Client = i.HistoryClientNavigation.ClientSurname + " " + i.HistoryClientNavigation.ClientName + " " + i.HistoryClientNavigation.ClientPatronymic,
               Diagnosis = i.HistoryDiagnosisNavigation.DiagnosisName,
               HistoryClient = i.HistoryClient,
               Presence = i.HistoryStatus,
           }).ToList();
            return new ObservableCollection<patient_turnoutDTO>(a);
        }
    }

    private void Exit(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    private void SaveChage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            using (MydatabaseContext db = new MydatabaseContext())
            {
                if (ListClients.ItemsSource is ObservableCollection<patient_turnoutDTO> list)
                {
                    foreach (var dto in list)
                    {
                        var objdb = db.Histories
                            .FirstOrDefault(dt => dt.HistoryDate == dto.HistoryDate && dt.HistoryTime == dto.HistoryTime);

                        if (objdb != null)
                        {
                            objdb.HistoryStatus = dto.IsPresent;
                        }
                    }

                    db.SaveChanges();
                    Mes.Foreground = Brushes.Green;
                    Mes.Text = "Изменения сохранены успешно!";

                    //ListClients.ItemsSource = AllD((groups.SelectedItem as Group)!.GroupId);
                }
            }

        }
        catch (Exception ex)
        {
            Mes.Foreground = Brushes.Red;
            Mes.Text = $"Ошибка сохранения: {ex.Message}";
        }
    }

    private void update(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ListClients.ItemsSource = Allpatient(idMedicalDepartment);
    }
}
