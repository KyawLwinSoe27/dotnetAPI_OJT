using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApi.Models
{
    [Table("tbl_customer_type")]
    public partial class CustomerType : BaseModel
    {
        [Column("customer_type_id")]
        [Key]
        public int Id {get; set;}

        [Column("customer_type_name")]
        [Required]
        [MaxLength(50)]
        public String CustomerTypeName {get; set;} = String.Empty;

        [Column("customer_type_description")]
        [Required]
        [MaxLength(100)]
        public String CustomerTypeDescription {get; set;} = String.Empty;
    }
}