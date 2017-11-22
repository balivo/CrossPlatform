using System;
using System.Threading.Tasks;

namespace CrossPlatform.Services
{
    public abstract class Service<TServiceArgs> where TServiceArgs : ServiceArgs
    {
        public virtual Task<ServiceResult> Execute(TServiceArgs serviceArgs)
        {
            try
            {
                if (serviceArgs == null)
                    throw new ArgumentNullException("serviceArgs");

                return ExecuteCore(serviceArgs);
            }
            catch (Exception ex) { throw ex; }
        }

        protected internal abstract Task<ServiceResult> ExecuteCore(TServiceArgs serviceArgs);
    }

    public abstract class Service<TServiceResult, TServiceArgs>
        where TServiceResult : ServiceResult
        where TServiceArgs : ServiceArgs
    {
        public virtual Task<TServiceResult> Execute(TServiceArgs serviceArgs)
        {
            try
            {
                if (serviceArgs == null)
                    throw new ArgumentNullException("serviceArgs");

                return ExecuteCore(serviceArgs);
            }
            catch (Exception ex) { throw ex; }
        }

        protected internal abstract Task<TServiceResult> ExecuteCore(TServiceArgs serviceArgs);
    }
}