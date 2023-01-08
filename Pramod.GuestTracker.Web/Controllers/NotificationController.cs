using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pramod.GuestTracker.Web.Data;
using Pramod.GuestTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
//using System.Web.Mvc;

namespace Pramod.GuestTracker.Web.Controllers
{
    public class NotificationController : ApiController
    {
        private const string HUB_VERIFICATION_TOKEN = "06ce34b0-167a-4b26-afbf-2afaf43627e9";
        private GuestTrackerContext db = new GuestTrackerContext();

        /// <summary>
        /// Verification request from WhatsApp Cloud API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult en()
        {

 
            var queryParams = Request.RequestUri.Query.Split('?')[1].Split('&');
      
 
            if (queryParams[0].Split('=')[1] != "subscribe")
                return BadRequest();

            if (queryParams[2].Split('=')[1] != HUB_VERIFICATION_TOKEN)
                return BadRequest();

            return Ok(long.Parse(queryParams[1].Split('=')[1]));
             
        }

         

        /// <summary>
        /// Event Notification from Whatsapp CLoud API
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> en( WhatsAppWebhookModel notification)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid Data");
            foreach (var entry in notification.entry)
            {
                foreach (var change in entry.changes)
                {
                    var contact = change.value.contacts?[0];
                    var metadata = change.value.metadata;

                    if  (change.value.errors !=null)
                    {
                        try
                        {


                            foreach (var status in change.value.statuses)
                            {
                                System.Diagnostics.Debug.WriteLine(status);
                                var wastatus = new WAMessageStatus
                                {
                                    Id = Guid.NewGuid(),
                                    ConversationId = status.conversation.id,
                                    ConversationOrigin = status.conversation.origin.type,
                                    PhoneNumber = status.recipient_id,
                                    Timestamp = status.timestamp,
                                    Type = status.status
                                };
                                db.WhatsappMessageStatus.Add(wastatus);
                            }
                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                            System.Diagnostics.Debug.WriteLine(ex.Message + "----" + ex.InnerException?.Message);

                        }
                    }
                    else if (change.value.messages != null)
                    {
                        foreach (var message in change.value.messages)
                        {
                            switch (message.type)
                            {
                                case MessageType.audio:
                                    break;
                                case MessageType.button:
                                    break;
                                case MessageType.document:
                                    break;
                                case MessageType.text:
                                    var wastatus = new WAMessageStatus
                                    {
                                        Id = Guid.NewGuid(),
                                        PhoneNumber = message.from,
                                        Remarks = message.text.body,
                                        Timestamp = message.timestamp,
                                        Type = "message"
                                    };
                                    db.WhatsappMessageStatus.Add(wastatus);
                                    await db.SaveChangesAsync();

                                    break;
                                case MessageType.image:
                                    break;
                                case MessageType.interactive:
                                    break;
                                case MessageType.order:
                                    break;
                                case MessageType.sticker:
                                    break;
                                case MessageType.system:
                                    break;
                                case MessageType.unknown:
                                    break;
                                case MessageType.video:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (change.value.statuses !=null)
                    {
                        try
                        {


                            foreach (var status in change.value.statuses)
                            {
                                 
                                var wastatus = new WAMessageStatus
                                { Id = Guid.NewGuid(),
                                    PhoneNumber = status.recipient_id,
                                    Timestamp = status.timestamp,
                                    Type = status.status
                                };

                                if (status.errors is null)
                                {
                                    wastatus.ConversationId = status.conversation?.id;
                                    wastatus.ConversationOrigin = status.conversation?.origin.type;

                                }
                                else
                                {
                                    foreach (var err in status.errors)
                                    {
                                        wastatus.Remarks += $"[{err.code}] {err.title} {Environment.NewLine} {err.details}";
                                        wastatus.Remarks += Environment.NewLine + Environment.NewLine;
                                    }
                                }
                                db.WhatsappMessageStatus.Add(wastatus);
                            }
                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                            System.Diagnostics.Debug.WriteLine(ex.Message + "----" + ex.InnerException?.Message);

                        }
                    }
                }
            }

            return Ok();
        }
    }

    public class VerificationRequest
    {
        [JsonProperty(PropertyName = "hub.mode")]
        public string HubMode { get; set; }
        [JsonProperty(PropertyName = "hub.challenge")]
        public int HubChallenge { get; set; }
        [JsonProperty(PropertyName = "hub.verify_token")]
        public string HubVerifyToken { get; set; }

    }
}
