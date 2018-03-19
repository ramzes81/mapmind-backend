using System;
using System.Collections.Generic;
using System.Text;

namespace Sokudo.Email.Options
{
    public class EmailAuthOptions
    {
        public string SmtpServer { get; set; }
        public int SmtpServerPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
    }
}
