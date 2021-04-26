using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result // Result = base()
    {
        public SuccessResult(string message):base(true,message)
        {

        }

        // Tek parametre li olanı da kullanabilme için bir tan daha constuctur oluşturduk
        public SuccessResult() : base(true)
        {

        }
    }
}
