using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledDataTile : TiledElement
    {
        private const uint HorizontalFlip = 0x80000000;
        private const uint VerticalFlip = 0x40000000;
        private const uint DiagonalFlip = 0x20000000;

        /// <summary>
        /// Gets the unmodified data stored in this tile. Used with embedded images.
        /// </summary>
        public uint FullData { get; private set; }

        /// <summary>
        /// Gets the global tile id of this tile.
        /// </summary>
        public uint Gid { get; private set; }

        /// <summary>
        /// Determines whether this tile was flipped horizontally.
        /// </summary>
        public bool FlippedHorizontally => (FullData & HorizontalFlip) == HorizontalFlip;

        /// <summary>
        /// Determines if the tile was flipped vertically.
        /// </summary>
        public bool FlippedVertically => (FullData & VerticalFlip) == VerticalFlip;

        /// <summary>
        /// Determines if the tile was flipped diagonally.
        /// </summary>
        public bool FlippedDiagonally => (FullData & DiagonalFlip) == DiagonalFlip;

        internal TiledDataTile(uint gid)
        {
            FullData = gid;
            Gid = ClearGidFlags(FullData);
        }

        internal TiledDataTile(XElement xTile)
            : base(xTile)
        {
        }

        protected override void ReadXml(XElement xTile)
        {
            var gid = xTile.Attribute("gid");
            if (gid != null)
                FullData = (uint)gid;
            else
                FullData = 0;
            Gid = ClearGidFlags(FullData);
        }

        private uint ClearGidFlags(uint gid)
        {
            return gid & ~(HorizontalFlip | VerticalFlip | DiagonalFlip);
        }
    }
}
