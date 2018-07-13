using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledImage : TiledElement
    {
        /// <summary>
        /// If this contains an embedded image, get it's format (i.e. png, gif, jpg, bmp).
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// Gets the source image used by this set.
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// Defines a color to be treated as transparent.
        /// </summary>
        public TiledColor Transparent { get; private set; }

        /// <summary>
        /// Gets the width of the image in pixels.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the image in pixels.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// If this contains an embedded image, it's data is stored here.
        /// </summary>
        public TiledData EmbeddedData { get; private set; } = null;

        internal TiledImage(XElement xImage)
            : base(xImage)
        {
        }

        protected override void ReadXml(XElement xImage)
        {
            Format = (string)xImage.Attribute("format");
            Source = (string)xImage.Attribute("source");
            Transparent = new TiledColor((string)xImage.Attribute("trans"));
            Width = (int)xImage.Attribute("width");
            Height = (int)xImage.Attribute("height");
            var data = xImage.Element("data");
            if (data != null)
                EmbeddedData = new TiledData(data);
        }
    }
}
