# Практика-2025
port: 5432
user: userdb
password: 1234
ip: 176.108.251.27
database: mydatabase

# Команда для создания связи с БД
dotnet ef dbcontext scaffold "Host=176.108.251.27;Port=5432;Database=mydatabase;Username=userdb;Password=1234" Npgsql.EntityFrameworkCore.PostgreSQL -o Models

### Создание базы данных на виртуальной машине

# Обновление пакетов
sudo apt update
sudo apt upgrade -y

# Установка PostgreSQL
sudo apt install postgresql postgresql-contrib -y

# Переключение на пользователя postgres
sudo -u postgres psql

#############################################################
-- Создание пользователя
CREATE USER userdb WITH PASSWORD '1234';

-- Создание базы данных
CREATE DATABASE mydatabase OWNER userdb;

-- Предоставление прав
GRANT ALL PRIVILEGES ON DATABASE mydatabase TO userdb;

-- Выход из psql
\q
###############################################################

# Редактирование основного конфигурационного файла
sudo nano /etc/postgresql/*/main/postgresql.conf

######################################
# Раскомментируйте и измените:
listen_addresses = '*'          # разрешаем подключения со всех IP
######################################

# Редактирование файла аутентификации
sudo nano /etc/postgresql/*/main/pg_hba.conf

######################################
# Разрешить подключения с любого IP
host    all             all             0.0.0.0/0               md5
######################################

# Разрешение порта PostgreSQL (по умолчанию 5432)
sudo ufw allow 5432/tcp

# Включение фаервола (если еще не включен)
sudo ufw enable

# Проверка правил
sudo ufw status

# Перезапуск службы для применения изменений
sudo systemctl restart postgresql

# Проверка статуса
sudo systemctl status postgresql

# Проверка, слушает ли PostgreSQL на нужном порте
sudo netstat -tlnp | grep 5432

#####################################
psql -h localhost -U userdb -d mydatabase