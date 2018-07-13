using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledObjectGroup : AbstractTiledLayer
    {
        /// <summary>
        /// Gets the color used to display objects in the group.
        /// </summary>
        public TiledColor Color { get; private set; }

        /// <summary>
        /// Determines in which order the objects are rendered.
        /// </summary>
        public DrawOrderType DrawOrder { get; private set; } = DrawOrderType.TopDown;

        /// <summary>
        /// Gets the custom properties defined by the group.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; }

        /// <summary>
        /// Gets the objects stored in the group.
        /// </summary>
        public List<TiledObject> Objects { get; private set; } = new List<TiledObject>();

        internal TiledObjectGroup(XElement xGroup)
            : base(xGroup)
        {
        }

        internal TiledObjectGroup(XElement xGroup, ITiledLayer parent)
            : base(xGroup, parent)
        {
        }

        protected override void ReadXml(XElement xGroup)
        {
            Name = (string)xGroup.Attribute("name");
            Color = new TiledColor((string)xGroup.Attribute("color"));
            _opacity = ((float?)xGroup.Attribute("opacity")) ?? 1f;

            var visible = xGroup.Attribute("visible");
            _visible = visible == null ? true : (int)visible == 1;

            _offsetX = ((int?)xGroup.Attribute("offsetx")) ?? 0;
            _offsetY = ((int?)xGroup.Attribute("offsety")) ?? 0;
            var drawOrder = (string)xGroup.Attribute("draworder");
            DrawOrder = drawOrder == null ? DrawOrderType.TopDown : DrawOrderParser[drawOrder];

            Properties = ReadProperties(xGroup.Element("properties"));

            foreach (var obj in xGroup.Elements("object"))
                Objects.Add(new TiledObject(obj));
        }
    }
}
