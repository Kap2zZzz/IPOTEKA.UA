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
        public int ProductID { get; set; }

        [Display(Name = "Назва продукту")]
        public string Name { get; set; }

        [Display(Name = "% ставка")]
        public decimal Rate { get; set; }

        [Display(Name = "Комісія за видачу, %")]
        public decimal Commission { get; set; }

        [Display(Name = "Максимальна сума кредиту, грн")]
        public decimal MaxSumCred { get; set; }

        [Display(Name = "Максимальний термін кредитування")]
        public int MaxTermCred { get; set; }

        public virtual Bank Rel { get; set; }

        public IEnumerable<SelectListItem> Dic_product
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "Первинний ринок", Text = "Іпотечне кредитування. Первинний ринок" },
                new SelectListItem { Value = "Вторинний ринок", Text = "Іпотечне кредитування. Вторинний ринок" },
                new SelectListItem { Value = "Поточні потреби", Text = "Поточні потреби" },
                new SelectListItem { Value = "Рефінансування", Text = "Рефінансування" },
                new SelectListItem { Value = "Придбання земельних ділянок", Text = "Придбання земельних ділянок" },
                };
            }
        }
    }
}