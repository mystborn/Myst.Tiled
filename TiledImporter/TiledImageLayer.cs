using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledImageLayer : AbstractTiledLayer
    {
        /// <summary>
        /// Gets the custom properties defined by the layer.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; }

        /// <summary>
        /// Gets the images stored in the layer.
        /// </summary>
        public List<TiledImage> Images { get; private set; } = new List<TiledImage>();

        internal TiledImageLayer(XElement xImageLayer)
            : base(xImageLayer)
        {
        }

        internal TiledImageLayer(XElement xImageLayer, ITiledLayer parent)
            : base(xImageLayer, parent)
        {
        }

        protected override void ReadXml(XElement xImageLayer)
        {
            Name = (string)xImageLayer.Attribute("name");
            _offsetX = (int?)xImageLayer.Attribute("offsetx") ?? 0;
            _offsetY = (int?)xImageLayer.Attribute("offsety") ?? 0;
            _opacity = (float?)xImageLayer.Attribute("opacity") ?? 1f;
            var visible = xImageLayer.Attribute("visible");
            _visible = visible == null ? true : (int)visible == 1;

            var properties = xImageLayer.Element("properties");
            Properties = ReadProperties(properties);

            foreach (var xImage in xImageLayer.Elements("image"))
                Images.Add(new TiledImage(xImage));
        }
    }
}
