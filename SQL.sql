-- Создание последовательностей
CREATE SEQUENCE IF NOT EXISTS clients_client_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 9223372036854775807
	START 1
	CACHE 1
	NO CYCLE;

CREATE SEQUENCE IF NOT EXISTS diagnosis_diagnosis_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 9223372036854775807
	START 1
	CACHE 1
	NO CYCLE;

CREATE SEQUENCE IF NOT EXISTS medical_departments_medical_department_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;

CREATE SEQUENCE IF NOT EXISTS users_type_user_type_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1
	CACHE 1
	NO CYCLE;

CREATE SEQUENCE IF NOT EXISTS users_user_id_seq
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 9223372036854775807
	START 1
	CACHE 1
	NO CYCLE;

-- Создание таблиц
CREATE TABLE IF NOT EXISTS medical_departments (
	medical_department_id int4 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	medical_department_name varchar NOT NULL,
	medical_department_number_of_seats int4 NULL,
	CONSTRAINT medical_departments_pk PRIMARY KEY (medical_department_id)
);

CREATE TABLE IF NOT EXISTS clients (
	client_id int8 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START 1 CACHE 1 NO CYCLE) NOT NULL,
	client_name varchar NOT NULL,
	client_surname varchar NOT NULL,
	client_patronymic varchar NULL,
	client_snils varchar NULL,
	client_passport varchar NULL,
	client_birthday date NOT NULL,
	client_polis varchar NULL,
	client_is_man bool DEFAULT true NULL,
	CONSTRAINT clients_pk PRIMARY KEY (client_id)
);

CREATE TABLE IF NOT EXISTS diagnosis (
	diagnosis_id int8 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START 1 CACHE 1 NO CYCLE) NOT NULL,
	diagnosis_name varchar NOT NULL,
	diagnosis_medical_department int4 NOT NULL,
	CONSTRAINT diagnosis_pk PRIMARY KEY (diagnosis_id),
	CONSTRAINT diagnosis_medical_departments_fk FOREIGN KEY (diagnosis_medical_department) REFERENCES medical_departments(medical_department_id)
);

CREATE TABLE IF NOT EXISTS users_type (
	user_type_id int4 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	user_type_name varchar NOT NULL,
	users_type_medical_departments int4 NULL,
	CONSTRAINT users_type_pk PRIMARY KEY (user_type_id),
	CONSTRAINT users_type_medical_departments_fk FOREIGN KEY (users_type_medical_departments) REFERENCES medical_departments(medical_department_id)
);

CREATE TABLE IF NOT EXISTS users (
	user_id int8 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START 1 CACHE 1 NO CYCLE) NOT NULL,
	user_name varchar NOT NULL,
	user_type int4 NOT NULL,
	user_login varchar NOT NULL,
	user_password varchar NOT NULL,
	CONSTRAINT users_pk PRIMARY KEY (user_id),
	CONSTRAINT users_users_type_fk FOREIGN KEY (user_type) REFERENCES users_type(user_type_id)
);

CREATE TABLE IF NOT EXISTS history (
	history_date date NOT NULL,
	history_time time NOT NULL,
	history_client int8 NOT NULL,
	history_diagnosis int8 NOT NULL,
	history_status bool DEFAULT false NOT NULL,
	CONSTRAINT history_pk PRIMARY KEY (history_date, history_time),
	CONSTRAINT history_clients_fk FOREIGN KEY (history_client) REFERENCES clients(client_id),
	CONSTRAINT history_diagnosis_fk FOREIGN KEY (history_diagnosis) REFERENCES diagnosis(diagnosis_id)
);

-- Вставка данных в medical_departments
INSERT INTO medical_departments (medical_department_name, medical_department_number_of_seats) VALUES
	 ('Терапевтическое отделение', 50),
	 ('Хирургическое отделение', 30),
	 ('Кардиологическое отделение', 25),
	 ('Неврологическое отделение', 40),
	 ('Офтальмологическое отделение', 20),
	 ('Отделение травматологии', 35),
	 ('Гинекологическое отделение', 28),
	 ('Педиатрическое отделение', 45),
	 ('Отделение реанимации', 15),
	 ('Инфекционное отделение', 22);

-- Вставка данных в diagnosis 
INSERT INTO diagnosis (diagnosis_name, diagnosis_medical_department) VALUES
	 ('Гипертоническая болезнь', 3),        -- Кардиологическое
	 ('Острый аппендицит', 2),              -- Хирургическое
	 ('Сахарный диабет 2 типа', 1),         -- Терапевтическое
	 ('Остеохондроз позвоночника', 4),      -- Неврологическое
	 ('Катаракта', 5),                      -- Офтальмологическое
	 ('Перелом лучевой кости', 6),          -- Травматологии
	 ('Бронхиальная астма', 1),             -- Терапевтическое
	 ('Ишемическая болезнь сердца', 3),     -- Кардиологическое
	 ('Гастрит', 1),                        -- Терапевтическое
	 ('Миопия средней степени', 5);         -- Офтальмологическое

INSERT INTO diagnosis (diagnosis_name, diagnosis_medical_department) VALUES
	 ('Острый инфаркт миокарда', 3),        -- Кардиологическое
	 ('Воспаление легких', 1),              -- Терапевтическое
	 ('Черепно-мозговая травма', 6),        -- Травматологии
	 ('Варикозная болезнь', 2),             -- Хирургическое
	 ('Артрит коленного сустава', 6),       -- Травматологии
	 ('Гнойное заболевание кожи', 2);       -- Хирургическое

-- Вставка данных в clients 
INSERT INTO clients (client_name, client_surname, client_patronymic, client_snils, client_passport, client_birthday, client_polis, client_is_man) VALUES
	 ('Иван', 'Петров', 'Сергеевич', '123-456-789 01', '4510 123456', '1985-03-15', '745632189000', true),
	 ('Мария', 'Иванова', 'Александровна', '234-567-890 12', '4510 234567', '1990-07-22', '845632189001', false),
	 ('Алексей', 'Сидоров', 'Викторович', '345-678-901 23', '4510 345678', '1978-11-30', '945632189002', true),
	 ('Елена', 'Кузнецова', 'Олеговна', '456-789-012 34', '4510 456789', '1982-05-18', '145632189003', false),
	 ('Дмитрий', 'Васильев', 'Игоревич', '567-890-123 45', '4510 567890', '1995-12-10', '245632189004', true),
	 ('Ольга', 'Попова', 'Владимировна', '678-901-234 56', '4510 678901', '1988-09-05', '345632189005', false),
	 ('Сергей', 'Морозов', 'Анатольевич', '789-012-345 67', '4510 789012', '1975-02-28', '445632189006', true),
	 ('Анна', 'Новикова', 'Сергеевна', '890-123-456 78', '4510 890123', '1992-08-14', '545632189007', false),
	 ('Павел', 'Федоров', 'Дмитриевич', '901-234-567 89', '4510 901234', '1980-04-25', '645632189008', true),
	 ('Наталья', 'Волкова', 'Павловна', '012-345-678 90', '4510 012345', '1987-06-12', '745632189009', false);

INSERT INTO clients (client_name, client_surname, client_patronymic, client_snils, client_passport, client_birthday, client_polis, client_is_man) VALUES
	 ('Андрей', 'Алексеев', 'Николаевич', '112-345-678 91', '4510 112345', '1972-10-08', '845632189010', true),
	 ('Ирина', 'Лебедева', 'Андреевна', '212-345-678 92', '4510 212345', '1998-01-20', '945632189011', false),
	 ('Михаил', 'Семенов', 'Петрович', '312-345-678 93', '4510 312345', '1983-03-03', '145632189012', true),
	 ('Виктор', 'Петров', 'Владимирович', '222-222-222 22', '4510 111112', '2022-01-01', '111111111111', false),  
	 ('Виктор', 'Нахимов', 'Петров', '111-111-111 11', '4510 111113', '2001-01-01', '333333333333', true),      
	 ('Иван', 'Иванов', 'Иванович', '123-476-354 86', '4510 879856', '2000-01-01', '222222222222', false);       

-- Вставка данных в users_type
INSERT INTO users_type (user_type_name, users_type_medical_departments) VALUES
	 ('Администратор', NULL),
	 ('Главный врач', NULL),
	 ('Заведующий хирургическим отделением', 2),
	 ('Заведующий терапевтическим отделением', 1),
	 ('Заведующий кардиологическим отделением', 3),
	 ('Заведующий неврологическим отделением', 4),
	 ('Заведующий офтальмологическим отделением', 5),
	 ('Заведующий отделением травматологии', 6),
	 ('Заведующий гинекологическим отделением', 7),
	 ('Заведующий педиатрическим отделением', 8);

INSERT INTO users_type (user_type_name, users_type_medical_departments) VALUES
	 ('Заведующий отделением реанимации', 9),
	 ('Заведующий инфекционным отделением', 10),
	 ('Врач-терапевт', 1),
	 ('Врач-хирург', 2),
	 ('Врач-кардиолог', 3),
	 ('Врач-невролог', 4),
	 ('Врач-офтальмолог', 5),
	 ('Врач-травматолог', 6),
	 ('Врач-гинеколог', 7),
	 ('Врач-педиатр', 8);

INSERT INTO users_type (user_type_name, users_type_medical_departments) VALUES
	 ('Врач-реаниматолог', 9),              
	 ('Врач-инфекционист', 10),             
	 ('Медсестра/медбрат терапевтического отделения', 1),
	 ('Медсестра/медбрат хирургического отделения', 2),
	 ('Медсестра/медбрат кардиологического отделения', 3),
	 ('Медсестра/медбрат неврологического отделения', 4),
	 ('Медсестра/медбрат офтальмологического отделения', 5),
	 ('Медсестра/медбрат отделения травматологии', 6),
	 ('Медсестра/медбрат гинекологического отделения', 7),
	 ('Медсестра/медбрат педиатрического отделения', 8);

INSERT INTO users_type (user_type_name, users_type_medical_departments) VALUES
	 ('Медсестра/медбрат отделения реанимации', 9),   
	 ('Медсестра/медбрат инфекционного отделения', 10),
	 ('Старшая медсестра', NULL),
	 ('Бухгалтер', NULL),
	 ('Регистратор', NULL),
	 ('Лаборант', NULL),
	 ('Рентгенолог', NULL),
	 ('Фармацевт', NULL),
	 ('Системный администратор', NULL),
	 ('Оператор ЭВМ', NULL);

-- Вставка данных в history
INSERT INTO history (history_date, history_time, history_client, history_diagnosis, history_status) VALUES
	 ('2024-01-15', '09:30:00', 1, 3, true),
	 ('2024-01-15', '10:15:00', 2, 7, true),
	 ('2024-01-16', '11:00:00', 3, 2, false),
	 ('2024-01-16', '14:20:00', 4, 9, true),
	 ('2024-01-17', '08:45:00', 5, 6, true),
	 ('2024-01-17', '12:30:00', 6, 1, false),
	 ('2024-01-18', '10:00:00', 7, 8, true),
	 ('2024-01-18', '15:45:00', 8, 5, true),
	 ('2024-01-19', '09:15:00', 9, 4, false),
	 ('2024-01-19', '13:20:00', 10, 10, true);

INSERT INTO history (history_date, history_time, history_client, history_diagnosis, history_status) VALUES
	 ('2024-01-20', '11:30:00', 11, 11, true),
	 ('2024-01-20', '16:00:00', 12, 7, false),
	 ('2024-01-21', '08:30:00', 13, 3, true),
	 ('2024-01-21', '14:10:00', 14, 12, true),
	 ('2024-01-22', '10:45:00', 15, 13, false),
	 ('2024-01-23', '09:00:00', 2, 14, true),
	 ('2024-01-23', '13:40:00', 3, 15, false),
	 ('2024-01-24', '11:20:00', 4, 8, true),
	 ('2024-01-24', '15:30:00', 5, 9, true),
	 ('2001-01-01', '01:01:01', 1, 1, true);

INSERT INTO history (history_date, history_time, history_client, history_diagnosis, history_status) VALUES
	 ('2024-01-22', '12:15:00', 1, 1, false),
	 ('2024-01-25', '23:41:20', 4, 10, true);