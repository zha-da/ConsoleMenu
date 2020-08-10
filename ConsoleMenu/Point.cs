using System;
namespace ConsoleMenu
{
    public class Point
    {
        Action method { get; set; }
        public string Name { get; set; }
        public Point(string name, Action method)
        {
            Name = name;
            this.method = method;
        }
        public void ExecuteMethod() => method?.Invoke();
    }
}
