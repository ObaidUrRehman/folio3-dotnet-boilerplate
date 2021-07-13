using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Service.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(string message = null, Exception innerException = null)
            : base(message, innerException) { }
    }
}
