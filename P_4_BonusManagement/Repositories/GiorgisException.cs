using System.Runtime.Serialization;

namespace P_4_BonusManagement.Repositories
{
    [Serializable]
    internal class GiorgisException : Exception
    {
        public GiorgisException()
        {
        }

        public GiorgisException(string? message) : base(message)
        {
        }

        public GiorgisException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GiorgisException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}