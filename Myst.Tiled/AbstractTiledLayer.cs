using System.Xml.Linq;

namespace Myst.Tiled
{
    public abstract class AbstractTiledLayer : TiledElement, ITiledLayer
    {
        protected int _offsetX;
        protected int _offsetY;
        protected float _opacity;
        protected bool _visible;

        /// <summary>
        /// Gets the parent layer.
        /// </summary>
        public ITiledLayer Parent { get; }

        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the rendering offset of the x axis in pixels.
        /// </summary>
        public int OffsetX => _offsetX + (Parent == null ? 0 : Parent.OffsetX);

        /// <summary>
        /// Gets the rendering offset of the y axis in pixels.
        /// </summary>
        public int OffsetY => _offsetY + (Parent == null ? 0 : Parent.OffsetY);

        /// <summary>
        /// Gets the opacity of the layer as a value from 0 to 1;
        /// </summary>
        public float Opacity => _opacity * (Parent == null ? 1 : Parent.Opacity);

        /// <summary>
        /// Gets whether the layer is shown or hidden.
        /// </summary>
        public bool Visible => _visible && (Parent == null ? true : Parent.Visible);

        internal AbstractTiledLayer(XElement xLayer)
            : base(xLayer)
        {
            Parent = null;
        }

        internal AbstractTiledLayer(XElement xLayer, ITiledLayer parent)
            : base(xLayer)
        {
            Parent = parent;
        }
    }
}
