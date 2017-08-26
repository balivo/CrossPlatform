using System;
using System.Threading.Tasks;

namespace CrossPlatform.Services
{
    public abstract class Service<TServiceArgs>
        where TServiceArgs : ServiceArgs<TServiceArgs>
    {
        public virtual Task<ServiceResult> Execute(TServiceArgs pServiceArgs)
        {
            try
            {
                if (pServiceArgs == null)
                    throw new ArgumentNullException("pServiceArgs");

                return ExecuteCore(pServiceArgs);
            }
            catch (Exception ex) { throw ex; }
        }

        protected internal abstract Task<ServiceResult> ExecuteCore(TServiceArgs pServiceArgs);
    }

    public abstract class Service<TServiceResult, TServiceArgs>
        where TServiceResult : ServiceResult
        where TServiceArgs : ServiceArgs<TServiceArgs>
    {
        public virtual Task<TServiceResult> Execute(TServiceArgs pServiceArgs)
        {
            try
            {
                if (pServiceArgs == null)
                    throw new ArgumentNullException("pServiceArgs");

                return ExecuteCore(pServiceArgs);
            }
            catch (Exception ex) { throw ex; }
        }

        protected internal abstract Task<TServiceResult> ExecuteCore(TServiceArgs pServiceArgs);
    }
}