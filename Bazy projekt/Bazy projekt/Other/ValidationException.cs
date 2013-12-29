using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazy_projekt.Other
{
    public class ValidationException : Exception
    {
        public ValidationException(String message) : base(message) { }
    }
}
