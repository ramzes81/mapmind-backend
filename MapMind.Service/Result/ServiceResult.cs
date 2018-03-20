using System;

namespace MapMind.Service.Result
{
    public abstract class ServiceResult<TCode>
    {
        private readonly TCode _code;

        protected ServiceResult()
        {
        }

        protected ServiceResult(TCode code)
        {
            _code = code;
            Success = false;
        }

        public TCode ResultCode
        {
            get
            {
                if (Success) throw new InvalidOperationException();
                return _code;
            }
        }

        public bool Success { get; } = true;
    }
}