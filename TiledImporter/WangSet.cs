using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class WangSet : TiledElement
    {
        /// <summary>
        /// Gets the name of the set.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the id of the tile representing this set.
        /// </summary>
        public int Tile { get; private set; }

        /// <summary>
        /// Gets a list of colors that can be used to define the corner of a wang tile.
        /// </summary>
        public List<WangCornerColor> CornerColors { get; private set; } = new List<WangCornerColor>();

        /// <summary>
        /// Gets a list of colors that can be used to define the edge of a wang tile.
        /// </summary>
        public List<WangEdgeColor> EdgeColors { get; private set; } = new List<WangEdgeColor>();

        /// <summary>
        /// Gets a list of defined wang tiles.
        /// </summary>
        public List<WangTile> WangTiles { get; private set; } = new List<WangTile>();

        internal WangSet(XElement xWang)
            : base(xWang)
        {
        }

        protected override void ReadXml(XElement xWang)
        {
            Name = (string)xWang.Attribute("name");
            Tile = (int)xWang.Attribute("tile");

            foreach (var xCorner in xWang.Elements("wangcornercolor"))
                CornerColors.Add(new WangCornerColor(xCorner));

            foreach (var xEdge in xWang.Elements("wangedgecolor"))
                EdgeColors.Add(new WangEdgeColor(xEdge));

            foreach (var xTile in xWang.Elements("wangtile"))
                WangTiles.Add(new WangTile(xTile));
        }
    }
}
