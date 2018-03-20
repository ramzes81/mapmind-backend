using MapMind.Service.Result;

namespace MapMind.Service.Account.Result
{
    public class EmailConfirmationResult : ServiceResult<EmailConfirmationErrorResultCodes>
    {
        public EmailConfirmationResult()
        {
        }

        public EmailConfirmationResult(EmailConfirmationErrorResultCodes code) : base(code)
        {
        }
    }

    public enum EmailConfirmationErrorResultCodes
    {
        EmailAlreadyConfirmed,
        InvalidToken,
        UserNotFound
    }
}