using System;
using System.Collections.Generic;
using System.Text;

namespace DaisyStudy.Utilities.Exceptions
{
    public class DaisyStudyException : Exception
    {
        public DaisyStudyException()
        {
        }

        public DaisyStudyException(string message) : base(message)
        {
        }

        public DaisyStudyException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}