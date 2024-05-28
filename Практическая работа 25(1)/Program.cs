using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Практическая_работа_25_1_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            c:
            Console.Write("Введите номер задания:");
            int n = int.Parse(Console.ReadLine());
            switch(n) 
            {
                case 1:
                    {
                        Console.WriteLine("Базовый уровень.");
                        string фаил = "books.txt";
                        string результатФайла = "filtered_books.txt";

                        // Создаем текстовый файл с произвольной информацией о книгах
                        СозданиеФайла(фаил);

                        // Читаем и обрабатываем данные из файла
                        ПроцессФайла(фаил, результатФайла);
                        // Выводим содержимое исходного файла на консоль
                        Console.WriteLine("Содержимое исходного файла:");
                        Вывод(фаил);

                        // Выводим содержимое файла с результатами на консоль
                        Console.WriteLine("\nСодержимое файла с результатами:");
                        Вывод(результатФайла);

                        Console.WriteLine("Обработка завершена. Результаты сохранены в " + результатФайла);
                        Console.ReadKey();
                    }
                    goto c;
                case 2:
                    {
                        Console.WriteLine("Средний уровень.");
                        string входнойФайл = "dates.txt";
                        string выходнойФайл = "springdates.txt";

                        // Создаем файл с различными датами
                        СозданиеДатаФайл(входнойФайл);

                        // Читаем и выводим содержимое исходного файла
                        Console.WriteLine("Содержимое исходного файла:");
                        List<DateTime> даты = ЧтениеДатИзФайла(входнойФайл);
                        foreach (var дата in даты)
                        {
                            Console.WriteLine(дата.ToString("dd.MM.yyyy"));
                        }

                        // Находим весенние даты
                        List<DateTime> весенниеДаты = НайтиВесенДаты(даты);

                        // Сохраняем весенние даты в новый файл
                        SaveDatesToFile(выходнойФайл, весенниеДаты);

                        // Читаем и выводим содержимое нового файла
                        Console.WriteLine("\nСодержимое файла с весенними датами:");
                        List<DateTime> сохраненоВеснаДаты = ЧтениеДатИзФайла(выходнойФайл);
                        foreach (var дата in сохраненоВеснаДаты)
                        {
                            Console.WriteLine(дата.ToString("dd.MM.yyyy"));
                        }
                        Console.ReadKey();
                    }
                    goto c;
                case 3:
                    {
                        //
                        Console.WriteLine("Высокий уровень.");
                        string первыйФайл = "firstFile.txt";
                        string второйФайл = "secondFile.txt";

                        List<int[,]> матрицыИзФайл1 = ЧтениеМатрицИзФайла(первыйФайл);
                        List<int[,]> матрицыИзФайл2 = ЧтениеМатрицИзФайла(второйФайл);

                        foreach (var матрица in матрицыИзФайл1)
                        {
                            if (ВычислитьОпределитель(матрица) == 5)
                            {
                                матрицыИзФайл2.Add(матрица);
                            }
                        }

                        ЗаписьМатриц(второйФайл, матрицыИзФайл2);

                        Console.WriteLine("Содержимое первого файла:");
                        ВыводМатриц(матрицыИзФайл1);

                        Console.WriteLine("Содержимое второго файла:");
                        ВыводМатриц(матрицыИзФайл2);
                        Console.ReadKey();
                    }
                    goto c;
                default:
                    {
                        Console.WriteLine("Неправильный номер задания.");
                    }
                    goto c;
            }
        }


        //1
        static void СозданиеФайла(string фаил)
        {
            string[] books = {
            "Название1;Кузнецов;Иван;2020;Издательство1;500;800;300",
            "Название2;Петров;Алексей;2018;Издательство2;500;800;300",
            "Название3;Ковалев;Сергей;2019;Издательство3;500;800;300",
            "Название4;Сидоров;Николай;2021;Издательство4;500;800;300"
        };

            File.WriteAllLines(фаил, books);
        }
        static void ПроцессФайла(string путьКВходномуФайлу, string путьКВыходномуФайлу)
        {
            var линии = File.ReadAllLines(путьКВходномуФайлу);
            var фильтрКниги = линии.Where(линия => линия.Split(';')[1].StartsWith("К")).ToArray();

            File.WriteAllLines(путьКВыходномуФайлу, фильтрКниги);
        }
        static void Вывод(string фаил)
        {
            var линии = File.ReadAllLines(фаил);
            foreach (var линия in линии)
            {
                Console.WriteLine(линия);
            }
        }





        //2

        static void СозданиеДатаФайл(string имяФайла)
        {
            var даты = new List<string>
        {
            "01.01.2023", "15.03.2023", "10.05.2023", "21.06.2023", "30.04.2023",
            "25.12.2023", "17.03.2023", "08.05.2023", "29.02.2024"
        };

            File.WriteAllLines(имяФайла, даты);
        }

        static List<DateTime> ЧтениеДатИзФайла(string имяФайла)
        {
            var даты = new List<DateTime>();
            var датаЛинии = File.ReadAllLines(имяФайла);

            foreach (var line in датаЛинии)
            {
                if (DateTime.TryParseExact(line, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime дата))
                {
                    даты.Add(дата);
                }
            }

            return даты;
        }

        static List<DateTime> НайтиВесенДаты(List<DateTime> даты)
        {
            var весенниеДаты = new List<DateTime>();

            foreach (var дата in даты)
            {
                if (дата.Month >= 3 && дата.Month <= 5)
                {
                    весенниеДаты.Add(дата);
                }
            }

            return весенниеДаты;
        }

        static void SaveDatesToFile(string имяФайла, List<DateTime> даты)
        {
            var датаВесенняя = new List<string>();

            foreach (var дата in даты)
            {
                датаВесенняя.Add(дата.ToString("dd.MM.yyyy"));
            }

            File.WriteAllLines(имяФайла, датаВесенняя);
        }






        //3
        static List<int[,]> ЧтениеМатрицИзФайла(string путьКФайлу)
        {
            var матрицы = new List<int[,]>();
            var линии = File.ReadAllLines(путьКФайлу);
            int считМатр = int.Parse(линии[0]);

            int индексСтроки = 1;
            for (int k = 0; k < считМатр; k++)
            {
                var матрица = new int[2, 2];
                for (int i = 0; i < 2; i++)
                {
                    var значение = линии[индексСтроки++].Split(' ').Select(int.Parse).ToArray();
                    for (int j = 0; j < 2; j++)
                    {
                        матрица[i, j] = значение[j];
                    }
                }
                матрицы.Add(матрица);
            }

            return матрицы;
        }

        static void ЗаписьМатриц(string путьКФайлу, List<int[,]> матрицы)
        {
            using (StreamWriter писать = new StreamWriter(путьКФайлу))
            {
                писать.WriteLine(матрицы.Count);

                foreach (var матрица in матрицы)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var ряд = new int[2];
                        for (int j = 0; j < 2; j++)
                        {
                            ряд[j] = матрица[i, j];
                        }
                        писать.WriteLine(string.Join(" ", ряд));
                    }
                }
            }
        }

        static void ВыводМатриц(List<int[,]> матрицы)
        {
            foreach (var матрица in матрицы)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Console.Write(матрица[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        static int ВычислитьОпределитель(int[,] матрица)
        {
            return матрица[0, 0] * матрица[1, 1] - матрица[0, 1] * матрица[1, 0];
        }
    }
}
