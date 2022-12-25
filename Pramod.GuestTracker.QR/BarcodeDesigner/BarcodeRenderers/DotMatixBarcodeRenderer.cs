using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

using Vintasoft.Barcode;
using Vintasoft.Barcode.BarcodeStructure;

namespace BarcodeClient
{
    /// <summary>
    /// A matrix barcode renderer that uses custom dots for drawing a matrix barcode.
    /// </summary>
    public class DotMatixBarcodeRenderer : MatrixBarcodeRendererBase
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotMatixBarcodeRenderer"/> class.
        /// </summary>
        public DotMatixBarcodeRenderer()
        {
            FormatInformationColor = DataLayerColor;
        }

        #endregion



        #region Properties

        float _dotDiameter = 0.8f;
        /// <summary>
        /// Gets or sets a dot diameter.
        /// </summary>
        [Category("Design")]
        [Description("A dot diameter.")]
        public float DotDiameter
        {
            get
            {
                return _dotDiameter;
            }
            set
            {
                _dotDiameter = Math.Max(0, value);
            }
        }

        BarcodeMatixDotType _dotType = BarcodeMatixDotType.Circle;
        /// <summary>
        /// Gets or sets a dot type.
        /// </summary>
        [Category("Design")]
        [Description("A dot type.")]
        public BarcodeMatixDotType DotType
        {
            get
            {
                return _dotType;
            }
            set
            {
                _dotType = value;
            }
        }

        bool _drawAlignmentPatternAsSinglePattern = true;
        /// <summary>
        /// Gets or sets a value indicating whether renderer must
        /// draw an alignment pattern as a single pattern.
        /// </summary>
        /// <value>
        /// <b>True</b> if renderer must draw an alignment pattern as a single pattern;
        /// otherwise, <b>false</b>.
        /// </value>
        [Category("Design")]
        public bool DrawAlignmentPatternAsSinglePattern
        {
            get
            {
                return _drawAlignmentPatternAsSinglePattern;
            }
            set
            {
                _drawAlignmentPatternAsSinglePattern = value;
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Dot Matrix";
        }

        #endregion


        #region PROTECTED

        /// <summary>
        /// Renders the barcode matrix element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="x">The X-coordinate of element.</param>
        /// <param name="y">The Y-coordinate of element.</param>
        protected override void RenderBarcodeMatrixElement(
            BarcodeMatrixElement element,
            int x,
            int y)
        {
            // customize drawing of search and alignment patterns

            bool isAztecSearchPattern = false;
            if (element == BarcodeElements.AztecCompactSearchPattern)
                isAztecSearchPattern = true;
            else if (element == BarcodeElements.AztecFullRangeSearchPattern)
                isAztecSearchPattern = true;

            bool isQrAlignmentPattern = false;
            if (element == BarcodeElements.QrAlignmentPattern)
                isQrAlignmentPattern = true;

            bool isHanXinRightTopOrLeftBottom = false;
            if (element == BarcodeElements.HanXinCodeRightTopSearchPattern)
                isHanXinRightTopOrLeftBottom = true;
            else if (element == BarcodeElements.HanXinCodeLeftBottomSearchPattern)
                isHanXinRightTopOrLeftBottom = true;

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
                using (Pen blackPen = new Pen(SearchPatternsColor))
                {
                    if (DotType != BarcodeMatixDotType.Square)
                        blackPen.LineJoin = LineJoin.Round;
                    // draw outer rectangle 7x7
                    Graphics.DrawRectangle(blackPen, x + 1.5f, y + 1.5f, 6, 6);
                    // draw inner cell 3x3
                    using (Brush brush = new SolidBrush(SearchPatternsColor))
                        Graphics.FillRectangle(brush, x + 3.75f, y + 3.75f, 1.5f, 1.5f);
                    Graphics.DrawRectangle(blackPen, x + 3.5f, y + 3.5f, 2f, 2f);
                }
            }
            // QR alignment pattern
            else if (DrawAlignmentPatternAsSinglePattern && isQrAlignmentPattern)
            {
                // (x,y)
                //    #####
                //    #   #
                //    # # #
                //    #   #
                //    #####
                //       (x+5,y+5)
                using (Pen blackPen = new Pen(AlignmentPatternsColor, DotDiameter))
                {
                    if (DotType != BarcodeMatixDotType.Square)
                        blackPen.LineJoin = LineJoin.Round;
                    // draw outer rectangle 5x5
                    Graphics.DrawRectangle(blackPen, x + 0.5f, y + 0.5f, 4, 4);
                    // draw inner cell 1x1
                    using (SolidBrush brush = new SolidBrush(AlignmentPatternsColor))
                        FillDot(brush, x + 2, y + 2);
                }
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
                using (Pen blackPen = new Pen(SearchPatternsColor))
                {
                    if (DotType != BarcodeMatixDotType.Square)
                        blackPen.LineJoin = LineJoin.Round;

                    // draw outer rectangles: 13x13, 9x9, 5x5
                    int rectCount;
                    if (element == BarcodeElements.AztecFullRangeSearchPattern)
                        rectCount = 3;
                    else
                        rectCount = 2;
                    for (int i = 0; i < rectCount; i++)
                    {
                        Graphics.DrawRectangle(blackPen,
                            x + 0.5f, y + 0.5f,
                            (rectCount - i) * 4, (rectCount - i) * 4);
                        x += 2;
                        y += 2;
                    }

                    // draw inner cell 1x1
                    using (Brush brush = new SolidBrush(blackPen.Color))
                        Graphics.FillRectangle(brush, x + 0.25f, y + 0.25f, 0.5f, 0.5f);
                    blackPen.Width = 0.5f;
                    Graphics.DrawRectangle(blackPen, x + 0.25f, y + 0.25f, 0.5f, 0.5f);
                }
            }
            // Han Xin Code search pattern (LeftTop)
            else if (element == BarcodeElements.HanXinCodeLeftTopSearchPattern)
            {
                // (x,y)
                //    #######
                //    #
                //    # #####
                //    # # 
                //    # # ###
                //    # # ###
                //    # # ###
                //       (x+7,y+7)
                using (Pen blackPen = new Pen(SearchPatternsColor))
                {
                    if (DotType != BarcodeMatixDotType.Square)
                        blackPen.LineJoin = LineJoin.Round;
                    // draw outer line
                    Graphics.DrawLines(blackPen, new PointF[] { 
                        new PointF(x + 1.5f, y + 8f),
                        new PointF(x + 1.5f, y + 1.5f), 
                        new PointF(x + 8f, y + 1.5f) });
                    // draw center line
                    Graphics.DrawLines(blackPen, new PointF[] {
                        new PointF(x + 3.5f, y + 8f), 
                        new PointF(x + 3.5f, y + 3.5f), 
                        new PointF(x + 8f, y + 3.5f) });
                    // draw cell 3x3
                    using (Brush brush = new SolidBrush(SearchPatternsColor))
                        Graphics.FillRectangle(brush, x + 5.75f, y + 5.75f, 1.5f, 1.5f);
                    Graphics.DrawRectangle(blackPen, x + 5.5f, y + 5.5f, 2f, 2f);
                }
            }
            // Han Xin Code search pattern (RightBottom)
            else if (element == BarcodeElements.HanXinCodeRightBottomSearchPattern)
            {
                // (x,y)
                //    ### # #
                //    ### # #
                //    ### # #
                //        # #
                //    ##### #
                //          #
                //    #######
                //       (x+7,y+7)
                using (Pen blackPen = new Pen(SearchPatternsColor))
                {
                    if (DotType != BarcodeMatixDotType.Square)
                        blackPen.LineJoin = LineJoin.Round;
                    // draw outer line
                    Graphics.DrawLines(blackPen, new PointF[] { 
                        new PointF(x + 1f, y + 7.5f), 
                        new PointF(x + 7.5f, y + 7.5f),
                        new PointF(x + 7.5f, y + 1f) });
                    // draw center line
                    Graphics.DrawLines(blackPen, new PointF[] {
                        new PointF(x + 1f, y + 5.5f),
                        new PointF(x + 5.5f, y + 5.5f), 
                        new PointF(x + 5.5f, y + 1f) });
                    // draw cell 3x3
                    using (Brush brush = new SolidBrush(SearchPatternsColor))
                        Graphics.FillRectangle(brush, x + 1.75f, y + 1.75f, 1.5f, 1.5f);
                    Graphics.DrawRectangle(blackPen, x + 1.5f, y + 1.5f, 2f, 2f);
                }
            }
            // Han Xin Code search pattern (RightTop or LeftBottom)
            else if (isHanXinRightTopOrLeftBottom)
            {
                // (x,y)
                //    #######
                //          #
                //    ##### #
                //        # #
                //    ### # #
                //    ### # #
                //    ### # #
                //    ### # #
                //       (x+7,y+7)
                using (Pen blackPen = new Pen(SearchPatternsColor))
                {
                    if (DotType != BarcodeMatixDotType.Square)
                        blackPen.LineJoin = LineJoin.Round;
                    // draw outer line
                    Graphics.DrawLines(blackPen, new PointF[] { 
                        new PointF(x + 1f, y + 1.5f), 
                        new PointF(x + 7.5f, y + 1.5f), 
                        new PointF(x + 7.5f, y + 8f) });
                    // draw center line
                    Graphics.DrawLines(blackPen, new PointF[] { 
                        new PointF(x + 1f, y + 3.5f), 
                        new PointF(x + 5.5f, y + 3.5f), 
                        new PointF(x + 5.5f, y + 8f) });
                    // draw cell 3x3
                    using (Brush brush = new SolidBrush(SearchPatternsColor))
                        Graphics.FillRectangle(brush, x + 1.75f, y + 5.75f, 1.5f, 1.5f);
                    Graphics.DrawRectangle(blackPen, x + 1.5f, y + 5.5f, 2f, 2f);
                }
            }
            // DataMatrix search pattern
            else if (element.Name.Contains("Search Pattern"))
            {
                // draw as single pattern use graphics path
                using (GraphicsPath path = new GraphicsPath())
                using (SolidBrush blackBrush = CreateBrush(x, y, true))
                {
                    for (int yc = 0; yc < element.Height; yc++)
                    {
                        for (int xc = 0; xc < element.Width; xc++)
                        {
                            if (element.Matrix[yc, xc] == BarcodeElements.BlackCell)
                                path.AddRectangle(new Rectangle(x + xc, y + yc, 1, 1));
                        }
                    }
                    Graphics.FillPath(blackBrush, path);
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
        /// <param name="element">The element.</param>
        /// <param name="x">The X-coordinate of element.</param>
        /// <param name="y">The Y-coordinate of element.</param>
        protected override void RenderBarcodeElement(BarcodeElement element, int x, int y)
        {
            if (element == BarcodeElements.BlackCell)
            {
                using (SolidBrush brush = CreateBrush(x, y, true))
                    FillDot(brush, x, y);
            }
            else if (element == BarcodeElements.WhiteCell)
            {
                using (SolidBrush brush = CreateBrush(x, y, false))
                    FillDot(brush, x, y);
            }
            else
            {
                base.RenderBarcodeElement(element, x, y);
            }
        }

        /// <summary>
        /// Creates the brush for cell at specified location.
        /// </summary>
        /// <param name="x">The X-coordinate of cell.</param>
        /// <param name="y">The Y-coordinate of cell.</param>
        /// <param name="isBlack">Defines when brush creates for "black" cell.</param>
        protected virtual SolidBrush CreateBrush(int x, int y, bool isBlack)
        {
            if (IsAlignmentPattern(x, y))
            {
                if (isBlack)
                    return new SolidBrush(AlignmentPatternsColor);
                return new SolidBrush(GetLightenColor(AlignmentPatternsColor));
            }
            if (IsSearchPattern(x, y))
            {
                if (isBlack)
                    return new SolidBrush(SearchPatternsColor);
                return new SolidBrush(GetLightenColor(SearchPatternsColor));
            }
            if (IsTimingPattern(x, y))
            {
                if (isBlack)
                    return new SolidBrush(TimingPatternsColor);
                return new SolidBrush(GetLightenColor(TimingPatternsColor));
            }
            if (IsDataLayer(x, y))
            {
                if (isBlack)
                    return new SolidBrush(DataLayerColor);
                return new SolidBrush(GetLightenColor(DataLayerColor));
            }
            if (IsFormatInformation(x, y))
            {
                if (isBlack)
                    return new SolidBrush(FormatInformationColor);
                return new SolidBrush(GetLightenColor(FormatInformationColor));
            }
            throw new NotImplementedException();
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Fills the dot at specified location.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The X-coordinate of dot.</param>
        /// <param name="y">The Y-coordinate of dot.</param>
        private void FillDot(SolidBrush brush, int x, int y)
        {
            if (brush.Color.ToArgb() == BackgroundColor.ToArgb())
                return;

            switch (DotType)
            {
                case BarcodeMatixDotType.Circle:
                    Graphics.FillEllipse(brush, GetDotBoundingBox(x, y));
                    break;
                case BarcodeMatixDotType.Rhombus:
                    Graphics.FillPolygon(brush, GetRhombusPolygon(GetDotBoundingBox(x, y)));
                    break;
                case BarcodeMatixDotType.Square:
                    Graphics.FillRectangle(brush, GetDotBoundingBox(x, y));
                    break;
                case BarcodeMatixDotType.BeveledSquare:
                    Graphics.FillPolygon(brush, GetBeveledSquarePolygon(GetDotBoundingBox(x, y)));
                    break;
                case BarcodeMatixDotType.Cross:
                    Graphics.FillPolygon(brush, GetCrossPolygon(GetDotBoundingBox(x, y)));
                    break;
                case BarcodeMatixDotType.DiagonalCross:
                    using (Pen pen = new Pen(brush, DotDiameter))
                    {
                        pen.EndCap = LineCap.Round;
                        pen.StartCap = LineCap.Round;
                        Graphics.DrawLine(pen, new PointF(x, y), new PointF(x + 1, y + 1));
                        Graphics.DrawLine(pen, new PointF(x + 1, y), new PointF(x, y + 1));
                    }
                    break;
            }
        }

        /// <summary>
        /// Returns a polygon that contains rhombus.
        /// </summary>
        /// <param name="bbox">The bounding box of a rhombus.</param>
        /// <returns>A polygon that contains rhombus.</returns>
        private PointF[] GetRhombusPolygon(RectangleF bbox)
        {
            return new PointF[]{
                new PointF(bbox.X + bbox.Width / 2, bbox.Y),
                new PointF(bbox.X + bbox.Width, bbox.Y + bbox.Height / 2),
                new PointF(bbox.X + bbox.Width / 2, bbox.Y + bbox.Height),
                new PointF(bbox.X, bbox.Y + bbox.Height / 2)
            };
        }

        /// <summary>
        /// Returns a polygon that contains beveled square.
        /// </summary>
        /// <param name="bbox">The bounding box of a beveled square.</param>
        /// <returns>A polygon that contains beveled square.</returns>
        private PointF[] GetBeveledSquarePolygon(RectangleF bbox)
        {
            const float t1 = 1 / 4f;
            const float t2 = 1 - t1;
            return new PointF[]{
                new PointF(bbox.X + bbox.Width * t1, bbox.Y),
                new PointF(bbox.X + bbox.Width * t2, bbox.Y),
                new PointF(bbox.X + bbox.Width, bbox.Y + bbox.Height * t1),
                new PointF(bbox.X + bbox.Width, bbox.Y + bbox.Height * t2),
                new PointF(bbox.X + bbox.Width * t2, bbox.Y + bbox.Height),
                new PointF(bbox.X + bbox.Width * t1, bbox.Y + bbox.Height),
                new PointF(bbox.X, bbox.Y + bbox.Height * t2),
                new PointF(bbox.X, bbox.Y + bbox.Height * t1)
            };
        }

        /// <summary>
        /// Returns a polygon that contains cross.
        /// </summary>
        /// <param name="bbox">The bounding box of a cross.</param>
        /// <returns>A polygon that contains cross.</returns>
        private PointF[] GetCrossPolygon(RectangleF bbox)
        {
            const float t1 = 1 / 3f;
            const float t2 = 1 - t1;
            return new PointF[]{
                new PointF(bbox.X + bbox.Width * t1, bbox.Y),
                new PointF(bbox.X + bbox.Width * t2, bbox.Y),
                new PointF(bbox.X + bbox.Width * t2, bbox.Y + bbox.Height * t1),

                new PointF(bbox.X + bbox.Width, bbox.Y + bbox.Height * t1),
                new PointF(bbox.X + bbox.Width, bbox.Y + bbox.Height * t2),
                new PointF(bbox.X + bbox.Width * t2, bbox.Y + bbox.Height * t2),

                new PointF(bbox.X + bbox.Width * t2, bbox.Y + bbox.Height),
                new PointF(bbox.X + bbox.Width * t1, bbox.Y + bbox.Height),
                new PointF(bbox.X + bbox.Width * t1, bbox.Y + bbox.Height * t2),

                new PointF(bbox.X, bbox.Y + bbox.Height * t2),
                new PointF(bbox.X, bbox.Y + bbox.Height * t1),
                new PointF(bbox.X + bbox.Width * t1, bbox.Y + bbox.Height * t1)
            };
        }

        /// <summary>
        /// Returns a bounding box of barcode cell (dot).
        /// </summary>
        /// <param name="x">The X-coordinate of dot.</param>
        /// <param name="y">The Y-coordinate of dot.</param>
        private RectangleF GetDotBoundingBox(int x, int y)
        {
            return new RectangleF(x + (1 - DotDiameter) / 2, y + (1 - DotDiameter) / 2, DotDiameter, DotDiameter);
        }

        #endregion

        #endregion

    }
}
