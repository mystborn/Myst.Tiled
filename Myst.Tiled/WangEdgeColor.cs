using System.Xml.Linq;

namespace Myst.Tiled
{
    public class WangEdgeColor : TiledElement
    {
        /// <summary>
        /// Gets the name of the edge color.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the color defined by the edge.
        /// </summary>
        public TiledColor Color { get; private set; }

        /// <summary>
        /// Gets the id of the tile that represents the edge.
        /// </summary>
        public int Tile { get; private set; }

        /// <summary>
        /// Gets the probability that the edge is chosen over others given multiple options.
        /// </summary>
        public float Probability { get; private set; }

        internal WangEdgeColor(XElement xWangEdge)
            : base(xWangEdge)
        {
        }

        protected override void ReadXml(XElement xWangEdge)
        {
            Name = (string)xWangEdge.Attribute("name");
            Color = new TiledColor((string)xWangEdge.Attribute("color"));
            Tile = (int)xWangEdge.Attribute("tile");
            Probability = (float)xWangEdge.Attribute("probability");
        }
    }
}
