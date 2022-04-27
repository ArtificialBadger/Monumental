using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(bool isBad, bool isVeryBad)
        {
        }
    }
}
