using System;
using System.Runtime.Serialization;

namespace dotnet_azure.Common
{
  [Serializable]
  internal class DotNetPublishException : Exception
  {
    public DotNetPublishException()
    {
    }

    public DotNetPublishException(string message) : base(message)
    {
    }

    public DotNetPublishException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected DotNetPublishException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}