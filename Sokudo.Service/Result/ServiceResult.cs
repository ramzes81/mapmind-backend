using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Service.Result
{
    public abstract class ServiceResult<TCode>
    {
        private readonly TCode _code;

        public ServiceResult() { }

        public ServiceResult(TCode code)
        {
            _code = code;
            Success = false;
        }

        public TCode ResultCode
        {
            get
            {
                if (Success)
                {
                    throw new InvalidOperationException();
                }
                return _code;
            }
        }

        public bool Success { get; private set; } = true;
    }
}
