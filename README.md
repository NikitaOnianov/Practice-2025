# Практика-2025
port: 5432
user: userdb
password: 1234
ip: 176.108.250.10
database: mydatabase

# Команда для создания связи с БД
dotnet ef dbcontext scaffold "Host=176.108.250.10;Port=5432;Database=mydatabase;Username=userdb;Password=1234" Npgsql.EntityFrameworkCore.PostgreSQL -o Models

