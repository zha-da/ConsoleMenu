using ConsoleMenu;
using System;
using System.Collections.Generic;

namespace lab6_17
{
    class Program
    {
        static void Main(string[] args)
        {
            Point point = new Point("jov", Hello);
            Point point1 = new Point("bem", HowAreYou);
            Point point2 = new Point("nes", ImFine);
            PointExecutionResult returnTo = new PointExecutionResult();
            Point point3 = new Point("bip", Line, returnTo);
            //point3.ExecuteMethod();
            //Console.WriteLine(returnTo.result.ToString());
            CMenu cm = new CMenu();
            cm.AddPoint(new List<Point> { point, point1, point2, point3 });
            cm.RunMenu();
            
            //Console.ReadKey();
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
