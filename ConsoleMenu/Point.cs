using System;
namespace ConsoleMenu
{
    public class Point
    {
        public delegate void PointMethods(object[] args);
        public PointMethods method { get; private set; }
        public string Name { get; set; }
        /// <summary>
        /// Создает новый пункт меню
        /// </summary>
        /// <param name="name">Название пункта меню</param>
        /// <param name="method">Имя метода, который данный пункт меню будет выполнять</param>
        public Point(string name, PointMethods method)
        {
            Name = name;
            this.method = method;
        }
        /// <summary>
        /// Выполняет метод, связанный с пунктом меню
        /// </summary>
        /// <param name="args"></param>
        public void ExecuteMethod(object[] args) => method?.Invoke(args);
    }
}
