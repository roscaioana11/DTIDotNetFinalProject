﻿using System.Runtime.Serialization;

namespace OnlineClassbook.Exceptions;

[Serializable]
public class DuplicatedException:Exception
{
    public DuplicatedException()
    {
    }

    public DuplicatedException(string? message) : base(message)
    {
    }

    public DuplicatedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DuplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}