using IPOTEKA.UA.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Models
{
    public class ProductBank
    {
        [Key]
        [HiddenInput]
        public int ProductBankID { get; set; }

        [Display(Name = "Назва продукту")]
        public string Name { get; set; }

        [Display(Name = "% ставка")]
        public decimal Rate { get; set; }

        [Display(Name = "Комісія за видачу, %")]
        public decimal Commission { get; set; }

        [Display(Name = "Максимальна сума кредиту, грн")]
        public decimal MaxSumCred { get; set; }

        [Display(Name = "Мінімальний термін кредитування")]
        public int MinTermCred { get; set; }

        [Display(Name = "Максимальний термін кредитування")]
        public int MaxTermCred { get; set; }

        public virtual Bank RelBank { get; set; }
        public Int32 RelProduct { get; set; }
    }
}