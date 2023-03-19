using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeApi.Models
{
    [Table("tbl_customers")]
    public partial class Customer
    {
        [Column("customer_id")]
        [Key]
        public int Id {get; set;}

        [Column("customer_name")]
        [Required]
        [MaxLength(50)]
        public String CustomerName {get; set;} = String.Empty;

        [Column("customer_register_date")]
        [Required]
        public DateTime CustomerRegisterDate { get; set; }

        [Column("customer_address")]
        [Required]
        [MaxLength(100)]
        public String CustomerAddress {get; set;} = String.Empty;

        [Column("customer_type_id")]
        [Required]
        public int CustomerTypeId { get; set; }

        [ForeignKey ("CustomerTypeId")]
        public CustomerType? customerType { get; set; }
    }
}