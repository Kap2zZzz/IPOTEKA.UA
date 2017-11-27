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
        public string Name { get; set; }
    }
}