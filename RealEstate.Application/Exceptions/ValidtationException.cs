using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Application.Exceptions
{
    public class ValidtationException : Exception
    {
        public ValidtationException(string message) : base(message)
        {
        }
    }
 }
