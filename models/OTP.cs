using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApi.Models
{
    [Table("tbl_ge_otp")]
    public partial class OTP : BaseModel
    {
        [Column("otpid")]
        public int OTPId { get; set; }
        [Column("emailphone")]
        public string EmailPhone { get; set; } = string.Empty;
        [Column("loginname")]
        public string LoginName { get; set; } = string.Empty;
        [Column("passcode")]
        public string Passcode { get; set; } = string.Empty;
        [Column("ipaddress")]
        public string IPAddress { get; set; } = string.Empty;
        [Column("otptoken")]
        public string OTPToken { get; set; } = string.Empty;
        [Column("senddatetime")]
        public DateTime SendDateTime { get; set; }
        [Column("failcount")]
        public int FailCount { get; set; }
        [Column("retrycount")]
        public int RetryCount { get; set; }
        [Column("lastmodifieddate")]
        public DateTime LastModifiedDate { get; set; }
        [Column("createddate")]
        public DateTime CreatedDate { get; set; }
    }
}