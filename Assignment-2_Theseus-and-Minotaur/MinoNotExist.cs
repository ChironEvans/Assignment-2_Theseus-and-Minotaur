using System;
using System.Runtime.Serialization;

namespace Assignment_2_Theseus_and_Minotaur
{
    [Serializable]
    internal class MinoNotExist : Exception
    {
        public MinoNotExist()
        {
        }

        public MinoNotExist(string message) : base(message)
        {
        }

        public MinoNotExist(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MinoNotExist(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}