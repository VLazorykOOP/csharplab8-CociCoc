using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Exercise1();
        Exercise2();
        Exercise3();
    }

    static void Exercise1()
    {
        string inputFile = "input1.txt";
        string outputFile = "output_exercise1.txt";

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

    static void Exercise2()
    {
        string inputFile = "input2.txt";
        string outputFile = "output_exercise2.txt";

        // Читаємо вміст вхідного файлу
        string[] lines = File.ReadAllLines(inputFile);

        // Паттерни для визначення слів, які треба видалити та замінити
        string deletePattern = @"\b\w*(re|nd|less)\b";
        string replacePattern = @"\bto\w*\b";

        // Змінна для зберігання відредагованого тексту
        string editedText = "";

        // Перебираємо рядки у файлі
        foreach (string line in lines)
        {
            // Видаляємо слова з вказаними закінченнями та замінюємо слова з префіксом "to"
            string result = Regex.Replace(line, deletePattern, "");
            result = Regex.Replace(result, replacePattern, "at");

            // Додаємо редагований рядок до загального тексту
            editedText += result + Environment.NewLine;
        }

        // Записуємо результат у вихідний файл
        File.WriteAllText(outputFile, editedText);
    }
    static void Exercise3()
    {
        string inputFile1 = "input1_Exercise3.txt";
        string inputFile2 = "input2_Exercise3.txt";
        string outputFile = "output_exercise3.txt";

        // Читаємо вміст перших та других файлів
        string text1 = File.ReadAllText(inputFile1);
        string text2 = File.ReadAllText(inputFile2);

        Console.WriteLine("Text from inputFile1:");
        Console.WriteLine(text1);
        Console.WriteLine();

        Console.WriteLine("Text from inputFile2:");
        Console.WriteLine(text2);
        Console.WriteLine();

        // Розділяємо слова з перших та других текстів за всіма розділовими знаками
        string[] words1 = Regex.Split(text1, @"\W+");
        string[] words2 = Regex.Split(text2, @"\W+");

        Console.WriteLine("Words from inputFile1:");
        foreach (var word in words1)
        {
            Console.WriteLine(word);
        }
        Console.WriteLine();

        Console.WriteLine("Words from inputFile2:");
        foreach (var word in words2)
        {
            Console.WriteLine(word);
        }
        Console.WriteLine();

        // Вибираємо слова з першого тексту, які не входять у другий текст
        IEnumerable<string> uniqueWords = words1.Except(words2);

        Console.WriteLine("Unique words from inputFile1:");
        foreach (var word in uniqueWords)
        {
            Console.WriteLine(word);
        }
        Console.WriteLine();

        // Записуємо результат у вихідний файл
        File.WriteAllText(outputFile, string.Join(" ", uniqueWords));
    }


}
