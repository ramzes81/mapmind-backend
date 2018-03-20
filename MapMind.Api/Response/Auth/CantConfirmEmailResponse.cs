namespace MapMind.Api.Response.Auth
{
    public class CantConfirmEmailResponse : ForbiddenResponse
    {
        public override string Message => "Can't confirm email using provided code";
    }
}