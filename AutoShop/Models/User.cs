using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]
        public string FNAME { get; set; }
        [Required]
        [MaxLength(20)]
        public string SNAME { get; set; }
        [MaxLength(20)]
        public string TNAME { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string TEL { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMAIL { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PASSWORD { get; set; }
    }
}