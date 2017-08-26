namespace CrossPlatform.Messages
{
    public sealed class ErrorMessage : MessageBase
    {
        public ErrorMessage()
            : base()
        {

        }

        public ErrorMessage(string pSystemKey, string pMessage)
            : base(pSystemKey, pMessage)
        {
        }
    }
}
