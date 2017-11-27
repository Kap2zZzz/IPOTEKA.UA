using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IPOTEKA.UA.Models
{
    public class Bank
    {
        [Key]
        public int BankID { get; set; }

        //[Display(Name = "Дата створення")]
        //public DateTime CreateDateTime { get; set; }

        //public Int32? CreateUserId { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Назва Банку")]
        public string Name { get; set; }

        public virtual List<ProductBank> Products { get; set; }
    }
}