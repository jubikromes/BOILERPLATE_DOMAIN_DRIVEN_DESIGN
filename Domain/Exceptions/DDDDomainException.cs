using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DDDDomainException : Exception
    {
        public DDDDomainException()
        { }

        public DDDDomainException(string message)
            : base(message)
        { }

        public DDDDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

}
