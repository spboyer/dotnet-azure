namespace dotnet_azure.Common
{

  public class LogingException : System.Exception
  {
      public LogingException() { }
      public LogingException(string message) : base(message) { }
      public LogingException(string message, System.Exception inner) : base(message, inner) { }
      protected LogingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }

}