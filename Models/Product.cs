using IPOTEKA.UA.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Models
{
    public class Product
    {
        [Key]
        [HiddenInput]
        public int ProductID { get; set; }

        [Display(Name = "Назва продукту")]
        public Dictionary<int, string> Name { get; set; }

        [Display(Name = "% ставка")]
        public decimal Rate { get; set; }

        [Display(Name = "Комісія за видачу, %")]
        public decimal Commission { get; set; }

        [Display(Name = "Максимальна сума кредиту, грн")]
        public decimal MaxSumCred { get; set; }

        [Display(Name = "Максимальний термін кредитування")]
        public int MaxTermCred { get; set; }

        public virtual Bank Rel { get; set; }

        public Product()
        {
            Name = MainHelp.dicProducts();
        }
    }
}