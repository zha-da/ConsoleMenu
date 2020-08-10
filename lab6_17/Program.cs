using ConsoleMenu;
using System;

namespace lab6_17
{
    #region old menu
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int[,] Matrix = null;
    //        string Choice = "0";
    //        while (Choice != "6")
    //        {
    //            Console.Clear();
    //            Console.WriteLine("Выберите один из следующих пунктов меню: ");
    //            Console.WriteLine("1. Ввод матрицы с клавиатуры");
    //            Console.WriteLine("2. Ввод матрицы из файла");
    //            Console.WriteLine("3. Вычисление характеристики");
    //            Console.WriteLine("4. Преобразование матрицы");
    //            Console.WriteLine("5. Печать матрицы");
    //            Console.WriteLine("6. Выход");
    //            Choice = Console.ReadLine();
    //            switch (Choice)
    //            {
    //                case "1":
    //                    Console.Write("Введите размерность матрицы: ");
    //                    string[] Size = Console.ReadLine().Split(' ');
    //                    Console.WriteLine();
    //                    int n = int.Parse(Size[0]);
    //                    int m = int.Parse(Size[1]);
    //                    Console.WriteLine($"Введите матрицу размера {n} на {m}: ");
    //                    Matrix = GetMatrixFromKeyboard(n , m);
    //                    Console.WriteLine("Матрица введена");
    //                    Console.WriteLine("Нажмите любую кнопку для продолжения");
    //                    Console.ReadKey();
    //                    break;
    //                case "2":
    //                    Matrix = GetMatrixFromFile("input1.txt");
    //                    Console.WriteLine("Матрица загружена");
    //                    Console.WriteLine("Нажмите любую кнопку для продолжения");
    //                    Console.ReadKey();
    //                    break;
    //                case "3":
    //                    if (Matrix == null)
    //                    {
    //                        Console.WriteLine("Матрица не инициализирована");
    //                        Console.WriteLine("Нажмите любую кнопку для продолжения");
    //                        Console.ReadKey();
    //                    }
    //                    else
    //                    {
    //                        Console.WriteLine(Characteristic(Matrix));
    //                        Console.WriteLine("Нажмите любую кнопку для продолжения");
    //                        Console.ReadKey();
    //                    }
    //                    break;
    //                case "4":
    //                    if (Matrix == null)
    //                    {
    //                        Console.WriteLine("Матрица не инициализирована");
    //                        Console.WriteLine("Нажмите любую кнопку для продолжения");
    //                        Console.ReadKey();
    //                    }
    //                    else
    //                    {
    //                        Matrix = ChangeMatrix(Matrix);
    //                        Console.WriteLine("Матрица изменена");
    //                        Console.WriteLine("Нажмите любую кнопку для продолжения");
    //                        Console.ReadKey();
    //                    }
    //                    break;
    //                case "5":
    //                    if (Matrix == null)
    //                    {
    //                        Console.WriteLine("Матрица не инициализирована");
    //                        Console.WriteLine("Нажмите любую кнопку для продолжения");
    //                        Console.ReadKey();
    //                    }
    //                    else
    //                    {
    //                        WriteMatrix(Matrix);
    //                        Console.WriteLine("Нажмите любую кнопку для продолжения");
    //                        Console.ReadKey();
    //                    }
    //                    break;
    //                case "6": return;
    //                default: Console.WriteLine("Нажмите цифру от 1 до 6 для продолжения");
    //                    Console.ReadKey();
    //                    break;
    //            }
    //        }

    //    }
    //    static int[,] GetMatrixFromKeyboard(int n, int m)
    //    {
    //        string[] NewRow;
    //        int[,] Matrix = new int[n,m];
    //        for (int i = 0; i < n; i++)
    //        {
    //            NewRow = Console.ReadLine().Split(' ');
    //            for (int j = 0; j < m; j++)
    //            {
    //                Matrix[i, j] = int.Parse(NewRow[j]);
    //            }
    //        }
    //        return (Matrix);
    //    }
    //    static int[,] GetMatrixFromFile(string FileName)
    //    {
    //        int[,] Matrix;
    //        int Rows = 0;
    //        int Columns = 0;
    //        StringBuilder Array = new StringBuilder();
    //        using (StreamReader sr = new StreamReader(FileName))
    //        {
    //            string NewLine;
    //            while ((NewLine = sr.ReadLine()) != null)
    //            {
    //                Array.Append(NewLine);
    //                Array.Append(" ");
    //                string[] Col = NewLine.Split(' ');
    //                Columns = Col.Length;
    //                Rows++;
    //            }
    //            Matrix = new int[Rows, Columns];
    //            string[] AllNums = Array.ToString().Split(' ');
    //            for (int i = 0; i < Rows; i++)
    //            {
    //                for (int j = 0; j < Columns; j++)
    //                {
    //                    Matrix[i, j] = int.Parse(AllNums[i * Columns + j]);
    //                }
    //            }
    //            sr.Close();
    //        }
    //        return Matrix; 
    //    }
    //    static bool Characteristic(int[,] Matrix)
    //    {
    //        int[] MaxElements = new int[Matrix.GetLength(0)];
    //        for (int i = 0; i < MaxElements.Length; i++)
    //        {
    //            MaxElements[i] = -100000;
    //        }
    //        for (int i = 0; i < Matrix.GetLength(0); i++)
    //        {
    //            for (int j = 0; j < Matrix.GetLength(1); j++)
    //            {
    //                if (MaxElements[i] < Matrix[i, j])
    //                {
    //                    MaxElements[i] = Matrix[i, j];
    //                }
    //            }
    //        }
    //        bool IsGood = true;
    //        for (int i = 0; i < MaxElements.Length - 1; i++)
    //        {
    //            if (MaxElements[i] <= MaxElements[i + 1])
    //            {
    //                IsGood = false;
    //            }
    //        }
    //        return IsGood;
    //    }
    //    static int[,] ChangeMatrix(int[,] Matrix)
    //    {
    //        int Rows = Matrix.GetLength(0);
    //        int Columns = Matrix.GetLength(1);
    //        for (int i = 0; i < Rows; i++)
    //        {
    //            for (int j = 0; j < Columns / 2; j++)
    //            {
    //                int temp = Matrix[i, j];
    //                Matrix[i, j] = Matrix[i, Columns - j - 1];
    //                Matrix[i, Columns - j - 1] = temp;
    //            }
    //        }
    //        return Matrix;
    //    }
    //    static void WriteMatrix(int[,] Matrix)
    //    {
    //        //Console.Clear();
    //        for (int i = 0; i < Matrix.GetLength(0); i++)
    //        {
    //            for (int j = 0; j < Matrix.GetLength(1); j++)
    //            {
    //                Console.Write($"{Matrix[i, j],3}");
    //            }
    //            Console.WriteLine();
    //        }
    //    }
    //}
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Point point = new Point("jov", Hello);
            Point point1 = new Point("bem", HowAreYou);
            Point point2 = new Point("nes", ImFine);
            ConsoleMenu.CMenu cm = new ConsoleMenu.CMenu();
            cm.AddPoint(point);
            cm.AddPoint(point1);
            cm.AddPoint(point2);
            cm.RunMenu();
            Console.ReadKey();
        }
        static void inc(object[] args)
        {
            args[0] = 12;
        }
        static int[,] arr(int[,] vs)
        {
            vs[0, 0] = -1;
            return vs;
        }
        static string Line() => "Line";
        static void Hello()
        {
            Console.WriteLine("Hello");
        }
        static void HowAreYou()
        {
            Console.WriteLine("How are you?");
        }
        static void ImFine()
        {
            Console.WriteLine("I am fine");
        }
    }
}
