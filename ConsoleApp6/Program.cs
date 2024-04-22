using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.Unicode;
//Розробка програми для керування студентським розкладом:
//  Створіть програму, яка дозволяє студентам переглядати розклад занять за тиждень.
//  Додайте функціонал для додавання нових занять, видалення та редагування існуючих.
//  Забезпечте можливість перегляду розкладу окремого студента та його зберігання у файлі.


namespace SchleduleApp
{
    public class SchleduleUniversity
    {
        private static int[] maxLenghts = new int[5];
        private static string[,] schledule = new string[5, 6];
        public Guid Id { get; set; }
        public string Day { get; set; } = DateTime.Now.Day.ToString();
        public string Time { get; set; }
        public string Locations { get; set; }

        internal class Program
        {
            static void Main(string[] args)
            {
                Console.InputEncoding = System.Text.Encoding.UTF8;
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                InterfaceStudent();
            }
            private static void InterfaceStudent()
            {
                Console.WriteLine("Вітаємо! Ви увійшли до системи як студент.");
                Console.WriteLine("Введіть ваше ім'я.");
                string name = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Введіть ваше прізвище.");
                string surname = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Введіть вашу групу.");
                string groups = Convert.ToString(Console.ReadLine());
                while (true)
                {
                    Console.WriteLine($"Вітаю,{surname}  {name}");
                    Console.WriteLine("Оберіть, що ви хочете зробити:");
                    Console.WriteLine("1. Переглянути розклад занять");
                    Console.WriteLine("2. Додати, відмінити чи редагуватизаняття");
                    Console.WriteLine("3. Зберегти відредагований розклад");

                    int choice = int.Parse(Console.ReadLine());
                    AgrigateSchledule();
                    switch (choice)
                    {
                        case 1:
                            AddShowMethod(ref schledule);
                            break;
                        case 2:
                            Editing(ref schledule, ref maxLenghts);
                            break;
                        case 3:
                            SaveBytes(ref schledule, ref maxLenghts);
                            break;
                        default:
                            Console.WriteLine("Некоректне введення. Спробуйте ще.");
                            Console.Clear();
                            break;
                    }
                }
            }

            private static void AgrigateSchledule()
            {
                schledule[0, 0] = " ";
                schledule[1, 0] = "1.";
                schledule[2, 0] = "2.";
                schledule[3, 0] = "3.";
                schledule[4, 0] = "4.";
                schledule[0, 1] = "понеділок";
                schledule[1, 1] = "Статистика";
                schledule[2, 1] = "Комп'ютерні технології";
                schledule[3, 1] = "Ділова українська мова";
                schledule[4, 1] = "Вища математика";
                schledule[0, 2] = "вівторок";
                schledule[1, 2] = "Вища математика";
                schledule[2, 2] = "Основи програмування";
                schledule[3, 2] = "Іноземна мова";
                schledule[4, 2] = "Аглоритмізація";
                schledule[0, 3] = "середа";
                schledule[1, 3] = "Математика";
                schledule[2, 3] = "Іноземна мова";
                schledule[3, 3] = "Основи програмування";
                schledule[4, 3] = "Комп'ютерні технології";
                schledule[0, 4] = "четвер";
                schledule[1, 4] = "Вища математика";
                schledule[2, 4] = "Ділова українська мова";
                schledule[3, 4] = "Вища математика";
                schledule[4, 4] = "";
                schledule[0, 5] = "п'ятниця";
                schledule[1, 5] = "Статистика";
                schledule[2, 5] = "Аглоритмізація";
                schledule[3, 5] = "Структури даних";
                schledule[4, 5] = "";
                int cols = schledule.GetLength(1);
                maxLenghts = new int[cols];
                for (int i = 0; i < schledule.GetLength(0); i++)
                {
                    for (int j = 0; j < schledule.GetLength(1); j++)
                    {
                        if (schledule[i, j].Length > maxLenghts[j])
                        {
                            maxLenghts[j] = schledule[i, j].Length;
                        }
                    }
                }
            }

            private static void AddShowMethod(ref string[,] schledule)
            {
                for (int i = 0; i < schledule.GetLength(0); i++)
                {
                    for (int j = 0; j < schledule.GetLength(1); j++)
                    {
                        Console.Write(schledule[i, j].PadRight(maxLenghts[j] + 1));
                    }
                    Console.WriteLine("\n");
                }
            }

            private static void Editing(ref string[,] schledule, ref int[] maxLenghts)
            {
                Console.Clear();
                bool continueEditing = true;

                while (continueEditing)
                    for (int i = 0; i < schledule.GetLength(0); i++)
                    {

                        Console.WriteLine("Для того щоб додати заняття, видалити або редагувати, виберіть будь ласка день тижня.");
                        string addindays = Console.ReadLine();
                        for (int j = 0; j < schledule.GetLength(1); j++)
                        {
                            if (addindays == schledule[0, j])
                            {
                                int indexj = j;
                                Console.WriteLine("Для того щоб додати заняття, видалити або редагувати,виберіть будь ласка # пари (1-4).");
                                int addinlessons = Convert.ToInt32(Console.ReadLine());
                                if (addinlessons <= 4)
                                {
                                    int indexi = addinlessons;
                                    Console.WriteLine("Введіть, будь ласка, зміни");
                                    string changes = schledule[indexi, indexj];
                                    changes = Convert.ToString(Console.ReadLine());
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"Заняття для пари {addinlessons} у день {addindays} змінено на '{changes}'");
                                    schledule[indexi, indexj] = changes;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("\n\t Для завершення редагування натисніть клавішу \"Space\".Для продовження будь яку клавішу. ");
                                    ConsoleKeyInfo key = Console.ReadKey();
                                    if (key.Key == ConsoleKey.Spacebar)
                                    {
                                        continueEditing = false;
                                        break; ;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        break;
                    }
                if (!continueEditing)
                {
                    for (int i = 0; i < schledule.GetLength(0); i++)
                    {
                        for (int j = 0; j < schledule.GetLength(1); j++)
                        {
                            Console.Write(schledule[i, j].PadRight(maxLenghts[j] + 1));
                        }
                        Console.WriteLine("\n");
                    }
                }
            }
            private static void SaveBytes(ref string[,] schledule, ref int[] maxLenghts)
            {
                {
                    Console.Clear();
                    string filePath = "C:\\Users\\ninad\\OneDrive\\Рабочий стол\\SchleduleUniversity.txt";

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            for (int i = 0; i < schledule.GetLength(0); i++)
                            {
                                for (int j = 0; j < schledule.GetLength(1); j++)
                                {
                                    writer.Write(schledule[i, j].PadRight(maxLenghts[j] + 1));
                                }
                                writer.WriteLine();
                            }
                        }

                        Console.WriteLine("Розклад успішно збережено у файл: " + filePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка при збереженні: " + ex.Message);
                    }
                }
            }
        }
    }

}

