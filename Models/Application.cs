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
        public int ApplicationId { get; set; }

        [Display(Name = "Дата створення")]
        public DateTime CreateDateTime { get; set; }

        public Int32? CreateUserId { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Прізвище")]
        public string Sname { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        //[RegularExpression(@"\d{13}", ErrorMessage = "Рахунок повинен містити 13 цифр")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "По батькові")]
        public string Patronymic { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Тип продукту")]
        public string ProductType { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "СМС код підтвердження")]
        public string SmsCode { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Сума кредиту")]
        public decimal? CreditSum { get; set; }

        //[Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Термін кредитування (місяців)")]
        public int? Termin { get; set; }

        [Display(Name = "Коментар (за бажанням)")]
        public string Comments { get; set; }

        public string Xml { get; set; }

        public byte[] XmlData { get; set; }

        public IEnumerable<SelectListItem> Dic_product
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "Іпотечне кредитування. Первинний ринок", Text = "Іпотечне кредитування. Первинний ринок" },
                new SelectListItem { Value = "Іпотечне кредитування. Вторинний ринок", Text = "Іпотечне кредитування. Вторинний ринок" },
                new SelectListItem { Value = "Інше", Text = "Інше" }
                };
            }
        }


    }
}