using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message) : base(false, message)
        {

        }

        // Tek parametre li olanı da kullanabilme için bir tan daha constuctur oluşturduk
        public ErrorResult() : base(false)
        {

        }
    }
}
