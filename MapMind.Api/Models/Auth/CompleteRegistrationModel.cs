﻿namespace MapMind.Api.Models.Auth
{
    public class CompleteRegistrationModel
    {
        public string UserId { get; set; }
        public string ConfirmationCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}