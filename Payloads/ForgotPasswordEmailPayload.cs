using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApi.Payloads
{
    public partial class ForgotPasswordEmailPayload
    {        
        public string LoginName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}