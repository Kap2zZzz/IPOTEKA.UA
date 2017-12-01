using IPOTEKA.UA.Models;
using IPOTEKA.UA.Repostory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Code
{
    public static class MainHelp
    {
        public static string CreateXML(Application lm)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Application));

            //System.IO.StreamWriter file = new System.IO.StreamWriter(@"d:\SerializationOverview.xml");
            //writer.Serialize(file, lm);

            using (StringWriter textWriter = new StringWriter())
            {
                writer.Serialize(textWriter, lm);
                return textWriter.ToString();
            }
        }

        public static Dictionary<int, string> dicSchems()
        {
            return new Dictionary<int, string>()
            {
                { 1, "Класична"},
                { 2, "Ануїтетна"}
            };
        }

        //public static Dictionary<int, string> dicProducts()
        //{
        //    return new Dictionary<int, string>()
        //    {
        //        { 1, "Іпотечне кредитування. Первинний ринок"},
        //        { 2, "Іпотечне кредитування. Вторинний ринок"},
        //        { 3, "Поточні потреби"},
        //        { 4, "Рефінансування"},
        //        { 5, "Придбання земельних ділянок"}
        //    };
        //}

        public static List<KeyValuePair<string, string>> IsValidEtap2(Application lm)
        {
            List<KeyValuePair<string, string>> errorList = new List<KeyValuePair<string, string>>();

            if ((lm.Sname == string.Empty) || (lm.Sname == null))
            {
                errorList.Add(new KeyValuePair<string, string>("Sname", "Поле: [Прізвище] обовязкове для заповнення!"));
            }

            if ((lm.Name == string.Empty) || (lm.Name == null))
            {
                errorList.Add(new KeyValuePair<string, string>("Name", "Поле: [Ім'я] обовязкове для заповнення!"));
            }

            if ((lm.PhoneNumber == string.Empty) || (lm.PhoneNumber == null))
            {
                errorList.Add(new KeyValuePair<string, string>("PhoneNumber", "Поле: [Номер телефону] обовязкове для заповнення!"));
            }

            //if ((lm.Email == string.Empty) || (lm.Email == null))
            //{
            //    errorList.Add(new KeyValuePair<string, string>("Email", "Поле: [Номер телефону] обовязкове для заповнення!"));
            //}

            if (lm.Zgoda == false)
            {
                errorList.Add(new KeyValuePair<string, string>("Zgoda", "Поле: [Згода] обовязкове для заповнення!"));
            }

            return errorList;
        }

        public static List<KeyValuePair<string, string>> IsValidEtap1(Application lm)
        {
            List<KeyValuePair<string, string>> errorList = new List<KeyValuePair<string, string>>();

            if (lm.ProductType == null)
            {
                errorList.Add(new KeyValuePair<string, string>("ProductType", "Поле: [Тип продукту] обовязкове для заповнення!"));
            }

            if (lm.CreditSum == null)
            {
                errorList.Add(new KeyValuePair<string, string>("CreditSum", "Поле: [Сума кредиту] обовязкове для заповнення!"));
            }

            if (lm.Termin == null)
            {
                errorList.Add(new KeyValuePair<string, string>("Termin", "Поле: [Термін кредитування] обовязкове для заповнення!"));
            }

            if (lm.Schema == null)
            {
                errorList.Add(new KeyValuePair<string, string>("Schema", "Поле: [Схема погашення] обовязкове для заповнення!"));
            }

            if ((lm.City == string.Empty) || (lm.City == null))
            {
                errorList.Add(new KeyValuePair<string, string>("City", "Поле: [Місто] обовязкове для заповнення!"));
            }

            return errorList;
        }

        public static void CreateUserAdmin()
        {
            MyDbContext context = new MyDbContext();

            if (context.Users.Count() == 0)
            {
                User u = new User();
                u.CreateDateTime = System.DateTime.Now;
                u.PIB = "Admin";
                u.Login = "Admin";
                u.Password = "P@ssword";
                u.Role = "Admin";
                u.Email = "Admin@com.ua";

                context.Users.Add(u);
                context.SaveChanges();
                context.Dispose();
            }
        }

        public static bool AddUser(User u)
        {
            MyDbContext context = new MyDbContext();
            context.Users.Add(u);
            context.SaveChanges();
            context.Dispose();
            return true;
        }
    }
}