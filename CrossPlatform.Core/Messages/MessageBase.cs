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

        public MessageBase(string pSystemKey, string pMessage) : this()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pSystemKey) || string.IsNullOrWhiteSpace(pMessage))
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "pSystemKey|pMessage"));

                this.SystemKey = pSystemKey;
                this.Message = pMessage;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
