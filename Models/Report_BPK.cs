using ReportServerMVC.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportServerMVC.Models
{
    public class Report_BPK : IReport
    {
        public int ReportID
        {
            get { return 105061; }
        }
        
        //= 105061;    

        [Required(ErrorMessage = "Поле не вибрано!")]
        [Display(Name = "Договір підписує (0,1,2,3,4)")]
        public string DogowirPidpysuje { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [RegularExpression(@"\d{13}", ErrorMessage = "Рахунок повинен містити 13 цифр")]
        [Display(Name = "Рахунок")]
        public string accountno { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Код валюти")]
        //[RegularExpression(@"\[980]", ErrorMessage= "Некоректна валюта")]
        public string currencyid { get; set; }

        [Required(ErrorMessage = "Поле не вибрано!")]
        [Display(Name = "Тип картки")]
        public string CardType { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Мобільний телефон Клієнта")]
        public string MobPhone { get; set; }

        [Required(ErrorMessage = "Поле не вибрано!")]
        [Display(Name = "Терміново")]
        public string Terminowo { get; set; }

        [Required(ErrorMessage = "Поле не вибрано!")]
        [Display(Name = "Mobinform")]
        public string Mobinform { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Код доступу Mobinform")]
        public string KodMobinform { get; set; }

        [Required(ErrorMessage = "Поле не вибрано!")]
        [Display(Name = "KredoDirect")]
        public string Kredodirect { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не вибрано!")]
        [Display(Name = "Договір овердрафту та рішення підписує (0,1,2,3,4)")]
        public string DogovirOver { get; set; }

        [Required(ErrorMessage = "Поле не вибрано!")]
        [Display(Name = "Видача БПК ч/з роботодавця")]
        public string BPK_Salary { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Назва роботодавця (ЮО)")]
        public string Name_YO { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Display(Name = "Код ЄДРПОУ")]
        public string EDRPOU { get; set; }

        public IEnumerable<SelectListItem> DDL_Tak_Ni
        {
            get
            {
                return new[]
            {
                new SelectListItem { Value = "0", Text = "Так" },
                new SelectListItem { Value = "1", Text = "Ні" },
            };
            }
        }

        public IEnumerable<SelectListItem> DDL_number
        {
            get
            {
                return new[]
            {
                new SelectListItem { Value = "0", Text = "0" },
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" }
            };
            }
        }

        public IEnumerable<SelectListItem> DDL_cards
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "0", Text = "MC World Debit Instant" },
                new SelectListItem { Value = "1", Text = "MC World Debit" },
                new SelectListItem { Value = "2", Text = "MC World Debit Standart" },
                new SelectListItem { Value = "3", Text = "MC World Debit Gold" },
                new SelectListItem { Value = "4", Text = "MC World Elite" },
                new SelectListItem { Value = "5", Text = "Debit MC Platinum" },
                new SelectListItem { Value = "6", Text = "Visa Electron" },
                new SelectListItem { Value = "7", Text = "Visa Classic" },
                new SelectListItem { Value = "8", Text = "Visa Gold" },
                };
            }
        }

        //int _umowaKredytowa = 105061; 
        //БПК
        /*
         
          listOfParameters.Add(new KeyValuePair<string, string>("Договір підписує (0,1,2,3,4)", "0"));
            listOfParameters.Add(new KeyValuePair<string, string>("accountno", "2625904368632")); //6327864 //2625305601855
            listOfParameters.Add(new KeyValuePair<string, string>("currencyid", "980"));
            listOfParameters.Add(new KeyValuePair<string, string>("ТИП_КАРТКИ", "0")); //Потрібно передавати індех з списку, 0,1,2,3,4,5,6,7,8
            listOfParameters.Add(new KeyValuePair<string, string>("Мобільний телефон Клієнта", "0632397113"));
            listOfParameters.Add(new KeyValuePair<string, string>("Терміново", "1")); //ПОМИЛКА В ПАРАМЕТРІ
            listOfParameters.Add(new KeyValuePair<string, string>("Mobinform", "0")); //По замовчуванню //ПОМИЛКА В ПАРАМЕТРІ
            listOfParameters.Add(new KeyValuePair<string, string>("КредоДайрект", "1")); //По замовчуванню //ПОМИЛКА В ПАРАМЕТРІ
            listOfParameters.Add(new KeyValuePair<string, string>("Код доступу Mobinform", "1")); //По замовчуванню
            listOfParameters.Add(new KeyValuePair<string, string>("Пароль", "PASSW0RD"));
            listOfParameters.Add(new KeyValuePair<string, string>("Договір овердрафту та рішення підписує (0,1,2,3,4)", "0"));
            listOfParameters.Add(new KeyValuePair<string, string>("Видача БПК ч/з роботодавця", "1"));
            listOfParameters.Add(new KeyValuePair<string, string>("Назва роботодавця (ЮО)", "asdadas"));
            listOfParameters.Add(new KeyValuePair<string, string>("Код ЄДРПОУ", "12345678"));
         * */
    }
}