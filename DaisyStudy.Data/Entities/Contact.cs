using System;
namespace DaisyStudy.Data.Entities
{
    public class Contact
    {
        public int Contact_ID { set; get; }
        public string CustomerName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string Message { set; get; }
        public Status Status { set; get; }
    }
}