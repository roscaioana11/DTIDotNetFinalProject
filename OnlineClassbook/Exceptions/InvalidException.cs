﻿using System.Runtime.Serialization;

namespace OnlineClassbook.Exceptions;

[Serializable]
public class InvalidException:Exception
{
    public InvalidException()
    {
    }

    public InvalidException(string? message) : base(message)
    {
    }

    public InvalidException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}