using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DailyLocalCode
{
    public static class Atrribute
    {
        static void Main2(string[] args)
        {
            Author author = new Author();
            //author.FirstName = "Joydip";
            author.FirstName = null;
            author.LastName = "";
            //author.LastName = "ssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";
            author.PhoneNumber = "1234567890";
            author.Emial = "joydipkanjilal@yahoo.com";

            ValidationContext context = new ValidationContext(author,null,null);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool valid = Validator.TryValidateObject(author,context,validationResults,true);

            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    Console.WriteLine("{0}",validationResult.ErrorMessage);
                }
            }

            //Console.ReadLine();
        }
    }

    public class Author
    {
        /*[Required(ErrorMessage = "{0} is required")]
        [StringLength(50,MinimumLength = 3,
         ErrorMessage = "First Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]*/
        [IsEmpty(ErrorMessage = "{0} Should not be null or empty.")]
        public string FirstName { get; set; }


        /*[Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 3,
         ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]*/
        [IsEmpty(ErrorMessage = "{0} Should not be null or empty.")]

        public string LastName { get; set; }

        
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }


        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Emial { get; set; }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false,Inherited = false)]
    public class IsEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string;
            return !string.IsNullOrEmpty(inputValue);
        }
    }


    [AttributeUsage(AttributeTargets.All)]
    public class HelpAttribute : System.Attribute
    {
        public readonly string Url;

        public string Topic { get; set; }

        public HelpAttribute(string url)
        {
            Url = url;
        }

        private string topic;
    }

    [Help("Information on the class MyClass")]
    class MyClass
    {

    }

    class Progra2m
    {
        static void Main4(string[] args)
        {
            MemberInfo info = typeof(MyClass);
            object[] attributes = info.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                Console.WriteLine(attributes[i]);
            }

            Console.ReadLine();
        }
    }

}
