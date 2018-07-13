using System.Xml.Linq;

namespace Myst.Tiled
{
    public class WangCornerColor : TiledElement
    {
        /// <summary>
        /// Gets the name of the corner color.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the color defined by the corner.
        /// </summary>
        public TiledColor Color { get; private set; }

        /// <summary>
        /// Gets the id of the tile that represents the corner.
        /// </summary>
        public int Tile { get; private set; }

        /// <summary>
        /// Gets the probability that the corner is chosen over others given multiple options.
        /// </summary>
        public float Probability { get; private set; }

        internal WangCornerColor(XElement xWangCorner)
            : base(xWangCorner)
        {
        }

        protected override void ReadXml(XElement xWangCorner)
        {
            Name = (string)xWangCorner.Attribute("name");
            Color = new TiledColor((string)xWangCorner.Attribute("color"));
            Tile = (int)xWangCorner.Attribute("tile");
            Probability = (float)xWangCorner.Attribute("probability");
        }
    }
}
