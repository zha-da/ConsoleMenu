using System;

namespace ConsoleMenu
{
    /// <summary>
    /// Класс пункта меню
    /// </summary>
    public class MenuPoint
    {
        Action method { get; set; }
        Func<object> r_method { get; set; }
        /// <summary>
        /// Указывает, отвечает ли пункт за завершение работы меню
        /// </summary>
        public bool IsExitPoint { get; set; } = false;
        /// <summary>
        /// Данное имя будет отображаться при запуске консольного меню
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Определяет, возвращает ли пункт меню какое-либо значение
        /// </summary>
        public bool ReturnsObject { get; private set; } = false;
        /// <summary>
        /// Возвращенное значение
        /// </summary>
        public PointExecutionResult ReturnedValue { get; private set; }
        /// <summary>
        /// Создает экземпляр пункта меню
        /// </summary>
        /// <param name="name">Имя пункта меню. Отображается при запуске консольного меню</param>
        /// <param name="method">
        /// Метод, с которым ассоциируется пункт. Метод не возвращает значений.
        /// Для передачи методов с параметрами используйте следующую запись
        /// <example>
        /// <code>
        /// Point testPoint = new Point("test name", () => ExampleMethod(someParameters), returnHere);
        /// </code>
        /// </example>
        /// Для передачи методов без параметров можно использовать другую запись
        /// <example>
        /// <code>
        /// Point testPoint = new Point("test name", ExampleMethod, returnHere);
        /// </code>
        /// </example>
        /// </param>
        public MenuPoint(string name, Action method)
        {
            Name = name;
            this.method = method;
            ReturnsObject = false;
        }
        /// <summary>
        /// Создает экземпляр пункта меню
        /// </summary>
        /// <param name="name">Имя пункта меню. Отображается при запуске консольного меню</param>
        /// <param name="r_method">
        /// Метод, с которым ассоциируется пункт. Метод возвращает значение типа <c>object</c>.
        /// Для передачи методов с параметрами используйте следующую запись
        /// <example>
        /// <code>
        /// Point testPoint = new Point("test name", () => ExampleMethod(someParameters), returnHere);
        /// </code>
        /// </example>
        /// Для передачи методов без параметров можно использовать другую запись
        /// <example>
        /// <code>
        /// Point testPoint = new Point("test name", ExampleMethod, returnHere);
        /// </code>
        /// </example>
        /// </param>
        /// <param name="result">Результат выполнения пункта меню хранится здесь</param>
        public MenuPoint(string name, Func<object> r_method, PointExecutionResult result)
        {
            Name = name;
            this.r_method = r_method;
            ReturnedValue = result;
            ReturnsObject = true;
        }
        /// <summary>
        /// Создает пункт меню, отвечающий за завершение работы меню
        /// </summary>
        /// <param name="name">Имя пункта. Рекомендуется выбирать имя обозначающее выход из меню (например, "Выход")</param>
        public MenuPoint(string name)
        {
            Name = name;
            IsExitPoint = true;
        }
        /// <summary>
        /// Запускает выполнение пункта меню
        /// </summary>
        public void ExecuteMethod()
        {
            if (ReturnsObject)
            {
                ReturnedValue.result = r_method?.Invoke();
            }
            else
            {
                method?.Invoke();
            }
        }
    }
    /// <summary>
    /// Ссылка на результат выполнения пункта меню
    /// </summary>
    public class PointExecutionResult
    {
        /// <summary>
        /// Результат выполнения пункта меню типа <c>object</c>
        /// </summary>
        public object result;
    }
}
