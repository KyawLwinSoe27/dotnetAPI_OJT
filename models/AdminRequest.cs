using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PracticeApi.Models
{
    public class AdminRequest 
    {
        [Column("admin_id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("admin_name")]
        [StringLength(50)]
        public String AdminName {get; set;} = string.Empty;

        [Required]
        [Column("email")]
        [StringLength(50)]
        public String Email {get; set;} = string.Empty;

        [Required]
        [Column("login_name")]
        [StringLength(50)]
        public String LoginName {get; set;} = string.Empty;

        [Required]
        [Column("password")]
        [StringLength(50)]
        public String Password {get; set;} = string.Empty;

        [Required]
        [Column("inactive")]
        public bool Inactive {get; set;}

        [NotMapped]
        public string? AdminPhoto {get; set; }

        [Required]
        [Column("admin_level_id")]
        public int AdminLevelId { get; set; }
    }
}