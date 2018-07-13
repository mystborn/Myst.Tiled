using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledOffset : TiledElement
    {
        /// <summary>
        /// Gets the horizontal offset in pixels.
        /// </summary>
        public int XOffset { get; private set; }

        /// <summary>
        /// Gets the vertical offset in pixels.
        /// </summary>
        public int YOffset { get; private set; }

        public static TiledOffset Zero
        {
            get => new TiledOffset() { XOffset = 0, YOffset = 0 };
        }

        private TiledOffset() { }

        internal TiledOffset(XElement element)
            : base(element)
        {
        }

        protected override void ReadXml(XElement element)
        {
            XOffset = (int)element.Attribute("x");
            YOffset = (int)element.Attribute("y");
        }
    }
}
