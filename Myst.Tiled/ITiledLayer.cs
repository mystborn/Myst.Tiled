namespace Myst.Tiled
{
    public interface ITiledLayer
    {
        /// <summary>
        /// Gets the name of the layer.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the rendering offset of the x axis in pixels.
        /// </summary>
        int OffsetX { get; }

        /// <summary>
        /// Gets the rendering offset of the y axis in pixels.
        /// </summary>
        int OffsetY { get; }

        /// <summary>
        /// Gets the opacity of the layer as a value from 0 to 1;
        /// </summary>
        float Opacity { get; }

        /// <summary>
        /// Gets whether the layer is shown or hidden.
        /// </summary>
        bool Visible { get; }

        /// <summary>
        /// Gets the parent layer.
        /// </summary>
        ITiledLayer Parent { get; }
    }
}
