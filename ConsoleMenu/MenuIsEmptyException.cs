using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleMenu
{
    internal class MenuIsEmptyException : Exception
    {
        public MenuIsEmptyException()
            : base()
        { }

        public MenuIsEmptyException(string message)
            : base(message)
        { }
    }
}
