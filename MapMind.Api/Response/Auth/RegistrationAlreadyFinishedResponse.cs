namespace MapMind.Api.Response.Auth
{
    public class RegistrationAlreadyFinishedResponse : BadRequestResponse
    {
        public override string Message => "Registration already finished!";
    }
}