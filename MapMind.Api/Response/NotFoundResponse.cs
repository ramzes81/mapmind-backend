namespace MapMind.Api.Response
{
    public class NotFoundResponse<TEntity> : BaseResponse
    {
        public override string Message => $"{nameof(TEntity)} not found.";
    }
}