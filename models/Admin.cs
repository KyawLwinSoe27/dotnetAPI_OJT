using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PracticeApi.Models
{
    [Table("tbl_admin")]
    public class Admin : BaseModel
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
        [StringLength(255)]
        public String Password {get; set;} = string.Empty;

        [Column("salt")]
        [StringLength(255)]
        public String Salt {get; set;} = string.Empty;

        [Required]
        [Column("inactive")]
        public bool Inactive {get; set;}

        [Column("admin_level_id")]
        public int AdminLevelId {get; set;}

        [ForeignKey ("AdminLevelId")]
        public AdminLevel? adminLevel { get; set; }
    }
}