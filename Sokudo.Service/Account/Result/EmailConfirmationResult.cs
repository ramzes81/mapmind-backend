using Sokudo.Service.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Service.Account.Result
{
    public class EmailConfirmationResult : ServiceResult<EmailConfirmationErrorResultCodes>
    {
        public EmailConfirmationResult(): base()
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
