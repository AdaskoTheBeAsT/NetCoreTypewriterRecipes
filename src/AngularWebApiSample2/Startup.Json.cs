using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AngularWebApiSample2.Models;
using Newtonsoft.Json.Serialization;

namespace AngularWebApiSample;

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

        protected TypeNotWhitelistedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
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

        protected TypeNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
#pragma warning restore CA1034
}
