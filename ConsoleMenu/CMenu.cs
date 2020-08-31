using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    /// <summary>
    /// Режимы управления консольным меню
    /// </summary>
    public enum MenuModes
    {
        /// <summary>
        /// <para>Режим управления кнопками на клавиатуре:</para>
        /// <para>Стрелки вверх/вниз - выбор пункта,</para>
        /// <para>Клавиша Enter - выполнение пункта,</para>
        /// <para>Клавиша завершения работы (по умолчанию Escape) - завершение работы меню</para>
        /// </summary>
        Buttons,
        /// <summary>
        /// <para>Режим управления с помощью цифр:</para>
        /// <para>Для выбора необходимого пункта введите в консоль его номер,</para>
        /// <para>Для выполнения пункта нажмите клавишу Enter,</para>
        /// <para>Для завершения работы меню введите в консоли стоп-слово (по умолчанию exit)</para>
        /// </summary>
        Numbers
    }
    /// <summary>
    /// Класс консольного меню
    /// </summary>
    public class CMenu
    {
        /// <summary>
        /// Стоп-слово для завершения работы меню в режиме Numbers (по умолчанию exit). Регистры не учитываются.
        /// </summary>
        public string StopWord { get; set; } = "exit";
        /// <summary>
        /// Клавиша для завершения работы меню в режиме  Buttons (по умолчанию Escape)
        /// </summary>
        public ConsoleKey StopButton { get; set; } = ConsoleKey.Escape;
        List<MenuPoint> points { get; set; }
        /// <summary>
        /// Создает экземпляр консольного меню из списка пунктов меню
        /// </summary>
        /// <param name="points">Список пунктов меню</param>
        public CMenu(List<MenuPoint> points) => this.points = points;
        /// <summary>
        /// Создает пустое консольное меню без пунктов
        /// </summary>
        public CMenu() => points = new List<MenuPoint>();
        /// <summary>
        /// Добавляет один пункт в меню
        /// </summary>
        /// <param name="point">Пункт</param>
        public void AddPoint(MenuPoint point) => points.Add(point);
        /// <summary>
        /// Добавляет список пунктов в меню
        /// </summary>
        /// <param name="points">Список пунктов</param>
        public void AddPoint(List<MenuPoint> points)
        {
            foreach (MenuPoint point in points)
            {
                this.points.Add(point);
            }
        }
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken;
        /// <summary>
        /// Запускает меню в режиме по умолчанию (управление стрелками на клавиатуре)
        /// </summary>
        public void RunMenu()
        {
            cancellationToken = cts.Token;
            Task run = Task.Factory.StartNew(() => RunMenuButtons());
            run.Wait();
        }
        /// <summary>
        /// Запускает меню в выбранном режиме
        /// </summary>
        /// <param name="mode">Желаемый режим запуска меню</param>
        public void RunMenu(MenuModes mode)
        {
            if (mode == MenuModes.Buttons)
            {
                cancellationToken = cts.Token;
                Task run = Task.Factory.StartNew(() => RunMenuButtons());
                run.Wait();
            }
            else
            {
                cancellationToken = cts.Token;
                Task run = Task.Factory.StartNew(() => RunMenuNumbers());
                run.Wait();
            }
        }
        private void RunMenuNumbers()
        {
            try
            {
                if (points.Count == 0) throw new Exception("Ошибка: в меню нет пунктов");

                string choice = "0";
                while (!cancellationToken.IsCancellationRequested && 
                    !choice.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].ToLower().Equals(StopWord.ToLower()))
                {
                    if (cancellationToken.IsCancellationRequested) throw new Exception("Завершение работы меню");
                    Console.Clear();
                    for (int i = 0; i < points.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} {points[i].Name}");
                    }
                    choice = Console.ReadLine();
                    if (int.TryParse(choice, out int num) && num <= points.Count)
                    {
                        if (points[num - 1].IsExitPoint) cts.Cancel();
                        else points[num - 1].ExecuteMethod();
                    }
                    else if (num > points.Count)
                    {
                        Console.WriteLine($"Пункта с подобным номером не существует. Введите число от 1 до {points.Count}");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
        private void RunMenuButtons()
        {
            try
            {
                if (points.Count == 0) throw new Exception("Ошибка: в меню нет пунктов");

                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Выберите один из следующих пунктов меню с помощью клавиш " +
                    "<Стрелка вверх> и <Стрелка вниз> " +
                    "и нажмите клавишу <Enter> для перехода к пункту");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"1 {points[0].Name}");
                Console.ForegroundColor = ConsoleColor.White;

                for (int i = 1; i < points.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {points[i].Name}");
                }
                int Cursor = 0;
                ConsoleKey ck = 0;

                while(!cancellationToken.IsCancellationRequested && 
                    ck != StopButton)
                {
                    if (cancellationToken.IsCancellationRequested) throw new Exception("Завершение работы меню");
                    ck = Console.ReadKey(true).Key;
                    if (ck == ConsoleKey.DownArrow && Cursor < points.Count - 1)
                    {
                        Cursor++;
                    }
                    else if (ck == ConsoleKey.UpArrow && Cursor > 0)
                    {
                        Cursor--;
                    }
                    else if (ck == ConsoleKey.Enter)
                    {
                        if (points[Cursor].IsExitPoint) cts.Cancel();
                        else points[Cursor].ExecuteMethod();
                    }
                    else
                    {
                        continue;
                    }
                    Console.Clear();
                    Console.WriteLine("Выберите один из следующих пунктов меню с помощью клавиш " +
                        "<Стрелка вверх> и <Стрелка вниз> " +
                        "и нажмите клавишу <Enter> для перехода к пункту");
                    for (int i = 0; i < points.Count; i++)
                    {
                        if (i == Cursor)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{i + 1} {points[i].Name}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1} {points[i].Name}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
