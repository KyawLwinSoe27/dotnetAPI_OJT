using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PracticeApi.Models;

namespace PracticeApi.Payloads
{
    public class ChangePasswordPayload
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        
    }
}