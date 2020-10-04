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
        /// Текущие настройки работы меню
        /// </summary>
        public MenuSettings CurrentSettings { get; set; } = new MenuSettings();
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
        /// Добавляет пользовательские настройки меню
        /// </summary>
        /// <param name="userSettings">Пользовательские настройки</param>
        public void AddUserSettings(MenuSettings userSettings)
        {
            CurrentSettings = userSettings;
        }
        /// <summary>
        /// Запускает меню в режиме по умолчанию (управление стрелками на клавиатуре)
        /// </summary>
        public void RunMenu()
        {
            if (CurrentSettings.CurrentMode == MenuModes.Buttons)
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
                if (points.Count == 0) throw new MenuIsEmptyException("Ошибка: в меню нет пунктов");

                string choice = "0";
                while (!cancellationToken.IsCancellationRequested && 
                    !choice.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].ToLower().Equals(CurrentSettings.ExitWord.ToLower()))
                {
                    if (cancellationToken.IsCancellationRequested) throw new CancellationException("Ошибка: завершение работы меню");
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
            catch (MenuIsEmptyException mex)
            {
                Console.WriteLine(mex.Message);
                Console.ReadKey();
            }
            catch (CancellationException cex)
            {
                Console.WriteLine(cex.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            finally
            {
                Console.WriteLine(CurrentSettings.ClosingPhrase);
                Console.ReadKey();
            }
        }
        private void RunMenuButtons()
        {
            try
            {
                if (points.Count == 0) throw new MenuIsEmptyException("Ошибка: в меню нет пунктов");

                Console.CursorVisible = false;
                Console.ForegroundColor = CurrentSettings.RestingColor;
                Console.WriteLine(CurrentSettings.OpeningPhrase);
                Console.ForegroundColor = CurrentSettings.HighlightColor;
                Console.WriteLine($"1 {points[0].Name}");
                Console.ForegroundColor = CurrentSettings.RestingColor;

                for (int i = 1; i < points.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {points[i].Name}");
                }
                int Cursor = 0;
                ConsoleKey ck = 0;

                while(!cancellationToken.IsCancellationRequested && 
                    ck != CurrentSettings.ExitKey)
                {
                    if (cancellationToken.IsCancellationRequested) throw new CancellationException("Ошибка: завершение работы меню");
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

                    Console.WriteLine(CurrentSettings.OpeningPhrase);
                    for (int i = 0; i < points.Count; i++)
                    {
                        if (i == Cursor)
                        {
                            Console.ForegroundColor = CurrentSettings.HighlightColor;
                            Console.WriteLine($"{i + 1} {points[i].Name}");
                            Console.ForegroundColor = CurrentSettings.RestingColor;
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1} {points[i].Name}");
                        }
                    }
                }

            }
            catch (MenuIsEmptyException mex)
            {
                Console.WriteLine(mex.Message);
                Console.ReadKey();
            }
            catch (CancellationException cex)
            {
                Console.WriteLine(cex.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            finally
            {
                Console.WriteLine(CurrentSettings.ClosingPhrase);
                Console.ReadKey();
            }
        }
    }
}
