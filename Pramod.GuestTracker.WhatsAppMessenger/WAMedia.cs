using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pramod.GuestTracker.WhatsAppMessenger
{
    public class WAMedia
    {
        public string Id { get; set; }
    }

    public class WAUploadMediaParameter
    {
        public string file { get; set; }
        public string type { get; set; }
        public string messaging_product { get; } = "whatsapp";
    }

    public class MediaUploadResponse 
    {
        public string id { get; set; }
        public Error  error { get; set; }
    }
}
