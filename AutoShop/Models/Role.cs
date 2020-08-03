using AutoShop.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Models
{
    public class Role
    {
        public int ID { get; set; }
        [MaxLength(10)]
        public string TITLE { get; set; }
        public RoleType TYPE { get; set; }
    }
}