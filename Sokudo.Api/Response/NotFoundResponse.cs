using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokudo.Api.Response
{
    public class NotFoundResponse<TEntity>: BaseResponse
    {
        public override string Message => $"{nameof(TEntity)} not found.";
    }
}
