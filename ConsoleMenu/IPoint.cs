namespace ConsoleMenu
{
    public interface IPoint
    {
        string Name { get; set; }
        void ExecuteMethod(params object[] pr);
    }
}