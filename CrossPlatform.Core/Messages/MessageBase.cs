using System;

namespace CrossPlatform.Messages
{
    public abstract class MessageBase
    {
        public string SystemKey { get; set; }
        public string Message { get; set; }

        public MessageBase()
        {

        }

        public MessageBase(string systemKey, string message) : this()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(systemKey) || string.IsNullOrWhiteSpace(message))
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "systemKey|message"));

                this.SystemKey = systemKey;
                this.Message = message;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
