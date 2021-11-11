using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;
using ReactWebApiSample.Models;

namespace ReactWebApiSample
{
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

        private sealed class LimitedBinder
            : ISerializationBinder
        {
            private readonly HashSet<Type> _allowedTypes = new HashSet<Type>
            {
                typeof(Exception),

                typeof(CombinedQueryModel),
                typeof(CombinedResultModel),
                typeof(ComplexAModel),
                typeof(ComplexBModel),
                typeof(ComplexBaseModel),
                typeof(SimpleModel),

                typeof(List<CombinedQueryModel>),
                typeof(List<CombinedResultModel>),
                typeof(List<ComplexAModel>),
                typeof(List<ComplexBModel>),
                typeof(List<ComplexBaseModel>),
                typeof(List<SimpleModel>),
            };

            public Type BindToType(string? assemblyName, string typeName)
            {
                var type = Type.GetType($"{typeName}, {assemblyName}", true);
                if (type is null)
                {
                    throw new TypeNotWhitelistedException();
                }

                if (_allowedTypes.Contains(type))
                {
                    return type;
                }

                // Don’t return null for unexpected types –
                // this makes some serializers fall back to a default binder, allowing exploits.
                throw new TypeNotWhitelistedException("Unexpected serialized type");
            }

            public void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
            {
                assemblyName = string.Empty;
                typeName = serializedType.Name;
            }
        }
    }
}
