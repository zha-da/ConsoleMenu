using ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace lab6_17
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Matrix
            PointExecutionResult Matrix = new PointExecutionResult();
            Point inkb = new Point("Ввод матрицы с клавиатуры", GetMatrixFromKeyboard, Matrix);
            Point inf = new Point("Ввод матрицы из файла", GetMatrixFromFile, Matrix);
            Point chrct = new Point("Вычисление характеристики", () => Characteristic((int[,])Matrix.result));
            Point chgmt = new Point("Преобразование матрицы", () => ChangeMatrix(Matrix));
            Point wrt = new Point("Печать матрицы", () => WriteMatrix((int[,])Matrix.result));
            CMenu menu = new CMenu(new List<Point> { inkb, inf, chrct, chgmt, wrt });
            menu.RunMenu(MenuModes.Buttons);
            #endregion
        }
        static int[,] GetMatrixFromKeyboard()
        {
            Console.Write("Введите размерность матрицы: ");
            Console.CursorVisible = true;
            string[] Size = Console.ReadLine().Split(' ');
            Console.WriteLine();
            int n = int.Parse(Size[0]);
            int m = int.Parse(Size[1]);
            Console.WriteLine($"Введите матрицу размера {n} на {m}: ");
            string[] NewRow;
            int[,] Matrix = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                NewRow = Console.ReadLine().Split(' ');
                for (int j = 0; j < m; j++)
                {
                    Matrix[i, j] = int.Parse(NewRow[j]);
                }
            }
            Console.CursorVisible = false;
            Console.WriteLine("Матрица введена");
            Console.WriteLine("Нажмите любую кнопку для продолжения");
            Console.ReadKey();
            return Matrix;
        }
        static int[,] GetMatrixFromFile()
        {
            string FileName = "input1.txt";
            int[,] Matrix;
            int Rows = 0;
            int Columns = 0;
            StringBuilder Array = new StringBuilder();
            using (StreamReader sr = new StreamReader(FileName))
            {
                string NewLine;
                while ((NewLine = sr.ReadLine()) != null)
                {
                    Array.Append(NewLine);
                    Array.Append(" ");
                    string[] Col = NewLine.Split(' ');
                    Columns = Col.Length;
                    Rows++;
                }
                Matrix = new int[Rows, Columns];
                string[] AllNums = Array.ToString().Split(' ');
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        Matrix[i, j] = int.Parse(AllNums[i * Columns + j]);
                    }
                }
                sr.Close();
            }
            Console.WriteLine("Матрица загружена");
            Console.WriteLine("Нажмите любую кнопку для продолжения");
            Console.ReadKey();
            return Matrix;
        }
        static void Characteristic(int[,] Matrix)
        {
            try
            {
                if (Matrix == null) throw new Exception("Матрица не инициализорована");
                int[] MaxElements = new int[Matrix.GetLength(0)];
                for (int i = 0; i < MaxElements.Length; i++)
                {
                    MaxElements[i] = -100000;
                }
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        if (MaxElements[i] < Matrix[i, j])
                        {
                            MaxElements[i] = Matrix[i, j];
                        }
                    }
                }
                bool IsGood = true;
                for (int i = 0; i < MaxElements.Length - 1; i++)
                {
                    if (MaxElements[i] <= MaxElements[i + 1])
                    {
                        IsGood = false;
                        break;
                    }
                }
                if (IsGood) Console.WriteLine("Матрица подходит под характеристику");
                else Console.WriteLine("Матрица не подходит под характеристику");
                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }

        }
        static void ChangeMatrix(PointExecutionResult mat)
        {
            try
            {
                int[,] Matrix = (int[,])mat.result;
                if (Matrix == null) throw new Exception("Матрица не инициализирована");
                int Rows = Matrix.GetLength(0);
                int Columns = Matrix.GetLength(1);
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns / 2; j++)
                    {
                        int temp = Matrix[i, j];
                        Matrix[i, j] = Matrix[i, Columns - j - 1];
                        Matrix[i, Columns - j - 1] = temp;
                    }
                }
                mat.result = Matrix;
                Console.WriteLine("Матрица изменена");
                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
        }
        static void WriteMatrix(int[,] Matrix)
        {
            try
            {
                if (Matrix == null) throw new Exception("Матрица не инициализирована");
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        Console.Write($"{Matrix[i, j],3}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Матрица выведена");
                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Нажмите любую кнопку для продолжения");
                Console.ReadKey();
            }
        }
    }
}
