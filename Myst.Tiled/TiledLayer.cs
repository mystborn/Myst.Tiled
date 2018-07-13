using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledLayer : AbstractTiledLayer
    {
        /// <summary>
        /// Gets the width of the layer in tiles.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the layer in tiles.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the custom properties defined by the layer.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; }

        /// <summary>
        /// Gets the tile data stored inside of the layer.
        /// </summary>
        public TiledData Data { get; private set; }

        internal TiledLayer(XElement xLayer)
            : base(xLayer)
        {
        }

        internal TiledLayer(XElement xLayer, ITiledLayer parent)
            : base(xLayer, parent)
        {
        }

        protected override void ReadXml(XElement xLayer)
        {
            Name = (string)xLayer.Attribute("name");
            Width = (int)xLayer.Attribute("width");
            Height = (int)xLayer.Attribute("height");
            _opacity = (float?)xLayer.Attribute("opacity") ?? 1f;
            _offsetX = (int?)xLayer.Attribute("offsetx") ?? 0;
            _offsetY = (int?)xLayer.Attribute("offsety") ?? 0;

            var visible = xLayer.Attribute("visible");
            _visible = visible == null ? true : (int)visible == 1;

            var properties = xLayer.Element("properties");
            Properties = ReadProperties(properties);

            Data = new TiledData(xLayer.Element("data"));
        }
    }
}
