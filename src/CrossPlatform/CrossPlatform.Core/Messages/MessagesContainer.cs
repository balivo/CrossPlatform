using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CrossPlatform.Messages
{
    public abstract class MessagesContainer
    {
        #region [ Fields ]

        private List<BrokenRuleMessage> _BrokenRules = new List<BrokenRuleMessage>();
        private List<ErrorMessage> _Errors = new List<ErrorMessage>();

        #endregion

        #region [ Properties ]

        public ReadOnlyCollection<BrokenRuleMessage> BrokenRules
        {
            get { return new ReadOnlyCollection<BrokenRuleMessage>(_BrokenRules); }
            set
            {
                if (value != null)
                {
                    foreach (var _brokenRule in value)
                    {
                        AddBrokenRule(_brokenRule);
                    }
                }
            }
        }

        public ReadOnlyCollection<ErrorMessage> Errors
        {
            get { return new ReadOnlyCollection<ErrorMessage>(_Errors); }
            set
            {
                if (value != null)
                {
                    foreach (var _error in value)
                    {
                        AddError(_error);
                    }
                }
            }
        }

        [JsonIgnore]
        public bool IsValid { get { return !(HasErrors || HasImpediments); } }

        [JsonIgnore]
        public bool HasErrors { get { return _Errors.Count() > 0; } }

        [JsonIgnore]
        public bool HasImpediments { get { return _BrokenRules.Count(lbda => lbda.Type == BrokenRuleMessageTypes.Impediment) > 0; } }

        [JsonIgnore]
        public bool HasAttentions { get { return _BrokenRules.Count(lbda => lbda.Type == BrokenRuleMessageTypes.Attention) > 0; } }

        #endregion

        public MessagesContainer()
            : base()
        {
            try
            {
                _BrokenRules = new List<BrokenRuleMessage>();
                _Errors = new List<ErrorMessage>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #region [ Methods ]

        public void AddBrokenRule(BrokenRuleMessageTypes messageType, string systemKey, string message)
        {
            try
            {
                if (_BrokenRules.Count(lbda => lbda.Type == messageType && lbda.SystemKey.Equals(systemKey)) == 0)
                    _BrokenRules.Add(new BrokenRuleMessage(messageType, systemKey, message));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddBrokenRule(BrokenRuleMessage brokenRule)
        {
            try
            {
                if (brokenRule == null)
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "brokenRule"));

                AddBrokenRule(brokenRule.Type, brokenRule.SystemKey, brokenRule.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddBrokenRules(IEnumerable<BrokenRuleMessage> brokenRules)
        {
            try
            {
                if (brokenRules == null)
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "brokenRules"));

                foreach (var _brokenRule in brokenRules)
                {
                    AddBrokenRule(_brokenRule);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public BrokenRuleMessage GetBrokenRule(string systemKey)
        {
            try
            {
                return _BrokenRules.FirstOrDefault(lbda => lbda.SystemKey.Equals(systemKey));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ReadOnlyCollection<BrokenRuleMessage> GetBrokenRules(BrokenRuleMessageTypes messageType)
        {
            try
            {
                return new ReadOnlyCollection<BrokenRuleMessage>(_BrokenRules.Where(lbda => lbda.Type == messageType).ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RemoveBrokenRule(string systemKey)
        {
            try
            {
                _BrokenRules.RemoveAll(lbda => lbda.SystemKey.Equals(systemKey));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddRequiredPropertyBrokenRule(string propertyName, string message = null)
        {
            try
            {
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, CreateRequiredPropertyBrokenRuleSystemKey(propertyName), string.IsNullOrWhiteSpace(message) ? CreateRequiredPropertyBrokenRuleMessage(propertyName) : message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void RemoveRequiredPropertyBrokenRule(string propertyName)
        {
            try
            {
                RemoveBrokenRule(CreateRequiredPropertyBrokenRuleSystemKey(propertyName));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddInvalidPropertyBrokenRule(string propertyName, string message = null)
        {
            try
            {
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, CreateInvalidPropertyBrokenRuleSystemKey(propertyName), string.IsNullOrWhiteSpace(message) ? CreateInvalidPropertyBrokenRuleMessage(propertyName) : message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void RemoveInvalidPropertyBrokenRule(string propertyName)
        {
            try
            {
                RemoveBrokenRule(CreateInvalidPropertyBrokenRuleSystemKey(propertyName));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddError(string systemKey, string message)
        {
            try
            {
                if (_Errors.Count(lbda => lbda.SystemKey.Equals(systemKey)) == 0)
                    _Errors.Add(new ErrorMessage(systemKey, message));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddError(ErrorMessage errorMessage)
        {
            try
            {
                if (errorMessage == null)
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "errorMessage"));

                AddError(errorMessage.SystemKey, errorMessage.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddErrors(IEnumerable<ErrorMessage> errorMessages)
        {
            try
            {
                if (errorMessages == null)
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "errorMessages"));

                foreach (var _error in errorMessages)
                {
                    AddError(_error);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        public void AddException(Exception ex)
        {
            if (ex.GetType() == typeof(InvalidOperationException))
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, "InvalidOperationException", ex.Message);
            else if (ex.GetType() == typeof(ArgumentException))
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, "ArgumentException", ex.Message);
            else if (ex.GetType() == typeof(ArgumentNullException))
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, "ArgumentNullException", ex.Message);
            else if (ex.GetType() == typeof(ArgumentOutOfRangeException))
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, "ArgumentOutOfRangeException", ex.Message);
            else
                AddError("Exception", string.Format("Erro desconhecido. (ExceptionType: {0} - Message: {1})", ex.GetType().ToString(), ex.Message));
        }

        private static string CreateInvalidPropertyBrokenRuleMessage(string propertyName)
        {
            return string.Format("A propriedade \"{0}\" está inválida", propertyName);
        }

        private static string CreateInvalidPropertyBrokenRuleSystemKey(string propertyName)
        {
            return string.Format("{0}Invalid", propertyName);
        }

        private static string CreateRequiredPropertyBrokenRuleMessage(string propertyName)
        {
            return string.Format("A propriedade \"{0}\" é requerida", propertyName);
        }

        private static string CreateRequiredPropertyBrokenRuleSystemKey(string propertyName)
        {
            return string.Format("{0}Required", propertyName);
        }

        protected bool HasInvalidPropertyBrokenRule(string propertyName)
        {
            return GetBrokenRule(CreateInvalidPropertyBrokenRuleSystemKey(propertyName)) != null;
        }

        protected bool HasRequiredPropertyBrokenRule(string propertyName)
        {
            return GetBrokenRule(CreateRequiredPropertyBrokenRuleSystemKey(propertyName)) != null;
        }

        public void CopyMessages(MessagesContainer messagesContainer)
        {
            if (messagesContainer != null)
            {
                AddBrokenRules(messagesContainer.BrokenRules);
                AddErrors(messagesContainer.Errors);
            }
        }
    }
}