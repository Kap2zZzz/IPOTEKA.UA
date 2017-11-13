using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "Дата реєстрації")]
        public DateTime CreateDateTime { get; set; }

        [Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Прізвище, Імя, По-батькові")]
        public string PIB { get; set; }

        [Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Логін")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Поле обовязкове для заповнення!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name="Запамятати мене")]
        public bool Remember { get; set; }

        public IEnumerable<SelectListItem> Dic_role
        {
            get
            {
                return new[]
                {
                new SelectListItem { Value = "Admin", Text = "Адміністратор" },
                new SelectListItem { Value = "Agent", Text = "Агент з продажу" }
                };
            }
        }

        public User()
        {
            CreateDateTime = System.DateTime.Now;

        }
    }
}