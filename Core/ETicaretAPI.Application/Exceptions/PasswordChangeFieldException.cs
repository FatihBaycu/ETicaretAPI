using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class PasswordChangeFieldException:Exception
    {
        public PasswordChangeFieldException() : base("There was a problem updating the password.")
        {
        }

        public PasswordChangeFieldException(string? message) : base(message)
        {
        }

        public PasswordChangeFieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
