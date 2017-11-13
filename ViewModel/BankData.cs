using IPOTEKA.UA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPOTEKA.UA.ViewModel
{
    public class BankData
    {
        //public IEnumerable<Bank> Banks { get; set; }
        public Bank Bank { get; set; }
        public List<Product> Products { get; set; }
    }
}