
namespace Stemstudios.DataAccessLayer
{
    public class DatabaseConnectionException : System.Exception
    {
            public DatabaseConnectionException() : base() { }
    public DatabaseConnectionException(string message) : base(message) { }
    public DatabaseConnectionException(string message, System.Exception inner) : base(message, inner) { }

    // Constructor needed for serialization 
    // when exception propagates from a remoting server to the client. 
    protected DatabaseConnectionException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) { }
    }
}
