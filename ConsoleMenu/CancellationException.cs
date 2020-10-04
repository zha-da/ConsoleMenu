using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleMenu
{
    internal class CancellationException : Exception
    {
        public CancellationException()
            : base()
        { }

        public CancellationException(string message)
            : base(message) 
        { }
    }
}
