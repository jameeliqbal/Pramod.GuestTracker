using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pramod.GuestTracker.Web.Models
{
    /// <summary>
    /// REF: https://developers.facebook.com/docs/whatsapp/cloud-api/webhooks/components
    /// </summary>
    public   class WhatsAppWebhookModel
    {
        public string Object { get; set; }
        public List<Entry> entry { get; set; }
    }



    public class Entry
    {
        public string id { get; set; }
        public List<Change> changes { get; set; }
    }

    public class Change
    {
        public Value value { get; set; }
        public string field { get; set; }
    }

    public class Value
    {
        public string messaging_product { get; set; }
        public Metadata metadata { get; set; }
        public List<contact> contacts { get; set; }
        public List<Error> errors { get; set; }
        public List<Message> messages { get; set; }
        public List<Status> statuses { get; set; }
    }

    public class Metadata
    {
        public string display_phone_number { get; set; }
        public string phone_number_id { get; set; }
    }

    public class contact
    {
        public Profile profile { get; set; }
        public string wa_id { get; set; }
    }


    public class Profile
    {
        public string Name { get; set; }
    }


    public class Message
    {
        public MessageType type { get; set; }
        public string from { get; set; }
        public string id { get; set; }
        public string timestamp { get; set; }
        public TextMessage text { get; set; }
        public ReactionMessage reaction { get; set; }
        public ImageMessage image { get; set; }
        public DocumentMessage document { get; set; }
        public AudioMessage audio { get; set; }
        public VideoMessage video { get; set; }
        public StickerMessage sticker { get; set; }
        public LocationMessage location { get; set; }

        public List<Error> errors { get; set; }

    }

    public class TextMessage
    {
        //text message fields 
        public string body { get; set; }
        //text message fields 
    }

    public class ReactionMessage
    {
        public string message_id { get; set; }
        public string emoji { get; set; }
    }


    public abstract class MediaMessageBase
    {
        public string id { get; set; }
        public string mime_type { get; set; }

    }

    public abstract class SecureMediaMessageBase : MediaMessageBase
    {
        public string filename { get; set; }
        public string caption { get; set; }
        public string sha256 { get; set; }
    }

    public class ImageMessage : SecureMediaMessageBase
    {
    }

    public class DocumentMessage : SecureMediaMessageBase
    {
    }

    public class AudioMessage : SecureMediaMessageBase
    {
    }

    public class VideoMessage : SecureMediaMessageBase
    {
    }

    public class StickerMessage : SecureMediaMessageBase
    {
    }

    public class LocationMessage
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }




   


    public class Status
    {
        public string id { get; set; }
        public string recipient_id { get; set; }
        public string status { get; set; }
        public string timestamp { get; set; }
        public Conversation conversation { get; set; }
        public Pricing pricing { get; set; }
        public List<Error> errors { get; set; }
    }

    public class Conversation
    {
        public string id { get; set; }
        public string expiration_timestamp { get; set; }
        public Origin origin { get; set; }
    }

    public class Origin
    {
        public string type { get; set; }
    }

    public class Pricing
    {
        public string pricing_model { get; set; }
        public bool billable { get; set; }
        public string category { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string details { get; set; }
        public string title { get; set; }
    }


    public enum MessageType
    {
        audio,
        button,
        document,
        text,
        image,
        interactive,
        order,
        sticker,
        system, // for customer number change messages
        unknown,
        video
    }

    public enum OriginType
    {
        user_initiated,
        business_initated,
        referral_conversion
    }
}