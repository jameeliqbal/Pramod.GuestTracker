using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pramod.GuestTracker.WhatsAppMessenger
{
    public class WAMessage
    {
        public string messaging_product { get;   } = "whatsapp";
        public string recipient_type { get;    } = "individual";
        public string to { get; set; }
        public string type { get; set; } 
        public Template template { get; set; } = new Template();
    }

    public class Template
    {
        public string name { get; set; }
        public Language language { get; set; }
        public List<WAComponent> components { get; set; } = new List<WAComponent>();
    }

    public class Language
    {
        public string code { get; set; }
    }

    public class WAComponent
    {
        public string type { get; set; }
        public List<WAParameter> parameters { get; set; } = new List<WAParameter>();
    }

    public class WAParameter
    {
        public string type { get; set; }
        public Image image { get; set; } = null;
        public dynamic document { get; set; }
        public string text { get; set; } = null;
        public Currency currency { get; set; } = null;
        public Date_Time date_time { get; set; } = new Date_Time();
    }

    public class Image
    {
        public string id { get; set; }
    }

    public abstract class WADocument
    {
        public string caption { get; set; }
        public string filename { get; set; }
    }

    public class WADocumentLink : WADocument
    {
        public string link { get; set; }

    }

    public class WADocumentMedia : WADocument
    {
        public string id { get; set; }
    }
    public class Currency
    {
        public string fallback_value { get; set; }
        public string code { get; set; }
        public int amount_1000 { get; set; }
    }

    public class Date_Time
    {
        string[] months;
        DateTime currentTime;
        string monthName = "";
        public Date_Time()
        {
            months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "Octorber", "November", "December" };
            currentTime = DateTime.Now;
            monthName = months[currentTime.Month - 1];
            fallback_value = $"{monthName} {currentTime.Day}, {currentTime.Year}";
        }
        public string fallback_value { get; set; }
    }


  


    #region ENUM
    public enum WAMessageTypes
    {
        [Description("audio")]
        audio,// for audio messages.

        [Description("contacts")]
        contacts,// for contact messages.

        [Description("document")]
        document,//for document messages.

        [Description("image")]
        image,// for image messages.

        [Description("interactive")]
        interactive,// for list and reply button messages.

        [Description("location")]
        location,// for location messages.

        [Description("sticker")]
        sticker,// for sticker messages.

        [Description("template")]
        template,//for template messages. Text and media (images and documents) message templates are supported.

        [Description("text")]
        text,// for text messages.

    }

    public enum WAParameterTypes
    {
        [Description("Currency")]
        currency,

        [Description("date_time")]
        date_time,

        [Description("document")]
        document,

        [Description("image")]
        image,

        [Description("text")]
        text,

        [Description("video")]
        video
    }

    public enum WAComponentTypes
    {
        [Description("header")]
        header,

        [Description("body")]
        body,

        [Description("button")]
        button
    }
    #endregion

    #region CONSTANTS
    public   class MediaTypes
    {
        public   const string PDF= "application/pdf";

    }
    #endregion
}
