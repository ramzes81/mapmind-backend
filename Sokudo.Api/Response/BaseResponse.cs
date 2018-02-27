using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response
{
    public abstract class BaseResponse
    {
        public abstract string Message { get; }
    }
}
