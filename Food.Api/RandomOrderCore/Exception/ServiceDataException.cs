using System;

namespace RandomOrderCore.Exception
{
    [Serializable]
    public class ServiceDataException : ApplicationException
    {
        public ServiceDataException()
        {
        }

        public ServiceDataException(string message, int hResult)
            : base(message)
        {
            HResult = hResult;
        }

        public ServiceDataException(string message, System.Exception inner, int hResult)
            : base(message, inner)
        {
            HResult = hResult;
        }
    }
}
