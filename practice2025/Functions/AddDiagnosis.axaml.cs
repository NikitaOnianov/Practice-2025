using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using practice2025.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace practice2025.Functions;

public partial class AddDiagnosis : Window
{
    public AddDiagnosis()
    {
        InitializeComponent();
        using (MydatabaseContext db = new MydatabaseContext())
        {
            ListMedicalDepartment.ItemsSource = new ObservableCollection<MedicalDepartment>(db.MedicalDepartments.ToList());
        }
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            if (ListMedicalDepartment.SelectedItem == null || name.Text == "")
            {
                return;
            }
            using (MydatabaseContext db = new MydatabaseContext())
            {
                Diagnosis diagnosis = new Diagnosis()
                {
                    DiagnosisName = name.Text,
                    DiagnosisMedicalDepartment = (ListMedicalDepartment.SelectedItem as MedicalDepartment).MedicalDepartmentId
                };
                db.Diagnoses.Add(diagnosis);
                db.SaveChanges();
            }
            Close();
        }
        catch(Exception ex)
        {
            mess.Foreground = Brushes.Red;
            mess.Text = "Ошибка" + ex.Message;
        }
    }
}