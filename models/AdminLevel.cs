using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApi.Models
{
    [Table("tbl_admin_level")]
    public partial class AdminLevel : BaseModel
    {
        [Column("admin_level_id")]
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        [Required]
        [Column("admin_level_name")]
        public string AdminLevelName { get; set; } = string.Empty;

    }
}