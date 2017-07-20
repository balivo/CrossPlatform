﻿using System;
using System.Threading.Tasks;

namespace CrossPlatform.Services
{
    //public abstract class Service
    //{
    //    public virtual async Task<ServiceResult> Call()
    //    {
    //        return await PlatformCall();
    //    }

    //    protected internal abstract Task<ServiceResult> PlatformCall();
    //}

    public abstract class Service<TServiceArgs> where TServiceArgs : ServiceArgs<TServiceArgs>
    {
        public virtual async Task<ServiceResult> Call(TServiceArgs pServiceArgs)
        {
            try
            {
                if (pServiceArgs == null)
                    throw new ArgumentNullException("pServiceArgs");

                return await CallCore(pServiceArgs);
            }
            catch (Exception ex) { return new ServiceResult(ex); }
        }

        protected internal abstract Task<ServiceResult> CallCore(TServiceArgs pServiceArgs);
    }

    public abstract class Service<TServiceResult, TServiceArgs>
        where TServiceResult : ServiceResult
        where TServiceArgs : ServiceArgs<TServiceArgs>
    {
        public virtual async Task<TServiceResult> Call(TServiceArgs pServiceArgs)
        {
            return await CallCore(pServiceArgs);
        }

        protected internal abstract Task<TServiceResult> CallCore(TServiceArgs pServiceArgs);
    }
}