using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledFrame : TiledElement
    {
        /// <summary>
        /// Gets the local if of a tile.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets how long the frame should be displayed, in milliseconds.
        /// </summary>
        public int Duration { get; private set; }

        internal TiledFrame(XElement xFrame)
            : base(xFrame)
        {
        }

        protected override void ReadXml(XElement xFrame)
        {
            Id = (int)xFrame.Attribute("tileid");
            Duration = (int)xFrame.Attribute("duration");
        }
    }
}
