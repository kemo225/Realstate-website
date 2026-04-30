using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Exceptions
{
    public class ValidatationException : Exception
    {
        public ValidatationException(string message) : base(message)
        {
        }
    }
}
