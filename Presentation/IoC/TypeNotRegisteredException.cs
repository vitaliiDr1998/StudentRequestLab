using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string message)
            : base(message)
        {
        }
    }
}