﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string inputFile = "input.txt";
        string outputFile = "output.txt";

        // Читаємо вміст вхідного файлу
        string[] lines = File.ReadAllLines(inputFile);

        // Паттерн для визначення дати у форматі дд.мм.рррр
        string pattern = @"\b(\d{1,2})\.(\d{1,2})\.(19\d{2}|20\d{2})\b";

        // Список для зберігання дат та їх кількості
        Dictionary<string, int> dates = new Dictionary<string, int>();

        // Перебираємо рядки у файлі
        foreach (string line in lines)
        {
            // Знаходимо всі дати у рядку
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                string date = match.Value;
                // Перевіряємо, чи дата відповідає формату
                if (IsValidDate(date))
                {
                    // Додаємо дату до словника та збільшуємо лічильник
                    if (dates.ContainsKey(date))
                    {
                        dates[date]++;
                    }
                    else
                    {
                        dates[date] = 1;
                    }
                }
            }
        }

        // Записуємо результат у вихідний файл
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            foreach (KeyValuePair<string, int> entry in dates)
            {
                writer.WriteLine($"{entry.Key}: {entry.Value} occurrences");
            }
        }
    }

    // Перевіряє чи дата відповідає формату дд.мм.рррр
    static bool IsValidDate(string date)
    {
        string[] parts = date.Split('.');
        if (parts.Length != 3)
            return false;

        int day, month, year;
        if (!int.TryParse(parts[0], out day) ||
            !int.TryParse(parts[1], out month) ||
            !int.TryParse(parts[2], out year))
            return false;

        if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1900 || year > 2099)
            return false;

        return true;
    }
}
