using System;

namespace Folio3.Sbp.Service.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}