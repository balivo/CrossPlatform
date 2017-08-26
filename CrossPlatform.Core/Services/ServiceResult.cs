using CrossPlatform.Messages;
using System;

namespace CrossPlatform.Services
{
    public class ServiceResult : MessagesContainer
    {
        public ServiceResult() : base()
        {
        }

        public ServiceResult(MessagesContainer pMessagesContainer) : this()
        {
            this.CopyMessages(pMessagesContainer);
        }

        public ServiceResult(Exception pException) : this()
        {
            this.AddException(pException);
        }
    }
}