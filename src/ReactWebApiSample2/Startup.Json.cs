using System;
using System.Runtime.Serialization;

namespace ReactWebApiSample2;

public partial class Startup
{
#pragma warning disable CA1034
    [Serializable]
    public class TypeNotWhitelistedException
        : Exception
    {
        public TypeNotWhitelistedException()
        {
        }

        public TypeNotWhitelistedException(string message)
            : base(message)
        {
        }

        public TypeNotWhitelistedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    [Serializable]
    public class TypeNotFoundException
        : Exception
    {
        public TypeNotFoundException()
        {
        }

        public TypeNotFoundException(string message)
            : base(message)
        {
        }

        public TypeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
#pragma warning restore CA1034
}
