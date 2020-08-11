using System;
using System.Runtime.CompilerServices;

namespace ConsoleMenu
{
    public class Point
    {
        Action method { get; set; }
        Func<object> r_method { get; set; }
        public string Name { get; set; }
        public bool ReturnsObject { get; private set; } = false;
        public object ReturnedValue { get; private set; }
        public Point(string name, Action method)
        {
            Name = name;
            this.method = method;
            ReturnsObject = false;
        }
        public Point(string name, Func<object> r_method)
        {
            Name = name;
            this.r_method = r_method;
            ReturnsObject = true;
        }
        public void ExecuteMethod()
        {
            if (ReturnsObject)
            {
                ReturnedValue = r_method?.Invoke();
            }
            else
            {
                method?.Invoke();
            }
        }
    }
}
