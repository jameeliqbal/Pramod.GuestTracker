using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using Vintasoft.Barcode;
using Vintasoft.Barcode.BarcodeStructure;

namespace BarcodeClient
{
    /// <summary>
    /// A renderer that highlights the barcode structure.
    /// </summary>
    public class BarcodeStructureHighlightRenderer : MatrixBarcodeRendererBase
    {

        #region Fields

        /// <summary>
        /// The path that is used for drawing the black cells of data layer.
        /// </summary>
        GraphicsPath _dataLayerBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of data layer.
        /// </summary>
        GraphicsPath _dataLayerWhitePath;

        /// <summary>
        /// The path that is used for drawing the black cells of format information.
        /// </summary>
        GraphicsPath _formatInformationBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of format information.
        /// </summary>
        GraphicsPath _formatInformationWhitePath;

        /// <summary>
        /// The path that is used for drawing the black cells of search pattern.
        /// </summary>
        GraphicsPath _searchPatternBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of search pattern.
        /// </summary>
        GraphicsPath _searchPatternWhitePath;

        /// <summary>
        /// The path that is used for drawing the black cells of timing pattern.
        /// </summary>
        GraphicsPath _timingPatternBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of timing pattern.
        /// </summary>
        GraphicsPath _timingPatternWhitePath;

        /// <summary>
        /// The path that is used for drawing the black cells of alignment pattern.
        /// </summary>
        GraphicsPath _alignmentPatternsBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of alignment pattern.
        /// </summary>
        GraphicsPath _alignmentPatternsWhitePath;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeStructureHighlightRenderer"/> class.
        /// </summary>
        public BarcodeStructureHighlightRenderer()
        {
            HiQuality = false;
            Modulation = 0.5f;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Renders the barcode.
        /// </summary>
        /// <seealso cref="BarcodeElement" />
        public override void Render()
        {
            // create paths
            _alignmentPatternsBlackPath = new GraphicsPath();
            _alignmentPatternsWhitePath = new GraphicsPath();
            _dataLayerBlackPath = new GraphicsPath();
            _dataLayerWhitePath = new GraphicsPath();
            _searchPatternBlackPath = new GraphicsPath();
            _searchPatternWhitePath = new GraphicsPath();
            _timingPatternBlackPath = new GraphicsPath();
            _timingPatternWhitePath = new GraphicsPath();
            _formatInformationBlackPath = new GraphicsPath();
            _formatInformationWhitePath = new GraphicsPath();
            _alignmentPatternsBlackPath.FillMode = FillMode.Winding;
            _alignmentPatternsWhitePath.FillMode = FillMode.Winding;

            // render barcode
            base.Render();

            // draw barcode on graphics
            using (SolidBrush brush = new SolidBrush(GetLightenColor(SearchPatternsColor)))
                Graphics.FillPath(brush, _searchPatternWhitePath);
            using (SolidBrush brush = new SolidBrush(SearchPatternsColor))
                Graphics.FillPath(brush, _searchPatternBlackPath);
            using (SolidBrush brush = new SolidBrush(GetLightenColor(TimingPatternsColor)))
                Graphics.FillPath(brush, _timingPatternWhitePath);
            using (SolidBrush brush = new SolidBrush(TimingPatternsColor))
                Graphics.FillPath(brush, _timingPatternBlackPath);
            using (SolidBrush brush = new SolidBrush(GetLightenColor(AlignmentPatternsColor)))
                Graphics.FillPath(brush, _alignmentPatternsWhitePath);
            using (SolidBrush brush = new SolidBrush(AlignmentPatternsColor))
                Graphics.FillPath(brush, _alignmentPatternsBlackPath);
            using (SolidBrush brush = new SolidBrush(GetLightenColor(FormatInformationColor)))
                Graphics.FillPath(brush, _formatInformationWhitePath);
            using (SolidBrush brush = new SolidBrush(FormatInformationColor))
                Graphics.FillPath(brush, _formatInformationBlackPath);
            using (SolidBrush brush = new SolidBrush(GetLightenColor(DataLayerColor)))
                Graphics.FillPath(brush, _dataLayerWhitePath);
            using (SolidBrush brush = new SolidBrush(DataLayerColor))
                Graphics.FillPath(brush, _dataLayerBlackPath);

            // free resources
            _alignmentPatternsBlackPath.Dispose();
            _alignmentPatternsWhitePath.Dispose();
            _dataLayerBlackPath.Dispose();
            _dataLayerWhitePath.Dispose();
            _searchPatternBlackPath.Dispose();
            _searchPatternWhitePath.Dispose();
            _timingPatternBlackPath.Dispose();
            _timingPatternWhitePath.Dispose();
            _formatInformationBlackPath.Dispose();
            _formatInformationWhitePath.Dispose();
        }

        /// <summary>
        /// Renders the barcode element.
        /// </summary>
        /// <param name="barcodeElement">The barcode element to render.</param>
        /// <param name="x">The X-coordinate, where barcode element must be rendered.</param>
        /// <param name="y">The Y-coordinate, where barcode element must be rendered.</param>
        protected override void RenderBarcodeElement(BarcodeElement barcodeElement, int x, int y)
        {
            bool isMatrixCell = false;
            if (barcodeElement == BarcodeElements.BlackCell)
                isMatrixCell = true;
            else if (barcodeElement == BarcodeElements.WhiteCell)
                isMatrixCell = true;

            if (isMatrixCell)
            {
                GraphicsPath path = SelectPath(x, y);
                path.AddRectangle(new Rectangle(x, y, 1, 1));
            }
            else
            {
                base.RenderBarcodeElement(barcodeElement, x, y);
            }
        }

        /// <summary>
        /// Selects the path for cell at specified location.
        /// </summary>
        /// <param name="x">The X-coordinate of element.</param>
        /// <param name="y">The Y-coordinate of element.</param>
        protected GraphicsPath SelectPath(int x, int y)
        {
            bool isBlack = false;
            if (GetMatrixElement(x, y) == BarcodeElements.BlackCell)
                isBlack = true;

            if (IsAlignmentPattern(x, y))
            {
                if (isBlack)
                    return _alignmentPatternsBlackPath;
                return _alignmentPatternsWhitePath;
            }
            if (IsSearchPattern(x, y))
            {
                if (isBlack)
                    return _searchPatternBlackPath;
                return _searchPatternWhitePath;
            }
            if (IsTimingPattern(x, y))
            {
                if (isBlack)
                    return _timingPatternBlackPath;
                return _timingPatternWhitePath;
            }
            if (IsDataLayer(x, y))
            {
                if (isBlack)
                    return _dataLayerBlackPath;
                return _dataLayerWhitePath;
            }
            if (IsFormatInformation(x, y))
            {
                if (isBlack)
                    return _formatInformationBlackPath;
                return _formatInformationWhitePath;
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Structure Highlight";
        }

        #endregion

    }
}
