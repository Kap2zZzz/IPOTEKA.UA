using IPOTEKA.UA.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Models
{
    public class Application
    {
        [Key]
        [HiddenInput]
        public int ApplicationId { get; set; }

        [HiddenInput]
        public DateTime CreateDateTime { get; set; }

        [HiddenInput]
        public Int32? CreateUserId { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Прізвище")]
        public string Sname { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        //[RegularExpression(@"\d{13}", ErrorMessage = "Рахунок повинен містити 13 цифр")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        //[Display(Name = "По батькові")]
        //public string Patronymic { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email адрес")]
        public string Email { get; set; }

        [Display(Name = "Місто")]
        public string City { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Тип продукту")]
        public string ProductType { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        //[Display(Name = "СМС код підтвердження")]
        //public string SmsCode { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Сума кредиту")]
        public decimal? CreditSum { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Термін кредитування (місяців)")]
        public int? Termin { get; set; }

        [Display(Name = "Схема погашення")]
        public string Schema { get; set; }

        [Display(Name = "Коментар (за бажанням)")]
        public string Comments { get; set; }

        [Display(Name = "Згода")]
        public bool Zgoda { get; set; }

        public string Xml { get; set; }

        public byte[] XmlData { get; set; }

        //public Dictionary<int, string> dicProducts;

        //public Dictionary<int, string> dicSchems;

        //public Application()
        //{
        //    dicProducts = MainHelp.dicProducts();
        //    dicSchems = MainHelp.dicSchems();
        //}

        //public IEnumerable<SelectListItem> dicProducts = MainHelp.dicProducts();

        //public IEnumerable<SelectListItem> dicSchems = MainHelp.dicSchems();
    }
}