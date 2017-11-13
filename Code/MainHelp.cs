using IPOTEKA.UA.Models;
using IPOTEKA.UA.Repostory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

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

        //public static bool IsValidEtap1(LoanModel lm)
        //{
        //    if ((lm.Patronymic == string.Empty) || (lm.Patronymic == null))
        //    {
        //        return false;
        //    }
        //    else if ((lm.Name == string.Empty) || (lm.Name == null))
        //    {
        //        return false;
        //    }
        //    else if ((lm.Sname == string.Empty) || (lm.Sname == null))
        //    {
        //        return false;
        //    }
        //    else if ((lm.PhoneNumber == string.Empty) || (lm.PhoneNumber == null))
        //    {
        //        return false;
        //    }
        //    //else if ((lm.ProductType == string.Empty) || (lm.ProductType == null))
        //    //{
        //    //    return false;
        //    //}
        //    else return true;
        //}

        public static List<KeyValuePair<string, string>> IsValidEtap1(Application lm)
        {
            List<KeyValuePair<string, string>> errorList = new List<KeyValuePair<string, string>>();

            if ((lm.Patronymic == string.Empty) || (lm.Patronymic == null))
            {
                errorList.Add(new KeyValuePair<string, string>("Patronymic", "Поле: [По батькові] обовязкове для заповнення!"));
            }
            
            if ((lm.Name == string.Empty) || (lm.Name == null))
            {
                errorList.Add(new KeyValuePair<string, string>("Name", "Поле: [Ім'я] обовязкове для заповнення!"));
            }
            
            if ((lm.Sname == string.Empty) || (lm.Sname == null))
            {
                errorList.Add(new KeyValuePair<string, string>("Sname", "Поле: [Прізвище] обовязкове для заповнення!"));
            }
            
            if ((lm.PhoneNumber == string.Empty) || (lm.PhoneNumber == null))
            {
                errorList.Add(new KeyValuePair<string, string>("PhoneNumber", "Поле: [Номер телефону] обовязкове для заповнення!"));
            }

            return errorList;
        }

        public static List<KeyValuePair<string, string>> IsValidEtap2(Application lm)
        {
            List<KeyValuePair<string, string>> errorList = new List<KeyValuePair<string, string>>();

            if ((lm.SmsCode == string.Empty) || (lm.SmsCode == null))
            {
                errorList.Add(new KeyValuePair<string, string>("SmsCode", "Поле: [SMS код підтвердження] обовязкове для заповнення!"));
            }
            
            if ((lm.ProductType == string.Empty) || (lm.ProductType == null))
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

        //public FileResult DownloadFile(LoanModel lm)
        //{
        //    return File();
        //    //return File(bytes, contentType, fileName);
        //}
    }
}