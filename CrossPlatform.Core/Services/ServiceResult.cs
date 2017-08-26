using CrossPlatform.Messages;

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
    }
}