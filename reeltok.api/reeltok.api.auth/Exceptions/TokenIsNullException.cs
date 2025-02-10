using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.auth.Exceptions
{
    public class TokenIsNullException: Exception
    {
        public TokenIsNullException()
        {
        }

        public TokenIsNullException(string message) : base(message)
        {
        }

        public TokenIsNullException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
