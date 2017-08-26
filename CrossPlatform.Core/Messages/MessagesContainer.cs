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

        public void AddBrokenRule(BrokenRuleMessageTypes pType, string pSystemKey, string pMessage)
        {
            try
            {
                if (_BrokenRules.Count(lbda => lbda.Type == pType && lbda.SystemKey.Equals(pSystemKey)) == 0)
                    _BrokenRules.Add(new BrokenRuleMessage(pType, pSystemKey, pMessage));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddBrokenRule(BrokenRuleMessage pBrokenRule)
        {
            try
            {
                if (pBrokenRule == null)
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "pBrokenRule"));

                AddBrokenRule(pBrokenRule.Type, pBrokenRule.SystemKey, pBrokenRule.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddBrokenRules(IEnumerable<BrokenRuleMessage> pBrokenRules)
        {
            try
            {
                if (pBrokenRules == null)
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "pBrokenRules"));

                foreach (var _brokenRule in pBrokenRules)
                {
                    AddBrokenRule(_brokenRule);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public BrokenRuleMessage GetBrokenRule(string pSystemKey)
        {
            try
            {
                return _BrokenRules.FirstOrDefault(lbda => lbda.SystemKey.Equals(pSystemKey));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ReadOnlyCollection<BrokenRuleMessage> GetBrokenRules(BrokenRuleMessageTypes pType)
        {
            try
            {
                return new ReadOnlyCollection<BrokenRuleMessage>(_BrokenRules.Where(lbda => lbda.Type == pType).ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RemoveBrokenRule(string pSystemKey)
        {
            try
            {
                _BrokenRules.RemoveAll(lbda => lbda.SystemKey.Equals(pSystemKey));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddRequiredPropertyBrokenRule(string pPropertyName, string pMessage = null)
        {
            try
            {
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, CreateRequiredPropertyBrokenRuleSystemKey(pPropertyName), string.IsNullOrWhiteSpace(pMessage) ? CreateRequiredPropertyBrokenRuleMessage(pPropertyName) : pMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void RemoveRequiredPropertyBrokenRule(string pPropertyName)
        {
            try
            {
                RemoveBrokenRule(CreateRequiredPropertyBrokenRuleSystemKey(pPropertyName));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddInvalidPropertyBrokenRule(string pPropertyName, string pMessage = null)
        {
            try
            {
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, CreateInvalidPropertyBrokenRuleSystemKey(pPropertyName), string.IsNullOrWhiteSpace(pMessage) ? CreateInvalidPropertyBrokenRuleMessage(pPropertyName) : pMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void RemoveInvalidPropertyBrokenRule(string pPropertyName)
        {
            try
            {
                RemoveBrokenRule(CreateInvalidPropertyBrokenRuleSystemKey(pPropertyName));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddError(string pSystemKey, string pMessage)
        {
            try
            {
                if (_Errors.Count(lbda => lbda.SystemKey.Equals(pSystemKey)) == 0)
                    _Errors.Add(new ErrorMessage(pSystemKey, pMessage));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddError(ErrorMessage pError)
        {
            try
            {
                if (pError == null)
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "pError"));

                AddError(pError.SystemKey, pError.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddErrors(IEnumerable<ErrorMessage> pErrors)
        {
            try
            {
                if (pErrors == null)
                    throw new ArgumentNullException(string.Format("Argument null ({0})", "pErrors"));

                foreach (var _error in pErrors)
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

        public void AddException(Exception pException)
        {
            if (pException.GetType() == typeof(InvalidOperationException))
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, "InvalidOperationException", pException.Message);
            else if (pException.GetType() == typeof(ArgumentException))
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, "ArgumentException", pException.Message);
            else if (pException.GetType() == typeof(ArgumentNullException))
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, "ArgumentNullException", pException.Message);
            else if (pException.GetType() == typeof(ArgumentOutOfRangeException))
                AddBrokenRule(BrokenRuleMessageTypes.Impediment, "ArgumentOutOfRangeException", pException.Message);
            else
                AddError("Exception", string.Format("Erro desconhecido. (ExceptionType: {0} - Message: {1})", pException.GetType().ToString(), pException.Message));
        }

        private static string CreateInvalidPropertyBrokenRuleMessage(string pPropertyName)
        {
            return string.Format("A propriedade \"{0}\" está inválida", pPropertyName);
        }

        private static string CreateInvalidPropertyBrokenRuleSystemKey(string pPropertyName)
        {
            return string.Format("{0}Invalid", pPropertyName);
        }

        private static string CreateRequiredPropertyBrokenRuleMessage(string pPropertyName)
        {
            return string.Format("A propriedade \"{0}\" é requerida", pPropertyName);
        }

        private static string CreateRequiredPropertyBrokenRuleSystemKey(string pPropertyName)
        {
            return string.Format("{0}Required", pPropertyName);
        }

        protected bool HasInvalidPropertyBrokenRule(string pPropertyName)
        {
            return GetBrokenRule(CreateInvalidPropertyBrokenRuleSystemKey(pPropertyName)) != null;
        }

        protected bool HasRequiredPropertyBrokenRule(string pPropertyName)
        {
            return GetBrokenRule(CreateRequiredPropertyBrokenRuleSystemKey(pPropertyName)) != null;
        }

        public void CopyMessages(MessagesContainer pArgs)
        {
            if (pArgs != null)
            {
                AddBrokenRules(pArgs.BrokenRules);
                AddErrors(pArgs.Errors);
            }
        }
    }
}