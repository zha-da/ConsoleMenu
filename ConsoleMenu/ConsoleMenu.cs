using System;
using System.Collections.Generic;

namespace ConsoleMenu
{
    public class ConsoleMenu
    {
        List<Point> points { get; set; }
        /// <summary>
        /// Создает новое консольное меню на основе готового списка пунктов меню
        /// </summary>
        /// <param name="Methods">Имя списка пунктов</param>
        public ConsoleMenu(List<Point> vs)
        {
            points = vs;
        }
        /// <summary>
        /// Создает пустое консольное меню
        /// </summary>
        public ConsoleMenu() { }
        /// <summary>
        /// Добавляет новый пункт в меню
        /// </summary>
        /// <param name="point"></param>
        public void AddPoint(Point point)
        {
            points.Add(point);
        }
        /// <summary>
        /// Запускает меню
        /// </summary>
        public void RunMenu()
        {
            string choice = "0";
            while (!choice.ToLower().Equals("exit"))
            {
                Console.Clear();
                foreach (Point p in points)
                {
                    Console.WriteLine(p.Name);
                }
                choice = Console.ReadLine();
                int num;
                if (int.TryParse(choice, out num) && num <= points.Count)
                {
                    points[num - 1].ExecuteMethod(null);
                }
                Console.ReadKey();
            }
        }
    }
}
