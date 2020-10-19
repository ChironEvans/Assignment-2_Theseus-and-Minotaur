using System;
using System.Runtime.Serialization;

namespace Assignment_2_Theseus_and_Minotaur
{
    [Serializable]
    internal class ExitNotExist : Exception
    {
        public ExitNotExist()
        {
        }

        public ExitNotExist(string message) : base(message)
        {
        }

        public ExitNotExist(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExitNotExist(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}