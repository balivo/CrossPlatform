namespace CrossPlatform.Messages
{
    public sealed class BrokenRuleMessage : MessageBase
    {
        public BrokenRuleMessageTypes Type { get; set; }

        public BrokenRuleMessage()
            : base()
        {

        }

        public BrokenRuleMessage(BrokenRuleMessageTypes messageType, string systemKey, string message)
            : base(systemKey, message)
        {
            this.Type = messageType;
        }
    }
}