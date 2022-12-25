using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

using Vintasoft.Barcode;
using Vintasoft.Barcode.BarcodeStructure;

namespace BarcodeClient
{
    /// <summary>
    /// A simple renderer of matrix barcode.
    /// </summary>
    /// <remarks>
    /// This renderer draws the barcode search patterns and other barcode elements separately.
    /// </remarks>
    public class BarcodeSimpleRenderer : BarcodeGraphicsRenderer
    {

        #region Fields

        /// <summary>
        /// The brush that is used for filling the barcode elements.
        /// </summary>
        SolidBrush _barcodeBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// The pen that is used for drawing the barcode elements.
        /// </summary>
        Pen _barcodePen = new Pen(Color.Black, 1);

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeSimpleRenderer"/> class.
        /// </summary>
        public BarcodeSimpleRenderer()
        {
            _barcodePen.LineJoin = LineJoin.Round;
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets the color that is used for drawing the barcode.
        /// </summary>
        [Category("Colors")]
        [Description("Color that is used for drawing the barcode.")]
        public Color BarcodeColor
        {
            get
            {
                return _barcodeBrush.Color;
            }
            set
            {
                _barcodeBrush.Color = value;
                _barcodePen.Color = value;
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Renders the barcode matrix element.
        /// </summary>
        /// <param name="element">The barcode matrix element to render.</param>
        /// <param name="x">The X-coordinate, where barcode matrix element must be rendered.</param>
        /// <param name="y">The Y-coordinate, where barcode matrix element must be rendered.</param>
        protected override void RenderBarcodeMatrixElement(
            BarcodeMatrixElement element,
            int x,
            int y)
        {
            // customize drawing of search patterns

            bool isAztecSearchPattern = false;
            if (element == BarcodeElements.AztecCompactSearchPattern)
                isAztecSearchPattern = true;
            else if (element == BarcodeElements.AztecFullRangeSearchPattern)
                isAztecSearchPattern = true;

            // QR search pattern
            if (element == BarcodeElements.QrSearchPattern)
            {
                // (x,y)
                //    #######
                //    #     #
                //    # ### #
                //    # ### #
                //    # ### #
                //    #     #
                //    #######
                //       (x+7,y+7)                
                // draw outer rectangle 7x7
                Graphics.DrawRectangle(_barcodePen, x + 1.5f, y + 1.5f, 6, 6);
                // draw inner cell 3x3
                Graphics.FillRectangle(_barcodeBrush, x + 3.75f, y + 3.75f, 1.5f, 1.5f);
                Graphics.DrawRectangle(_barcodePen, x + 3.5f, y + 3.5f, 2f, 2f);
            }
            // Aztec search pattern
            else if (isAztecSearchPattern)
            {
                //  Compact/Rune          Full Range
                //                    (x,y)
                //                       #############
                // (x,y)                 #           #
                //    #########          # ######### #
                //    #       #          # #       # #
                //    # ##### #          # # ##### # #
                //    # #   # #          # # #   # # #
                //    # # # # #          # # # # # # #
                //    # #   # #          # # #   # # #
                //    # ##### #          # # ##### # #
                //    #       #          # #       # #
                //    #########          # ######### #
                //         (x+9,y+9)     #           #
                //                       #############
                //                               (x+13,y+13)
                // draw outer rectangles: 13x13, 9x9, 5x5
                int rectCount;
                if (element == BarcodeElements.AztecFullRangeSearchPattern)
                    rectCount = 3;
                else
                    rectCount = 2;
                for (int i = 0; i < rectCount; i++)
                {
                    Graphics.DrawRectangle(_barcodePen,
                        x + 0.5f, y + 0.5f,
                        (rectCount - i) * 4, (rectCount - i) * 4);
                    x += 2;
                    y += 2;
                }

                // draw inner cell 1x1
                Graphics.FillEllipse(_barcodeBrush, new Rectangle(x, y, 1, 1));
            }            
            // DataMatrix and Han Xin Code search patterns
            else if (element.Name.Contains("Search Pattern"))
            {
                // draw as single pattern use graphics path
                using (GraphicsPath path = new GraphicsPath())
                {
                    for (int yc = 0; yc < element.Height; yc++)
                    {
                        for (int xc = 0; xc < element.Width; xc++)
                        {
                            if (element.Matrix[yc, xc] == BarcodeElements.BlackCell)
                                path.AddRectangle(new Rectangle(x + xc, y + yc, 1, 1));
                        }
                    }
                    Graphics.FillPath(_barcodeBrush, path);
                }
            }
            else
            {
                base.RenderBarcodeMatrixElement(element, x, y);
            }
        }
        
        /// <summary>
        /// Renders the barcode element.
        /// </summary>
        /// <param name="barcodeElement">The barcode element to render.</param>
        /// <param name="x">The X-coordinate, where barcode element must be rendered.</param>
        /// <param name="y">The Y-coordinate, where barcode element must be rendered.</param>
        protected override void RenderBarcodeElement(BarcodeElement barcodeElement, int x, int y)
        {
            if (barcodeElement == BarcodeElements.BlackCell)
            {
                Graphics.FillEllipse(_barcodeBrush, new Rectangle(x, y, 1, 1));
            }
            else
            {
                base.RenderBarcodeElement(barcodeElement, x, y);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Simple dots";
        }

        #endregion

    }
}
