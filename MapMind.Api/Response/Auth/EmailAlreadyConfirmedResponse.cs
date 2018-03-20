namespace MapMind.Api.Response.Auth
{
    public class EmailAlreadyConfirmedResponse : BadRequestResponse
    {
        public override string Message => "Email already confirmed!";
    }
}