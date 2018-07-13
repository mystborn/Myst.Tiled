using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledText : TiledElement
    {
        /// <summary>
        /// The character data represented by the text object.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the font family used.
        /// </summary>
        public string FontFamily { get; private set; }

        /// <summary>
        /// Gets the size of the text in pixels.
        /// </summary>
        public int PixelSize { get; private set; }

        /// <summary>
        /// Determines whether word wrapping is enabled or not.
        /// </summary>
        public bool Wrap { get; private set; }

        /// <summary>
        /// Gets the color of the text.
        /// </summary>
        public TiledColor Color { get; private set; }

        /// <summary>
        /// Determines whether the text should be bold.
        /// </summary>
        public bool Bold { get; private set; }

        /// <summary>
        /// Determines whether the text should be italic.
        /// </summary>
        public bool Italic { get; private set; }

        /// <summary>
        /// Determines whether the text should be underlined.
        /// </summary>
        public bool Underline { get; private set; }

        /// <summary>
        /// Determines whether the text should be striked out.
        /// </summary>
        public bool StrikeOut { get; private set; }

        /// <summary>
        /// Determines whether the text should be rendered with kerning.
        /// </summary>
        public bool Kerning { get; private set; }

        /// <summary>
        /// Gets the horizontal alignment of the text.
        /// </summary>
        public HorizontalAlignment HAlign { get; private set; }

        /// <summary>
        /// Gets the vertical alignment of the text.
        /// </summary>
        public VerticalAlignment VAlign { get; private set; }
        
        internal TiledText(XElement xText) 
            : base(xText)
        {
        }

        protected override void ReadXml(XElement xText)
        {
            Text = xText.Value;
            FontFamily = (string)xText.Attribute("fontfamily") ?? "sans-serif";
            PixelSize = (int?)xText.Attribute("pixelsize") ?? 16;
            Wrap = ((int?)xText.Attribute("wrap") ?? 0) == 1;
            Color = new TiledColor((string)xText.Attribute("color"));
            Bold = ((int?)xText.Attribute("bold") ?? 0) == 1;
            Italic = ((int?)xText.Attribute("italic") ?? 0) == 1;
            Underline = ((int?)xText.Attribute("underline") ?? 0) == 1;
            StrikeOut = ((int?)xText.Attribute("strikeout") ?? 0) == 1;
            Kerning = ((int?)xText.Attribute("kerning") ?? 0) == 1;
            var halign = (string)xText.Attribute("halign");
            HAlign = halign == null ? HorizontalAlignment.Left : HorizontalAlignmentParser[halign];
            var valign = (string)xText.Attribute("valign");
            VAlign = valign == null ? VerticalAlignment.Top : VerticalAlignmentParser[valign];
        }
    }
}
