using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledGrid : TiledElement
    {
        /// <summary>
        /// Gets the orientation of the grid for tiles in this tileset.
        /// </summary>
        public OrientationType Orientation { get; private set; }

        /// <summary>
        /// Gets the width of a grid cell.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of a grid cell.
        /// </summary>
        public int Height { get; private set; }

        internal TiledGrid(XElement element)
            : base(element)
        {
        }

        protected override void ReadXml(XElement element)
        {
            Orientation = OrientationParser[(string)element.Attribute("orientation")];
            Width = (int)element.Attribute("width");
            Height = (int)element.Attribute("height");
        }
    }
}
