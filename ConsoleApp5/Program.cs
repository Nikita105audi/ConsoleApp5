using RunTracker;
using System.Globalization;

class Program
{
    static RunLog log = new RunLog();

    static void Main()
    {
        InitializeDemoData();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Трекер пробежек ===");
            Console.WriteLine("1. Показать все записи");
            Console.WriteLine("2. Добавить пробежку");
            Console.WriteLine("3. Найти по названию");
            Console.WriteLine("4. Средняя дистанция");
            Console.WriteLine("5. Сложить два лога (демо)");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        PrintTable();
                        break;
                    case "2":
                        AddEntry();
                        break;
                    case "3":
                        SearchByName();
                        break;
                    case "4":
                        PrintAverageDistance();
                        break;
                    case "5":
                        DemoOperatorPlus();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный пункт.");
                        break;
                }
            }
            catch (InvalidRunDataException ex)
            {
                Console.WriteLine($"Ошибка данных: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите Enter...");
            Console.ReadLine();
        }
    }

    static void InitializeDemoData()
    {
        log.Add(new RunEntry("Парк Горького", 5.5m, new DateTime(2025, 10, 20), false));
        log.Add(new RunEntry("Набережная", 8.2m, new DateTime(2025, 10, 22), true));
        log.Add(new RunEntry("Лосиный Остров", 12.0m, new DateTime(2025, 10, 25), true));
        log.Add(new RunEntry("ВДНХ", 6.0m, new DateTime(2025, 10, 27), false));
        log.Add(new RunEntry("Коломенское", 10.5m, new DateTime(2025, 10, 29), true));
        log.Add(new RunEntry("Бульварное кольцо", 7.1m, new DateTime(2025, 10, 30), false));
        log.Add(new RunEntry("Парк Горького", 5.8m, new DateTime(2025, 11, 01), false));
        log.Add(new RunEntry("Строгино", 9.3m, new DateTime(2025, 11, 03), true));
    }

    static void PrintTable()
    {
        Console.WriteLine("\nВсе пробежки:");
        string header = string.Format(CultureInfo.InvariantCulture, "{0,-4} | {1,-20} | {2,-10} | {3,-12} | {4}",
            "№", "Маршрут", "Дистанция", "Дата", "Сложность");
        Console.WriteLine(header);
        Console.WriteLine(new string('-', header.Length));

        for (int i = 0; i < log.Count; i++)
        {
            var e = log[i];
            string line = string.Format(CultureInfo.InvariantCulture, "{0,-4} | {1,-20} | {2,-10} | {3,-12:dd.MM.yyyy} | {4}",
                i + 1, e.RouteName, e.DistanceKm, e.Date, e.WasHard ? "Да" : "Нет");
            Console.WriteLine(line);
        }
    }

    static void AddEntry()
    {
        Console.Write("Название маршрута: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Дистанция (км): ");
        if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal dist))
            throw new InvalidRunDataException("Дистанция введена неверно.");

        Console.Write("Дата (дд.мм.гггг): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            throw new InvalidRunDataException("Дата введена неверно.");

        Console.Write("Было тяжело? (д/н): ");
        bool hard = (Console.ReadLine() ?? "н").ToLower().StartsWith("д");

        log.Add(new RunEntry(name, dist, date, hard));
        Console.WriteLine("Запись добавлена.");
    }

    static void SearchByName()
    {
        Console.Write("Часть названия: ");
        string needle = Console.ReadLine()?.ToLower() ?? "";
        bool found = false;

        for (int i = 0; i < log.Count; i++)
        {
            if (log[i].RouteName.ToLower().Contains(needle))
            {
                Console.WriteLine(log[i]);
                found = true;
            }
        }
        if (!found) Console.WriteLine("Ничего не найдено.");
    }

    static void PrintAverageDistance()
    {
        if (log.Count == 0) { Console.WriteLine("Лог пуст."); return; }

        decimal sum = 0;
        for (int i = 0; i < log.Count; i++) sum += log[i].DistanceKm;

        double avg = (double)sum / log.Count;
        Console.WriteLine($"Средняя дистанция: {avg:F2} км");
    }

    static void DemoOperatorPlus()
    {
        RunLog log2 = new RunLog();
        log2.Add(new RunEntry("Сокольники", 4.0m, DateTime.Today, false));

        RunLog combined = log + log2;
        Console.WriteLine($"\nСложили два лога. Итог записей: {combined.Count}");
        PrintLogInternal(combined);
    }

    static void PrintLogInternal(RunLog target)
    {
        for (int i = 0; i < target.Count; i++)
            Console.WriteLine(target[i]);
    }
}