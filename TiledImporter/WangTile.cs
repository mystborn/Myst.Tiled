using System.Xml.Linq;

namespace Myst.Tiled
{
    public class WangTile : TiledElement
    {
        /// <summary>
        /// Gets the id of the tile.
        /// </summary>
        public int TileId { get; private set; }

        /// <summary>
        /// Gets the wang id.
        /// </summary>
        public uint WangId { get; private set; }

        internal WangTile(XElement xWangTile)
            : base(xWangTile)
        {
        }

        protected override void ReadXml(XElement xWangTile)
        {
            TileId = (int)xWangTile.Attribute("tileid");
            WangId = (uint)xWangTile.Attribute("wangid");
        }
    }
}
