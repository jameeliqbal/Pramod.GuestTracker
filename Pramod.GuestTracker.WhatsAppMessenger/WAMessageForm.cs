using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pramod.GuestTracker.WhatsAppMessenger
{
    public partial class WAMessageForm : Form
    {
        private const string token = ""; //GTWAMessenger

        public WAMessageForm()
        {
            InitializeComponent();
        }

        private async void btnSendInvites_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var filePath = txtGuestList.Text.Trim();
            txtLog.AppendText("Fetching Guest List"+Environment.NewLine);
            var guestList = GetGuestList(filePath);
            await SendInvites(guestList);
            Cursor = Cursors.Default;
            var message = "[ " + DateTime.Now +"]++++ Sending Complete! ++++" +Environment.NewLine;
            txtLog.AppendText(message);
            //MessageBox.Show(message);
            var logDir = Path.GetDirectoryName(filePath);
            var logName = Path.GetFileNameWithoutExtension(filePath) + "_log_" + DateTime.Now.ToString("ddMMyyyy-HHmmss")+".txt";
            var logPath = Path.Combine(logDir, logName);
            File.WriteAllText(logPath, txtLog.Text.Trim());
        }

        private async Task SendInvites(IEnumerable<Guest> guestList)
        {
            var logMessage = string.Empty;

            try
            {
                txtLog.AppendText("Creating Message List..."+Environment.NewLine);
                var messageList = await  CreateMessageList(guestList);
                if (messageList.Count() == 0) return;
                var client = new RestClient("https://graph.facebook.com/v15.0/113967044907770"); //reception
                
                var resource = "messages";

                client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                                            token, "Bearer");                

                foreach (var message in messageList)
                {
                    //message.template.components[0].parameters[0].document

                    var serializedMessage = JsonConvert.SerializeObject(message);

                    var request = new RestRequest(resource, Method.Post);
                    request.AddJsonBody(message);

                    var cancellationTokenSource = new CancellationTokenSource();
                    var response = await client.ExecuteAsync<MessageResponse>(request, cancellationTokenSource.Token);
                    if (response.IsSuccessful)
                    {
                        //System.Diagnostics.Debug.WriteLine("WA SUCCES:");// + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException?.Message);
                          logMessage =  "[" + DateTime.Now + "] " + message.to + " [MESSAGE SENT]";
                    }
                    else
                    {
                        //System.Diagnostics.Debug.WriteLine("WA ERROR:" + Environment.NewLine + response.Data.error.message);

                        logMessage = "[" + DateTime.Now + "]" + message.to  + " [*** MESSAGE FAILED ***]" + Environment.NewLine;
                        logMessage += response.Data.error.message + Environment.NewLine;
                        logMessage += response.Data.error.error_data?.details+Environment.NewLine;
                    }
                    txtLog.AppendText("\t"+logMessage + Environment.NewLine);
                    Thread.Sleep(1600);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "WA ERROR:" + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException?.Message;
                System.Diagnostics.Debug.WriteLine(errorMessage);
                MessageBox.Show(errorMessage, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logMessage = errorMessage;
                txtLog.AppendText(logMessage + Environment.NewLine);
            }
            finally
            {
                
            }
        }

        private IEnumerable<WAMessage> CreateMessageList_USING_FILELINK(IEnumerable<Guest> guestList)
        {
            var host = "Meghana & Manish Sabade";
            var messageList = new List<WAMessage>();

            foreach (var guest in guestList)
            {
                //if (guest.Phone != "8380011699") continue;

                var waMessage = new WAMessage { type = WAMessageTypes.template.ToString() };
                waMessage.template.name = "invitee";
                waMessage.template.language = new Language { code = "en_US" };
                waMessage.to =  guest.Phone;

                //header - DOCUMENT
                var pdfFileName = $"{guest.Code}-{guest.Name.Replace(" ", "-")}.pdf";
                var fileLink =  $"https://gt.wowqr.in/content/invites/{pdfFileName}";
                var paramDoc = new WADocumentLink { link = fileLink, filename = pdfFileName };

                var headerParameter = new WAParameter { type = WAParameterTypes.document.ToString() };
                headerParameter.document = paramDoc;

                var headerComponent = new WAComponent { type = WAComponentTypes.header.ToString() };
                headerComponent.parameters.Add(headerParameter);
                waMessage.template.components.Add(headerComponent);

                //body
                var bodyParameter1 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter1.text = guest.Name;

                var bodyParameter2 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter2.text = host;

                var bodyComponent = new WAComponent { type = WAComponentTypes.body.ToString() };
                bodyComponent.parameters.Add(bodyParameter1);
                bodyComponent.parameters.Add(bodyParameter2);
                waMessage.template.components.Add(bodyComponent);

                messageList.Add(waMessage);

            }

            return messageList;
        }

        private async Task<IEnumerable<WAMessage>> CreateMessageList(IEnumerable<Guest> guestList)
        {
            var host1 = "Meghana & Manish Sabade";
            var host2 = "Reshma & Jeevan Bansod";
            var bride = "Sanchita";
            var groom = "Anuj";

            var messageList = new List<WAMessage>();

            foreach (var guest in guestList)
            {
                

                var waMessage = new WAMessage { type = WAMessageTypes.template.ToString() };
                waMessage.template.name = "invitej";
                waMessage.template.language = new Language { code = "en_US" };
                waMessage.to =   guest.Phone;

                //header - IMAGE
                var pngFileName = $"{guest.Code}-{guest.Name.Trim().Replace(" ", "-")}.png";
                var mediaID = await GetMediaId(guest);
                if (string.IsNullOrEmpty(mediaID)) continue;
                var paramImage = new Image {   id = mediaID   };

                var headerParameter = new WAParameter { type = WAParameterTypes.image.ToString() };
                headerParameter.image = paramImage;

                var headerComponent = new WAComponent { type = WAComponentTypes.header.ToString() };
                headerComponent.parameters.Add(headerParameter);
                waMessage.template.components.Add(headerComponent);

                //body
                var bodyParameter1 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter1.text = guest.Name;

                var bodyParameter2 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter2.text = groom;

                var bodyParameter3 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter3.text = bride;

                var bodyParameter4 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter4.text = host1;

                var bodyParameter5 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter5.text = host2;

                var bodyComponent = new WAComponent { type = WAComponentTypes.body.ToString() };
                bodyComponent.parameters.Add(bodyParameter1);
                bodyComponent.parameters.Add(bodyParameter2);
                bodyComponent.parameters.Add(bodyParameter3);
                bodyComponent.parameters.Add(bodyParameter4);
                bodyComponent.parameters.Add(bodyParameter5);
                waMessage.template.components.Add(bodyComponent);

                messageList.Add(waMessage);

            }

            return messageList;
        }

        private async Task<IEnumerable<WAMessage>> CreateMessageList_PDF(IEnumerable<Guest> guestList)
        {
            var host1 = "Meghana & Manish Sabade";
            var host2 = "Reshma & Jeevan Bansod";
            var bride = "Sanchita";
            var groom = "Anuj";

            var messageList = new List<WAMessage>();

            foreach (var guest in guestList)
            {


                var waMessage = new WAMessage { type = WAMessageTypes.template.ToString() };
                waMessage.template.name = "entrypass";
                waMessage.template.language = new Language { code = "en_US" };
                waMessage.to = guest.Phone;

                //header - DOCUMENT
                var pdfFileName = $"{guest.Code}-{guest.Name.Trim().Replace(" ", "-")}.pdf";
                var mediaID = await GetMediaId(guest);
                if (string.IsNullOrEmpty(mediaID)) continue;
                var paramDoc = new WADocumentMedia { id = mediaID, filename = pdfFileName };

                var headerParameter = new WAParameter { type = WAParameterTypes.document.ToString() };
                headerParameter.document = paramDoc;

                var headerComponent = new WAComponent { type = WAComponentTypes.header.ToString() };
                headerComponent.parameters.Add(headerParameter);
                waMessage.template.components.Add(headerComponent);

                //body
                var bodyParameter1 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter1.text = guest.Name;

                var bodyParameter2 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter2.text = groom;

                var bodyParameter3 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter3.text = bride;

                var bodyParameter4 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter4.text = host1;

                var bodyParameter5 = new WAParameter { type = WAParameterTypes.text.ToString() };
                bodyParameter5.text = host2;

                var bodyComponent = new WAComponent { type = WAComponentTypes.body.ToString() };
                bodyComponent.parameters.Add(bodyParameter1);
                bodyComponent.parameters.Add(bodyParameter2);
                bodyComponent.parameters.Add(bodyParameter3);
                bodyComponent.parameters.Add(bodyParameter4);
                bodyComponent.parameters.Add(bodyParameter5);
                waMessage.template.components.Add(bodyComponent);

                messageList.Add(waMessage);

            }

            return messageList;
        }

        private async Task<string> GetMediaId(Guest guest)
        {
            var logMessage = string.Empty;

            //var client = new RestClient("https://graph.facebook.com/v15.0/113967044907770"); //reception
            //client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
            //                            token, "Bearer");


            var filepath = $"gatepass/{guest.Code}-{guest.Name.Trim().Replace(" ", "-")}.jpg";
            //var filepath = $"gatepass/invite.png";
            //var guestPDF = new WAUploadMediaParameter
            //{
            //    file = filepath,
            //    type = MediaTypes.PDF 
            //    //messaging_product = "whatsapp"
            //};

            var resource = "media";
            var mediaID = string.Empty;
            var uploadFileParam = FileParameter.FromFile(filepath);
            using (var client = new HttpClient())
            {
                    client.DefaultRequestHeaders.Authorization= new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                
                using (var content = new MultipartFormDataContent("Upload----" + Guid.NewGuid()) )
                {
                    content.Add(new StringContent("whatsapp"), "messaging_product");

                    var filebytes = File.ReadAllBytes(filepath);
                    var fileContent = new ByteArrayContent(filebytes);
                    fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");
                    content.Add(fileContent,"file", uploadFileParam.FileName);
 
                    using (
                       var message =
                           await client.PostAsync("https://graph.facebook.com/v15.0/113967044907770/media", content))
                    {
                        var input = await message.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<MediaUploadResponse>(input);

                        if (response.error ==null)
                        {
                            mediaID = response.id;
                            //System.Diagnostics.Debug.WriteLine("WA SUCCES:");// + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException?.Message);
                            logMessage = "\t [" + DateTime.Now + "] [FILE UPLOADED]" + Path.GetFileName(filepath) + " - " + mediaID;
                        }
                        else
                        {
                            //System.Diagnostics.Debug.WriteLine("WA ERROR:" + Environment.NewLine + response.Data.error.message);

                            logMessage = "[" + DateTime.Now + "]  [*** FILE UPLOAD FAILED ***]" + Path.GetFileName(filepath) + Environment.NewLine;
                            logMessage += response.error.message + Environment.NewLine;
                            logMessage += response.error.error_data?.details + Environment.NewLine;
                        }
                        txtLog.AppendText(logMessage + Environment.NewLine);
                    }
                }
            }





            //var request = new RestRequest(resource, Method.Post);
            //request.AddJsonBody(guestPDF);
            ////request.AddHeader("messaging_product", "whatsapp");
            //request.AddHeader("Content-Type", "multipart/form-data;boundary="+ Guid.NewGuid());
            //request.AlwaysMultipartFormData = true;
            //request.MultipartFormQuoteParameters = true;
            //var uploadFileParam = FileParameter.FromFile(filepath);
            ////request.AddFile(uploadFileParam.Name,uploadFileParam.GetFile,uploadFileParam.ContentType);
            ////request.AddFile("messaging_product=\"whatsapp\"", filepath, guestPDF.type);

            //request.AddFile(uploadFileParam.Name, filepath, "application/octet-stream");
            //var cancellationTokenSource = new CancellationTokenSource();
            //var response = await client.ExecuteAsync<MediaUploadResponse>(request, cancellationTokenSource.Token);
            //if (response.IsSuccessful)
            //{
            //    mediaID = response.Data.id;
            //    //System.Diagnostics.Debug.WriteLine("WA SUCCES:");// + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException?.Message);
            //    logMessage = "[\t [" + DateTime.Now + "] [FILE UPLOADED]" + Path.GetFileName(guestPDF.file) + " - " + mediaID;  
            //}
            //else
            //{
            //    //System.Diagnostics.Debug.WriteLine("WA ERROR:" + Environment.NewLine + response.Data.error.message);

            //    logMessage = "[" + DateTime.Now + "]  [*** FILE UPLOAD FAILED ***]" + Path.GetFileName(guestPDF.file) + Environment.NewLine;
            //    logMessage += response.Data.error.message + Environment.NewLine;
            //    logMessage += response.Data.error.error_data?.details + Environment.NewLine;
            //}
            //txtLog.AppendText(logMessage + Environment.NewLine);
            return mediaID;
        }

        private async Task<string> GetMediaId_PDF(Guest guest)
        {
            var logMessage = string.Empty;

            //var client = new RestClient("https://graph.facebook.com/v15.0/113967044907770"); //reception
            //client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
            //                            token, "Bearer");


            var filepath = $"gatepass/{guest.Code}-{guest.Name.Trim().Replace(" ", "-")}.pdf";
            var guestPDF = new WAUploadMediaParameter
            {
                file = filepath,
                type = MediaTypes.PDF
                //messaging_product = "whatsapp"
            };

            var resource = "media";
            var mediaID = string.Empty;
            var uploadFileParam = FileParameter.FromFile(filepath);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                using (var content = new MultipartFormDataContent("Upload----" + Guid.NewGuid()))
                {
                    content.Add(new StringContent("whatsapp"), "messaging_product");

                    var filebytes = File.ReadAllBytes(filepath);
                    var fileContent = new ByteArrayContent(filebytes);
                    fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(MediaTypes.PDF);
                    content.Add(fileContent, "file", uploadFileParam.FileName);

                    using (
                       var message =
                           await client.PostAsync("https://graph.facebook.com/v15.0/113967044907770/media", content))
                    {
                        var input = await message.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<MediaUploadResponse>(input);

                        if (response.error == null)
                        {
                            mediaID = response.id;
                            //System.Diagnostics.Debug.WriteLine("WA SUCCES:");// + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException?.Message);
                            logMessage = "\t [" + DateTime.Now + "] [FILE UPLOADED]" + Path.GetFileName(guestPDF.file) + " - " + mediaID;
                        }
                        else
                        {
                            //System.Diagnostics.Debug.WriteLine("WA ERROR:" + Environment.NewLine + response.Data.error.message);

                            logMessage = "[" + DateTime.Now + "]  [*** FILE UPLOAD FAILED ***]" + Path.GetFileName(guestPDF.file) + Environment.NewLine;
                            logMessage += response.error.message + Environment.NewLine;
                            logMessage += response.error.error_data?.details + Environment.NewLine;
                        }
                        txtLog.AppendText(logMessage + Environment.NewLine);
                    }
                }
            }





            //var request = new RestRequest(resource, Method.Post);
            //request.AddJsonBody(guestPDF);
            ////request.AddHeader("messaging_product", "whatsapp");
            //request.AddHeader("Content-Type", "multipart/form-data;boundary="+ Guid.NewGuid());
            //request.AlwaysMultipartFormData = true;
            //request.MultipartFormQuoteParameters = true;
            //var uploadFileParam = FileParameter.FromFile(filepath);
            ////request.AddFile(uploadFileParam.Name,uploadFileParam.GetFile,uploadFileParam.ContentType);
            ////request.AddFile("messaging_product=\"whatsapp\"", filepath, guestPDF.type);

            //request.AddFile(uploadFileParam.Name, filepath, "application/octet-stream");
            //var cancellationTokenSource = new CancellationTokenSource();
            //var response = await client.ExecuteAsync<MediaUploadResponse>(request, cancellationTokenSource.Token);
            //if (response.IsSuccessful)
            //{
            //    mediaID = response.Data.id;
            //    //System.Diagnostics.Debug.WriteLine("WA SUCCES:");// + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException?.Message);
            //    logMessage = "[\t [" + DateTime.Now + "] [FILE UPLOADED]" + Path.GetFileName(guestPDF.file) + " - " + mediaID;  
            //}
            //else
            //{
            //    //System.Diagnostics.Debug.WriteLine("WA ERROR:" + Environment.NewLine + response.Data.error.message);

            //    logMessage = "[" + DateTime.Now + "]  [*** FILE UPLOAD FAILED ***]" + Path.GetFileName(guestPDF.file) + Environment.NewLine;
            //    logMessage += response.Data.error.message + Environment.NewLine;
            //    logMessage += response.Data.error.error_data?.details + Environment.NewLine;
            //}
            //txtLog.AppendText(logMessage + Environment.NewLine);
            return mediaID;
        }

        private IEnumerable<Guest> GetGuestList(string filePath)
        {
            //var guestListTable = CSVLibraryAK.CSVLibraryAK.Import(filePath, true);
            var guestListTable = GetCSVData(filePath);
            var guestList = new List<Guest>();

            foreach (var guestcsv in guestListTable.Rows)
            {
                var guestData = ((DataRow)guestcsv);
                var guest = new Guest
                {
                    Code = guestData[0].ToString(),
                    Name = guestData[1].ToString(),
                    Phone = guestData[2].ToString()
                    //IsVip = guestData[3].ToString() == "VIP" ? true : false
                };

                guestList.Add(guest);
            }

            return guestList;
        }

        private DataTable GetCSVData(string filePath)
        {
            var dataTable = new DataTable();
            var csvData = File.ReadAllLines(filePath);
            var columns = csvData[0].Split(',');
            for (int i = 0; i <= columns.Length-1; i++)
            {
                dataTable.Columns.Add();
            }

            foreach (var line in csvData.Skip(1))
            {
                var rowData = line.Split(',');
                //for (int i = 0; i <= columns.Length - 1; i++)
                {
                    dataTable.Rows.Add(rowData);
                    
                }
                dataTable.AcceptChanges();
            }
            return dataTable ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowseGuestList_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a CSV File...";
                ofd.Filter = "CSV (*.csv)|*.csv";
                ofd.Multiselect = false;
                var response = ofd.ShowDialog(this);
                if (response == DialogResult.Cancel) return;

                txtGuestList.Text = ofd.FileName;
            }
        }
    }


    public class Guest
    {
       

        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsVip { get; set; }
    }
}
