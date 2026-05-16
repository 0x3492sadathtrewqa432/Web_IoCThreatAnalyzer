# IoC Threat Analyzer

## Описание проекта

IoC Threat Analyzer - web-приложение для индексирования и анализа web-страниц с целью обнаружения Indicators of Compromise (IOC).

Система анализирует HTML-контент страницы и обнаруживает:

* IP-адреса
* домены
* URL
* email-адреса

Результаты анализа сохраняются в базе данных SQLite.

---

# Используемые технологии

* .NET 9
* ASP.NET Core
* Blazor Server
* Entity Framework Core
* SQLite
* Razor Components
* Docker

---

# Возможности системы

* Анализ web-страниц
* Обнаружение IOC-индикаторов
* Отображение результатов анализа
* Сохранение истории сканирований
* Работа через web-интерфейс
* Локальный тестовый IOC endpoint

---

# Архитектура проекта

Проект состоит из следующих компонентов:

* Components - Razor UI компоненты
* Services - сервисы получения web-контента
* Parsers - IOC parser
* Models - модели данных
* Data - DbContext и работа с БД

---

# База данных

Используется Entity Framework Core (Code First).

Таблицы:

* ScanResult
* IocIndicator

Связь:

* Один ScanResult содержит множество IocIndicator.

---

# Запуск проекта

## Локальный запуск

```bash
dotnet restore
dotnet ef database update
dotnet run
```

После запуска приложение будет доступно по адресу:

```text
http://localhost:5206
```

---

# Тестовый IOC endpoint

Для тестирования используется локальная страница:

```text
http://localhost:5206/test-ioc
```

---

# Docker

## Сборка контейнера

```bash
docker build -t iocthreatanalyzer .
```

---

## Запуск контейнера

```bash
docker run -p 8080:8080 iocthreatanalyzer
```

---

# Docker Compose

```bash
docker-compose up --build
```

---



# Информация о проекте
Возможны ошибки и неточности, если вы наткнулись на этот проект случайно - прошу перепроверяйте все. Проект сделан в рамках студентческой курсовой работы =)

