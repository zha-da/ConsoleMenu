using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class CMenu
    {
        List<Point> points { get; set; }
        public CMenu(List<Point> vs)
        {
            points = vs;
        }
        public CMenu() => points = new List<Point>();
        public void AddPoint(Point point)
        {
            points.Add(point);
        }
        public void RunMenu()
        {
            try
            {
                if (points.Count == 0) throw new Exception("Ошибка: в меню нет пунктов");
                string choice = "0";
                while (!choice.ToLower().Equals("exit"))
                {
                    Console.Clear();
                    for (int i = 0; i < points.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} {points[i].Name}");
                    }
                    choice = Console.ReadLine();
                    int num;
                    if (int.TryParse(choice, out num) && num <= points.Count)
                    {
                        points[num - 1].ExecuteMethod();
                    }
                    else if (num > points.Count)
                    {
                        Console.WriteLine($"Пункта с подобным номером не существует. Введите число от 1 до {points.Count}");
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
