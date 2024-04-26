using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlTypes;
using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;
using System.Reflection;
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
        public string Time { get; set; } = DateTime.Now.Hour.ToString();
        public string Locations { get; set; } = "default_value";

        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            InterfaceStudent();
        }
        private static void InterfaceStudent()
        {
            try
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
                    Console.Clear();
                    Console.WriteLine($"Вітаю,{surname}  {name}");
                    Console.WriteLine("Оберіть, що ви хочете зробити:");
                    Console.WriteLine("1. Переглянути розклад занять");
                    Console.WriteLine("2. Додати, відмінити чи редагувати заняття");
                    Console.WriteLine("3. Зберегти відредагований розклад");
                    Console.WriteLine("4. Вийти. ");
                    Console.WriteLine("5. Знайти предмет по назві."); //Зберегти відредагований розклад pdf


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
                            Environment.Exit(0);
                            break;
                        case 4:
                            SaveBytes(ref schledule, ref maxLenghts);
                            Environment.Exit(0);
                            break;
                        case 5:
                            //       ExportToPdf(ref schledule);
                            Founded(ref schledule);
                            break;
                        default:
                            Console.WriteLine("Некоректне введення. Спробуйте ще.");
                            Console.Clear();
                            break;
                    }

                }
            }
            catch
            {
                Console.WriteLine("Ви ввели некоректне значення.");
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
                            Console.WriteLine("Для того щоб додати заняття, видалити або редагувати, виберіть будь ласка # пари (1-4).");
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
            SaveBytes(ref schledule, ref maxLenghts);
        }
        private static void SaveBytes(ref string[,] schledule, ref int[] maxLenghts)
        {
            Console.Clear();
            {
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
                                Console.WriteLine(schledule[i, j]);

                            }
                            writer.WriteLine("\n");
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

        //   private static void ExportToPdf(ref string[,] schledule)
        //   {
        //       Console.Clear();
        //       Console.Write("Введіть назву для PDF-файлу (без розширення): ");
        //       string pdfFileName = Console.ReadLine();
        //       pdfFileName = Path.ChangeExtension(pdfFileName, ".pdf");
        //       string filePath = "C:\\Users\\ninad\\OneDrive\\Рабочий стол\\" + pdfFileName; ;
        //
        //       PdfDocument pdf = new PdfDocument();
        //       PdfPage page = pdf.AddPage();
        //       page.Orientation = PdfSharp.PageOrientation.Landscape;
        //       XGraphics gfx = XGraphics.FromPdfPage(page);
        //       XFont font = new XFont("Calibri", 12, XFontStyleEx.Italic); // mistake with "Calibri"
        //       
        //       int xPosition = 50;
        //       try
        //       {
        //           for (int i = 0; i < schledule.GetLength(0); i++)
        //           {
        //               int yPosition = 20;
        //               for (int j = 1; j < schledule.GetLength(1); j++)
        //               {
        //                   if (schledule[i, j] != null)
        //                   {
        //                       gfx.DrawString(schledule[i, j], font, XBrushes.Black, xPosition, yPosition);
        //                       yPosition += 20;
        //                   }
        //               }
        //
        //               xPosition += 100;
        //           }
        //
        //           pdf.Save(filePath);
        //           Console.WriteLine($"Розклад успішно експортовано у файл {pdfFileName} у шляху {filePath}");
        //
        //
        //       }
        //       catch (Exception ex)
        //       {
        //           Console.WriteLine("Помилка при збереженні: " + ex.Message);
        //       }
        //       finally
        //       {
        //           pdf.Close();
        //       }
        //   }
        private static void Founded(ref string[,] schledule)
        {
            try
            {
                Console.Clear();
                List<string> foundLessons = new List<string>();
                Console.WriteLine("Введіть будь ласка предмет для пошуку в розкладі.");
                string SearchLesson = Console.ReadLine();
                for (int i = 0; i < schledule.GetLength(0); i++)
                {
                    for (int j = 1; j < schledule.GetLength(1); j++)
                    {
                        if (schledule[i, j] != null && schledule[i, j].Contains(SearchLesson, StringComparison.OrdinalIgnoreCase))
                        {
                            foundLessons.Add($"{schledule[i, 0]}: {schledule[0, j]}");
                        }
                    }
                }
                if (foundLessons.Any())
                {
                    Console.WriteLine($"Результати пошуку для '{SearchLesson}':");
                    foreach (var lesson in foundLessons)
                    {
                        Console.WriteLine(lesson);
                    }
                }
                else
                {
                    Console.WriteLine($"Занять з назвою '{SearchLesson}' не знайдено.");
                }
            }
            catch
            {
                Console.WriteLine("Ви ввели некоректне значення.");
            }
        }
    }
}






