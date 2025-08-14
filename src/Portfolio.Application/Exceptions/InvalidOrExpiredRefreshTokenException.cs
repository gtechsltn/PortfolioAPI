using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Exceptions
{
    public class InvalidOrExpiredRefreshTokenException : Exception
    {
        public InvalidOrExpiredRefreshTokenException(string message) : base(message) { }
    }
}
