using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pramod.GuestTracker.WhatsAppMessenger
{
    public class MessageResponse
    {
        public string messaging_product { get; set; }
        public List<Contact> contacts { get; set; }
        public List<Message> messages { get; set; }
        public Error error { get; set; }
    }

    public class Contact
    {
        public string input { get; set; }
        public string wa_id { get; set; }
    }

    public class Message
    {
        public string id { get; set; }
    }


    public class Error
    {
        public string message { get; set; }
        public string type { get; set; }
        public int code { get; set; }
        public int error_subcode { get; set; }
        public string fbtrace_id { get; set; }
        public ErrorData error_data { get; set; }
    }


    public class ErrorData
    {
        public string messaging_product { get; set; }
        public string details { get; set; }
    }
}
