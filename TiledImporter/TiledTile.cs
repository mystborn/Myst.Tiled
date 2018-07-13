using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledTile : TiledElement
    {
        /// <summary>
        /// Gets the local tile id within this tiles set.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the type of the tile. (Only used by tile objects).
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Defines the terrain type of each corner of the tile.
        /// </summary>
        public string Terrain { get; private set; }

        /// <summary>
        /// Gets the percentage chance indicating the probability that this tile is chosen when competing against others.
        /// </summary>
        public float Probability { get; private set; }

        /// <summary>
        /// Gets the custom properties assigned to the tile.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; } = new List<TiledProperty>();

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        public TiledImage EmbeddedImage { get; private set; } = null;

        /// <summary>
        /// Unknown purpose.
        /// </summary>
        public TiledObjectGroup Objects { get; private set; } = null;

        /// <summary>
        /// Gets a list of animation frames.
        /// </summary>
        public List<TiledFrame> Animation { get; private set; } = new List<TiledFrame>();

        internal TiledTile(XElement xTile)
            : base(xTile)
        {
        }

        protected override void ReadXml(XElement xTile)
        {
            Id = (int)xTile.Attribute("id");
            Type = (string)xTile.Attribute("type");
            Terrain = (string)xTile.Attribute("terrain") ?? "";
            Probability = (float?)xTile.Attribute("probability") ?? 1f;

            var properties = xTile.Element("properties");
            Properties = ReadProperties(properties);

            var image = xTile.Element("image");
            if (image != null)
                EmbeddedImage = new TiledImage(image);

            var objects = xTile.Element("objectgroup");
            if (objects != null)
                Objects = new TiledObjectGroup(objects);

            var animation = xTile.Element("animation");
            if (animation != null)
            {
                foreach (var frame in animation.Elements("frame"))
                    Animation.Add(new TiledFrame(frame));
            }
        }
    }
}
