using System;

namespace Db
{
    public class CDbException : Exception
    {
        public CDbException()
        {
        }

        public CDbException(string message)
            : base(message)
        {
        }

        public CDbException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
