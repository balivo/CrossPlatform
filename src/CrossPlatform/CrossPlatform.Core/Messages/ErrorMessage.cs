namespace CrossPlatform.Messages
{
    public sealed class ErrorMessage : MessageBase
    {
        public ErrorMessage()
            : base()
        {

        }

        public ErrorMessage(string systemKey, string message)
            : base(systemKey, message)
        {
        }
    }
}
