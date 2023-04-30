using System;
using System.Runtime.Serialization;

namespace Gwtdo;

[Serializable]
public class FeatureException : Exception
{
    public FeatureException(string? message) : base(message)
    { 
    }

    public FeatureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public FeatureException(SerializationInfo info, StreamingContext context): base(info, context)
    {
    }
}