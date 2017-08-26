namespace CrossPlatform.Messages
{
    public sealed class BrokenRuleMessage : MessageBase
    {
        public BrokenRuleMessageTypes Type { get; set; }

        public BrokenRuleMessage()
            : base()
        {

        }

        public BrokenRuleMessage(BrokenRuleMessageTypes pType, string pSystemKey, string pMessage)
            : base(pSystemKey, pMessage)
        {
            this.Type = pType;
        }
    }
}