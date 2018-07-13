using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledGroup : AbstractTiledLayer
    {
        /// <summary>
        /// Gets the custom properties defined by the group.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; }

        /// <summary>
        /// Gets the tile layers stored in the group.
        /// </summary>
        public List<TiledLayer> Layers { get; private set; } = new List<TiledLayer>();

        /// <summary>
        /// Gets the object groups stored in the group.
        /// </summary>
        public List<TiledObjectGroup> Objects { get; private set; } = new List<TiledObjectGroup>();

        /// <summary>
        /// Gets the image layers stored in the group.
        /// </summary>
        public List<TiledImageLayer> Images { get; private set; } = new List<TiledImageLayer>();

        /// <summary>
        /// Gets the nested groups.
        /// </summary>
        public List<TiledGroup> Groups { get; private set; } = new List<TiledGroup>();

        internal TiledGroup(XElement xGroup)
            : base(xGroup)
        {
        }

        internal TiledGroup(XElement xGroup, ITiledLayer parent)
            : base(xGroup, parent)
        {
        }

        protected override void ReadXml(XElement xGroup)
        {
            Name = (string)xGroup.Attribute("name");
            _offsetX = (int?)xGroup.Attribute("offsetx") ?? 0;
            _offsetY = (int?)xGroup.Attribute("offsety") ?? 0;
            _opacity = (float?)xGroup.Attribute("opacity") ?? 1;
            var visible = xGroup.Attribute("visible");
            _visible = visible == null ? true : (int)visible == 1;
            var properties = xGroup.Element("properties");
            Properties = ReadProperties(properties);

            foreach(var xLayer in xGroup.Elements("layer"))
                Layers.Add(new TiledLayer(xLayer, this));

            foreach (var xObjectGroup in xGroup.Elements("objectgroup"))
                Objects.Add(new TiledObjectGroup(xObjectGroup, this));

            foreach (var xImage in xGroup.Elements("imagelayer"))
                Images.Add(new TiledImageLayer(xImage, this));

            foreach (var childGroup in xGroup.Elements("group"))
                Groups.Add(new TiledGroup(childGroup, this));
        }
    }
}
