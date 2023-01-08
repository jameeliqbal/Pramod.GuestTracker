using BarcodeClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vintasoft.Barcode;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;

namespace Pramod.GuestTracker.QR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetGuestList_Click(object sender, EventArgs e)
        {
            //var client = new RestClient("https://localhost:44334/" );
            var client = new RestClient("http://gt.wowqr.in/");
            var request = new RestRequest("Guests/List",Method.Get);
            //request.AddHeader("X-Token-Key", "dsds-sdsdsds-swrwerfd-dfdfd");
            var response = client.Execute(request);
            var content = response.Content; // raw content as string
            var guestList = JsonConvert.DeserializeObject<List<Guest>>(content);


            
            grdGuestList.DataSource = guestList;

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grdGuestList.FilterRowPosition = Syncfusion.WinForms.DataGrid.Enums.RowPosition.FixedTop;
            grdGuestList.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.AllCellsWithLastColumnFill;
            grdGuestList.AutoSizeController.AutoSizeCalculationMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeCalculationMode.SmartFit;
            grdGuestList.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            
            GridTableSummaryRow tableSummaryRow1 = new GridTableSummaryRow();
            tableSummaryRow1.Name = "TableSummary";
            tableSummaryRow1.ShowSummaryInRow = false;
            tableSummaryRow1.Position = VerticalPosition.Bottom;

            GridSummaryColumn TotalGuestsColumn = new GridSummaryColumn();
            TotalGuestsColumn.Name = "TotalGuests";
            TotalGuestsColumn.SummaryType = SummaryType.DoubleAggregate;
            TotalGuestsColumn.Format = "Total Guests: {Count}";
            TotalGuestsColumn.MappingName = "Name";

            tableSummaryRow1.SummaryColumns.Add(TotalGuestsColumn);

            grdGuestList.TableSummaryRows.Add(tableSummaryRow1);
        }

        private void btnGenerteQR_Click_PDF(object sender, EventArgs e)
        {
            PdfTemplate templatePage1 = null;
            PdfTemplate templatePage2 = null;

            //using (var templateCard = new PdfLoadedDocument("Reception_Map-02.pdf"))
            using (var templateCard = new PdfLoadedDocument("Reception-compressed.pdf"))
           {
                var loadedPage1 = templateCard.Pages[0] as PdfLoadedPage;
                var loadedPage2 = templateCard.Pages[1] as PdfLoadedPage;
                var pgraphics = loadedPage2.Graphics;
                var dirImage = new PdfBitmap("directions.png");
                var directionsWidth = dirImage.Width * 1.81f;
                var directionsHeight = dirImage.Height * 1.5f;
                //pgraphics.DrawImage(dirImage, 180f, 1100f, directionsWidth, directionsHeight);
                pgraphics.DrawImage(dirImage, 300f, 970f, directionsWidth, directionsHeight);
                pgraphics.Flush();

                //Create url annotation
                //PdfUriAnnotation directionsLink = new PdfUriAnnotation(new RectangleF(300f, 1000f, 490f, 50f));
                PdfUriAnnotation directionsLink = new PdfUriAnnotation(new RectangleF(300f, 1000f, directionsWidth, directionsHeight));
                //Add the link 
                directionsLink.Uri = "https://maps.app.goo.gl/finDQUhB9Q9CXh7QA";
                //Border
                var border= new PdfAnnotationBorder();
                border.Width = 0;
                border.VerticalRadius = 0;
                border.HorizontalRadius = 0;
               directionsLink.Border = border;
                    //Add the color
                directionsLink.Color = new PdfColor(Color.White);

                templatePage1 = loadedPage1.CreateTemplate();
                templatePage2 = loadedPage2.CreateTemplate();



                //foreach (var record in grdGuestList.View.Records)
                //{
                //    using (var newDoc = new PdfDocument())
                //    {
                //        newDoc.PageSettings.Margins = new PdfMargins { All = 0 };
                //        newDoc.PageSettings.Size = loadedPage2.Size;

                //        var p1 = newDoc.Pages.Add();
                //        var p1Graphics = p1.Graphics;

                //        p1Graphics.DrawPdfTemplate(templatePage1, PointF.Empty);
                //        p1Graphics.Flush();

                //        var p2 = newDoc.Pages.Add();
                //        var p2Graphics = p2.Graphics;

                //        p2Graphics.DrawPdfTemplate(templatePage2, PointF.Empty);
                //        p2Graphics.Flush();

                //        //Add the annotation
                //        p2.Annotations.Add(directionsLink);

                //        var guest = (Guest)record.Data;
                //        var code = guest.Code;
                //        var qr = CreateQRCode($"http://gt.wowqr.in?id={code}");

                //        DrawGuestInfoPDF(guest, qr, newDoc);
                //        newDoc.Close();
                //    }
                //}

                var guest1 = new Guest
                {
                    Code = "1",
                    Name = "Pramod Gokhale",
                    Phone = "919422080811"
                };

                var guest2= new Guest
                {
                    Code = "2",
                    Name = "Ashutosh Soman",
                    Phone = "918668987244"
                };

                var list = new List<Guest> { guest1, guest2 };

                foreach (var guest in list)
                {
                    using (var newDoc = new PdfDocument())
                    {
                        newDoc.PageSettings.Margins = new PdfMargins { All = 0 };
                        newDoc.PageSettings.Size = loadedPage2.Size;

                        var p1 = newDoc.Pages.Add();
                        var p1Graphics = p1.Graphics;

                        p1Graphics.DrawPdfTemplate(templatePage1, PointF.Empty);
                        p1Graphics.Flush();

                        var p2 = newDoc.Pages.Add();
                        var p2Graphics = p2.Graphics;

                        p2Graphics.DrawPdfTemplate(templatePage2, PointF.Empty);
                        p2Graphics.Flush();

                        //Add the annotation
                        p2.Annotations.Add(directionsLink);

                       
                        var code = guest.Code;
                        var qr = CreateQRCode($"http://gt.wowqr.in?id={code}");

                        DrawGuestInfoPDF(guest, qr, newDoc);
                        newDoc.Close();
                    }
                }

                templateCard.Close();
            }

        }

        private void btnGenerteQR_Click(object sender, EventArgs e)
        {
            

            using (var templateCard = Image.FromFile("ReceptionCard.jpg"))
            {
                



                //foreach (var record in grdGuestList.View.Records)
                //{
                //    using (var newDoc = new PdfDocument())
                //    {
                //        newDoc.PageSettings.Margins = new PdfMargins { All = 0 };
                //        newDoc.PageSettings.Size = loadedPage2.Size;

                //        var p1 = newDoc.Pages.Add();
                //        var p1Graphics = p1.Graphics;

                //        p1Graphics.DrawPdfTemplate(templatePage1, PointF.Empty);
                //        p1Graphics.Flush();

                //        var p2 = newDoc.Pages.Add();
                //        var p2Graphics = p2.Graphics;

                //        p2Graphics.DrawPdfTemplate(templatePage2, PointF.Empty);
                //        p2Graphics.Flush();

                //        //Add the annotation
                //        p2.Annotations.Add(directionsLink);

                //        var guest = (Guest)record.Data;
                //        var code = guest.Code;
                //        var qr = CreateQRCode($"http://gt.wowqr.in?id={code}");

                //        DrawGuestInfoPDF(guest, qr, newDoc);
                //        newDoc.Close();
                //    }
                //}

                var guest1 = new Guest
                {
                    Code = "1",
                    Name = "Pramod Gokhale",
                    Phone = "919422080811"
                };

                var guest2 = new Guest
                {
                    Code = "2",
                    Name = "Ashutosh Soman",
                    Phone = "918668987244"
                };

                var guest3 = new Guest
                {
                    Code = "3",
                    Name = "Jameel Saroash Iqbal",
                    Phone = "919611133571"
                };

                var guest4 = new Guest
                {
                    Code = "4",
                    Name = "Mohana Basaran",
                    Phone = "91987654321"
                };


                var list = new List<Guest> { guest1, guest2,guest3, guest4 };
                string dirName = "cards";
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }

                foreach (var guest in list)
                {
                        var code = guest.Code;
                        var qr = CreateQRCode($"http://gt.wowqr.in?id={code}");

                    using (var newImage = new Bitmap(templateCard))
                    {
                        newImage.SetResolution(182.0f, 182.0f);
                        using (var niGraphics = Graphics.FromImage(newImage))
                        {
                            niGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                            DrawGuestInfoJPEG(guest, qr, newImage);
                        }


                        var guestName = guest.Name.ToLower().Trim().Replace(' ', '-');
                        var filePath = Path.Combine(dirName, $"{guest.Code}-{guestName}.jpg");
                        newImage.Save(filePath, ImageFormat.Jpeg);
                    }
                }

                 
            }

        }


        private void DrawGuestInfoPDF(Guest guest, Bitmap qr, PdfDocument newDoc)
        {

            var cgraphics = newDoc.Pages[1].Graphics;
            using (var label = new Bitmap(800, 400))
            using (var lgraphics = Graphics.FromImage(label))
            {
                FillRoundedRectangle(lgraphics, new SolidBrush(Color.White), new Rectangle(0, 110, 210, 210), 20);
                //DrawRoundedRectangle(lgraphics, new Pen(Color.FromArgb(255, 1, 32, 17),2f), new Rectangle(10,10,400,300), 20);
                var brushColor = Color.FromArgb(255, 1, 32, 17);
                lgraphics.DrawString(guest.Name, new Font("Verdana", 30f, FontStyle.Bold),
                   new SolidBrush(brushColor), 250, 120);


                lgraphics.DrawImage(qr, 10, 120);

                if (guest.IsVip)
                    brushColor = Color.DarkGoldenrod;
                lgraphics.DrawString(guest.Code.PadLeft(3, '0'), new Font("Verdana", 110f, FontStyle.Regular),
                    new SolidBrush(brushColor), 240, 150);

                //label.Save("label.png", ImageFormat.Png);

                cgraphics.DrawImage(PdfImage.FromImage(label), new Point(300, 1250));

                string dirName = "cards";
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }

                var guestName = guest.Name.ToLower().Replace(' ', '-');
                var filePath = Path.Combine(dirName, $"{guest.Code}-{guestName}.pdf");
                newDoc.Save(filePath);
            }

        }

        private void DrawGuestInfoJPEG(Guest guest, Bitmap qr, Bitmap card)
        {
            using (var cgraphics = Graphics.FromImage(card))
            //using (var label = new Bitmap(400, 300))
            {
                //label.SetResolution(300.0f, 300.0f);
                //using (var lgraphics = Graphics.FromImage(label))
                {
                    //lgraphics.SmoothingMode = SmoothingMode.AntiAlias;

                    FillRoundedRectangle(cgraphics, new SolidBrush(Color.White), new Rectangle(2900, 2750, 320, 320), 20);
                    //DrawRoundedRectangle(lgraphics, new Pen(Color.FromArgb(255, 1, 32, 17),2f), new Rectangle(10,10,400,300), 20);
                    var brushColor = Color.FromArgb(255, 1, 32, 17);
                    cgraphics.DrawString(guest.Name, new Font("Verdana", 20f, FontStyle.Bold),
                       new SolidBrush(brushColor), 3250, 2760);


                    cgraphics.DrawImage(qr, 2910, 2760, 300,300);

                    if (guest.IsVip)
                        brushColor = Color.DarkGoldenrod;
                   cgraphics.DrawString(guest.Code.PadLeft(3, '0'), new Font("Verdana", 110f, FontStyle.Regular),
                        new SolidBrush(brushColor), 3200, 2760);

                    //label.Save("label.png", ImageFormat.Png);

                    //cgraphics.DrawImage(label, new Point(1120, 1050));



                }
            }
        }

        private void DrawGuestInfo(Guest guest, Bitmap qr)
        {
            using (var card = Image.FromFile("Reception_Map-02.png"))
            using (var cgraphics = Graphics.FromImage(card))
            using (var label = new Bitmap(800, 400))
            using (var lgraphics = Graphics.FromImage(label))
            {
                FillRoundedRectangle(lgraphics, new SolidBrush(Color.White), new Rectangle(0, 110, 210, 210), 20);
                //DrawRoundedRectangle(lgraphics, new Pen(Color.FromArgb(255, 1, 32, 17),2f), new Rectangle(10,10,400,300), 20);
                var brushColor = Color.FromArgb(255, 1, 32, 17);
                lgraphics.DrawString(guest.Name, new Font("Verdana", 30f, FontStyle.Bold),
                   new SolidBrush(brushColor), 250, 120);


                lgraphics.DrawImage(qr, 10, 120);

                if (guest.IsVip)
                    brushColor = Color.DarkGoldenrod;
                lgraphics.DrawString(guest.Code.PadLeft(3, '0'), new Font("Verdana", 110f, FontStyle.Regular),
                    new SolidBrush(brushColor), 240, 150);

                //label.Save("label.png", ImageFormat.Png);

                cgraphics.DrawImage(label, new Point(400, 1670));

                string dirName = "cards";
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }

                var guestName = guest.Name.ToLower().Replace(' ', '-');
                var filePath = Path.Combine(dirName, $"{guest.Code}-{guestName}.jpg");
                card.Save(filePath, ImageFormat.Jpeg);
            }

        }

        private void DrawGuestInfo(string guestName, string guestCode,bool isVip,Bitmap qr)
        {
            using (var card=Image.FromFile("Reception_Map-02.png"))
            using (var cgraphics=Graphics.FromImage(card))
            using (var label = new Bitmap(800, 400))
            using (var lgraphics = Graphics.FromImage(label))
            {
                FillRoundedRectangle(lgraphics, new SolidBrush(Color.White), new Rectangle(0, 110, 210, 210), 20);
                //DrawRoundedRectangle(lgraphics, new Pen(Color.FromArgb(255, 1, 32, 17),2f), new Rectangle(10,10,400,300), 20);
                var brushColor = Color.FromArgb(255, 1, 32, 17);
                lgraphics.DrawString(guestName, new Font("Verdana", 30f, FontStyle.Bold),
                   new SolidBrush(brushColor),250,120);
                
                 
                lgraphics.DrawImage(qr, 10, 120);

                if (isVip)
                    brushColor = Color.DarkGoldenrod;
                lgraphics.DrawString(guestCode.PadLeft(3,'0'), new Font("Verdana", 110f, FontStyle.Regular),
                    new SolidBrush(brushColor), 250, 150);

                //label.Save("label.png", ImageFormat.Png);

                cgraphics.DrawImage(label, new Point(400, 1670));

                string dirName= "cards";
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                 
                var filePath = Path.Combine(dirName, $"{guestCode}-{guestName}.jpg");
                card.Save(filePath, ImageFormat.Jpeg);
            }
            
        }

        public static void DrawRoundedRectangle( Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (pen == null)
                throw new ArgumentNullException("pen");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        public static void FillRoundedRectangle( Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (brush == null)
                throw new ArgumentNullException("brush");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }


   


        private Bitmap CreateQRCode(string data)
        {
            return GenerateColorQRCodeFromText("", data, 2, 2, "", 0);
        }

        
        private Bitmap GenerateColorQRCodeFromText(string QRPDFFilename, string QRData, double widthInch, double heightInch, string QRFileType, double logoSizePercentage)
        {
            try
            {
                //BarcodeGraphicsRenderer Class
                //https://www.vintasoft.com/docs/vsbarcode-dotnet/Vintasoft.Barcode/Vintasoft.Barcode.BarcodeGraphicsRenderer.html


                float rez = 300.0f;
                //int width =  600, height = 600;
                //formula for inch to pixel = inch x resolution; 1inch = 96pixels https://www.pixelto.net/inches-to-px-converter
                double width = (double)(rez * widthInch), height = (double)(rez * heightInch);

                BarcodeWriter writer = new BarcodeWriter();
                writer.Settings.Barcode = BarcodeType.QR;
                writer.Settings.QRErrorCorrectionLevel = Vintasoft.Barcode.BarcodeInfo.QRErrorCorrectionLevel.L;
                writer.Settings.QRSymbol = Vintasoft.Barcode.BarcodeInfo.QRSymbolVersion.Version2;
                writer.Settings.Value = QRData;
                writer.Settings.Resolution = rez;
                writer.Settings.PixelFormat = BarcodeImagePixelFormat.Bgr24;
                writer.Settings.Padding = 0;
                double quietZone = Math.Min(width, height) / 20;
                writer.Settings.QuietZoneTop = (int)quietZone;
                writer.Settings.QuietZoneLeft = (int)quietZone;
                writer.Settings.QuietZoneRight = (int)quietZone;
                writer.Settings.QuietZoneBottom = (int)quietZone;



                var connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
                connectedMatixBarcodeRenderer.ConnectOrthogonalCells = false;
                connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
                connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.BeveledSquare;
                connectedMatixBarcodeRenderer.DotDiameter = 0.75f;
                connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.4f;
                connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
                connectedMatixBarcodeRenderer.ForegroundColor = Color.FromArgb(255, 1, 32, 17);// Color.DarkGreen;

                //////0, 92, 162 dark blue
                //////197, 38, 58 red
                //connectedMatixBarcodeRenderer.AlignmentPatternsColor = Color.FromArgb(0, 92, 162);
                ////connectedMatixBarcodeRenderer.BackgroundColor = Color.White;
                //connectedMatixBarcodeRenderer.DataLayerColor = Color.FromArgb(0, 92, 162);
                //connectedMatixBarcodeRenderer.FormatInformationColor = Color.FromArgb(0, 92, 162);
                //connectedMatixBarcodeRenderer.SearchPatternsColor = Color.FromArgb(197, 38, 58);
                //connectedMatixBarcodeRenderer.TimingPatternsColor = Color.FromArgb(0, 92, 162);
                //connectedMatixBarcodeRenderer.HiQuality = true;



                //using (var bmQR = connectedMatixBarcodeRenderer.GetBarcodeAsBitmap(writer, widthInch, heightInch, UnitOfMeasure.Inches))
                //using (var gfxQR = Graphics.FromImage(bmQR))
                //{
                //    var logopath = $@"C:\Users\Jameel\Documents\LaibaTech\clients\SKF\asset\logos\aid\AID logo.jpg";
                //    var logoWidth = width * (logoSizePercentage / 100);
                //    var logoHeight = height * (logoSizePercentage / 100);
                //    var logoX = (width - logoWidth) / 2;
                //    var logoY = (height - logoHeight) / 2;
                //    var bmLogo = new Bitmap(logopath);
                //    gfxQR.DrawImage(bmLogo, (float)logoX, (float)logoY, (float)logoWidth, (float)logoHeight);

                //    //SaveToPdf((Bitmap)bmQR.Clone(), QRPDFFilename, widthInch, heightInch, QRFileType);


                //    bmQR.Save("output.png", ImageFormat.Jpeg);
                //}

                using (var bmQR = connectedMatixBarcodeRenderer.GetBarcodeAsBitmap(writer, widthInch, heightInch, UnitOfMeasure.Inches))
                {
                    //bmQR.Save("output.png", ImageFormat.Png);
                    return (Bitmap)bmQR.Clone();
                }
            }
            catch
            {

                throw;
            }
        }

        private void btnGenerateHubVerifyToken_Click(object sender, EventArgs e)
        {
           var hubVerifyToken= Guid.NewGuid();
            System.Diagnostics.Debug.WriteLine(hubVerifyToken);
        }
    }

    public class Guest
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsVip { get; set; }
    }
}
