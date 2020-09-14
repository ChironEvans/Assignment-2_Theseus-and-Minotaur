using System;
using System.Runtime.Serialization;

namespace Assignment_2_Theseus_and_Minotaur
{
    [Serializable]
    internal class TheseusNotExist : Exception
    {
        public TheseusNotExist()
        {
        }

        public TheseusNotExist(string message) : base(message)
        {
        }

        public TheseusNotExist(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TheseusNotExist(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}