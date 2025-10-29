using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace practice2025.Models;

public partial class MydatabaseContext : DbContext
{
    public MydatabaseContext()
    {
    }

    public MydatabaseContext(DbContextOptions<MydatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<MedicalDepartment> MedicalDepartments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersType> UsersTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=176.108.251.27;Port=5432;Database=mydatabase;Username=userdb;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("clients_pk");

            entity.ToTable("clients");

            entity.Property(e => e.ClientId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("client_id");
            entity.Property(e => e.ClientBirthday).HasColumnName("client_birthday");
            entity.Property(e => e.ClientIsMan)
                .HasDefaultValue(true)
                .HasColumnName("client_is_man");
            entity.Property(e => e.ClientName)
                .HasColumnType("character varying")
                .HasColumnName("client_name");
            entity.Property(e => e.ClientPassport)
                .HasColumnType("character varying")
                .HasColumnName("client_passport");
            entity.Property(e => e.ClientPatronymic)
                .HasColumnType("character varying")
                .HasColumnName("client_patronymic");
            entity.Property(e => e.ClientPolis)
                .HasColumnType("character varying")
                .HasColumnName("client_polis");
            entity.Property(e => e.ClientSnils)
                .HasColumnType("character varying")
                .HasColumnName("client_snils");
            entity.Property(e => e.ClientSurname)
                .HasColumnType("character varying")
                .HasColumnName("client_surname");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.DiagnosisId).HasName("diagnosis_pk");

            entity.ToTable("diagnosis");

            entity.Property(e => e.DiagnosisId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("diagnosis_id");
            entity.Property(e => e.DiagnosisMedicalDepartment).HasColumnName("diagnosis_medical_department");
            entity.Property(e => e.DiagnosisName)
                .HasColumnType("character varying")
                .HasColumnName("diagnosis_name");

            entity.HasOne(d => d.DiagnosisMedicalDepartmentNavigation).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.DiagnosisMedicalDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("diagnosis_medical_departments_fk");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => new { e.HistoryDate, e.HistoryTime }).HasName("history_pk");

            entity.ToTable("history");

            entity.Property(e => e.HistoryDate).HasColumnName("history_date");
            entity.Property(e => e.HistoryTime).HasColumnName("history_time");
            entity.Property(e => e.HistoryClient).HasColumnName("history_client");
            entity.Property(e => e.HistoryDiagnosis).HasColumnName("history_diagnosis");
            entity.Property(e => e.HistoryStatus)
                .HasDefaultValue(false)
                .HasColumnName("history_status");

            entity.HasOne(d => d.HistoryClientNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.HistoryClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_clients_fk");

            entity.HasOne(d => d.HistoryDiagnosisNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.HistoryDiagnosis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_diagnosis_fk");
        });

        modelBuilder.Entity<MedicalDepartment>(entity =>
        {
            entity.HasKey(e => e.MedicalDepartmentId).HasName("medical_departments_pk");

            entity.ToTable("medical_departments");

            entity.Property(e => e.MedicalDepartmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("medical_department_id");
            entity.Property(e => e.MedicalDepartmentName)
                .HasColumnType("character varying")
                .HasColumnName("medical_department_name");
            entity.Property(e => e.MedicalDepartmentNumberOfSeats).HasColumnName("medical_department_number_of_seats");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.UserLogin)
                .HasColumnType("character varying")
                .HasColumnName("user_login");
            entity.Property(e => e.UserName)
                .HasColumnType("character varying")
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasColumnType("character varying")
                .HasColumnName("user_password");
            entity.Property(e => e.UserType).HasColumnName("user_type");

            entity.HasOne(d => d.UserTypeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_users_type_fk");
        });

        modelBuilder.Entity<UsersType>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("users_type_pk");

            entity.ToTable("users_type");

            entity.Property(e => e.UserTypeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_type_id");
            entity.Property(e => e.UserTypeName)
                .HasColumnType("character varying")
                .HasColumnName("user_type_name");
            entity.Property(e => e.UsersTypeMedicalDepartments).HasColumnName("users_type_medical_departments");

            entity.HasOne(d => d.UsersTypeMedicalDepartmentsNavigation).WithMany(p => p.UsersTypes)
                .HasForeignKey(d => d.UsersTypeMedicalDepartments)
                .HasConstraintName("users_type_medical_departments_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
