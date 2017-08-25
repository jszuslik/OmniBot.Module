using System;

namespace OmniBot.Services.Exceptions
{
    public class OmniBotException : Exception
    {
        public OmniBotException(string message)
        :base(message) { }

        public OmniBotException(string format, params object[] args)
        : base(string.Format(format, args)) { }

        public OmniBotException(string message, Exception innerException)
        : base(message, innerException) { }

        public OmniBotException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }
    }
}
