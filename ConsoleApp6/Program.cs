using System;
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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Changes { get; set; }
        public string Looking { get; set; }
        public string Saving { get; set; }
        internal class Program
        {
            static void Main(string[] args)
            {
                InterfaceStudent();
            }
            private static void InterfaceStudent()
            {
                while (true)
                {
                    Console.WriteLine("Вітаємо! Ви увійшли до системи як студент.");
                    Console.WriteLine("Введіть ваше ім'я.");
                    Console.WriteLine("Введіть ваше прізвище.");
                    Console.WriteLine("Введіть вашу групу.");
                    Console.WriteLine("Оберіть, що ви хочете зробити:");
                    Console.WriteLine("1. Переглянути розклад занять");
                    Console.WriteLine("2. Додати заняття");
                    Console.WriteLine("3. Відмінити заняття");
                    Console.WriteLine("4. Редагувати заняття");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            ShowSchledule();
                            break;
                        case 2:
                            AddLesson();
                            break;
                        case 3:
                            RemoveLesson();
                            break;
                        case 4:
                            Editing();
                            break;
                        default:
                            Console.WriteLine("Некоректне введення. Спробуйте ще.");
                            Console.Clear();
                            break;
                    }
                }
            }
        }
        private static void ShowSchledule()
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[,] schledule = new string[5, 6];
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
            int[] maxLenghts = new int[cols];
            int changelesson = 0;
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
            for (int i = 0; i < schledule.GetLength(0); i++)
            {
                for (int j = 0; j < schledule.GetLength(1); j++)
                {
                    Console.Write(schledule[i, j].PadRight(maxLenghts[j] + 1));
                }
                Console.WriteLine("\n");
            }

        }
        private static void AddLesson()
        {

            Console.Clear();
        }
        private static void RemoveLesson()
        {
            Console.Clear();
        }
        private static void Editing()
        {
            Console.Clear();
        }
    }
}
