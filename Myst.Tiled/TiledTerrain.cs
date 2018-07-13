using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledTerrain : TiledElement
    {
        /// <summary>
        /// Gets the name of the terrain.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the local tile id of the tile that represents the terrain visually.
        /// </summary>
        public int Tile { get; private set; }

        /// <summary>
        /// Gets the custom properties assigned to the terrain.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; }

        internal TiledTerrain(XElement xTerrain)
            : base(xTerrain)
        {
        }

        protected override void ReadXml(XElement xTerrain)
        {
            Name = (string)xTerrain.Attribute("name");
            Tile = (int)xTerrain.Attribute("tile");
            Properties = ReadProperties(xTerrain.Element("properties"));
        }
    }
}