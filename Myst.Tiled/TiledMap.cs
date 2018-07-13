using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledMap : TiledElement
    {
        /// <summary>
        /// The file-format version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// The version of Tiled used to save the file.
        /// </summary>
        public string TiledVersion { get; private set; }

        /// <summary>
        /// The orientation of the map.
        /// </summary>
        public OrientationType Orientation { get; private set; }

        /// <summary>
        /// The order in which tiles on tile layers are rendered.
        /// </summary>
        public RenderOrderType? RenderOrder { get; private set; } = null;

        /// <summary>
        /// The map width in tiles.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The map height in tiles.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The width of a tile in pixels.
        /// </summary>
        public int TileWidth { get; private set; }

        /// <summary>
        /// The height of a tile in pixels.
        /// </summary>
        public int TileHeight { get; private set; }

        /// <summary>
        /// If the map is hexagonal, determines the width or height (depending on the StaggerAxis) of the tiles edge in pixels.
        /// </summary>
        public int? HexSideLength { get; private set; } = null;

        /// <summary>
        /// For staggered and hexagonal maps, determines which axis is staggered.
        /// </summary>
        public StaggerAxisType? StaggerAxis { get; private set; } = null;

        /// <summary>
        /// For staggered an hexagonal maps, determines whether the even or odd indexes along the staggered axis are shifted.
        /// </summary>
        public StaggerIndexType? StaggerIndex { get; private set; } = null;

        /// <summary>
        /// The background color of the map.
        /// </summary>
        public TiledColor BackgroundColor { get; private set; }

        /// <summary>
        /// Whether the has an infinite amount of space or not.
        /// </summary>
        public bool Infinite { get; private set; }

        /// <summary>
        /// Stores the next available id for new objects.
        /// </summary>
        public int? NextObjectId { get; private set; }

        /// <summary>
        /// Gets the custom properties defined by the map.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; } = new List<TiledProperty>();

        /// <summary>
        /// Gets the tilesets used in the map.
        /// </summary>
        public List<TiledTileset> Tilesets { get; private set; } = new List<TiledTileset>();

        /// <summary>
        /// Gets the layers dedicated to Tiles.
        /// </summary>
        public List<TiledLayer> Layers { get; private set; } = new List<TiledLayer>();

        /// <summary>
        /// Gets the layers dedicated to objects.
        /// </summary>
        public List<TiledObjectGroup> Objects { get; private set; } = new List<TiledObjectGroup>();

        /// <summary>
        /// Gets the layers dedicated to images. (Semi-Supported)
        /// </summary>
        public List<TiledImageLayer> Images { get; private set; } = new List<TiledImageLayer>();

        /// <summary>
        /// Gets the groups of nested layers.
        /// </summary>
        public List<TiledGroup> Groups { get; private set; } = new List<TiledGroup>();

        /// <summary>
        /// Creates a new TiledMap from a tiled file.
        /// </summary>
        /// <param name="filePath">The location of the tiled file.</param>
        public TiledMap(string filePath)
        {
            ReadXml(XDocument.Load(filePath, LoadOptions.SetBaseUri));
        }

        /// <summary>
        /// Creates a new TiledMap from a stream, given a BaseUri.
        /// </summary>
        /// <param name="input">The stream used to generate a TiledMap.</param>
        /// <param name="baseUri">The original file location for the stream.</param>
        public TiledMap(Stream input, string baseUri = "")
        {
            var xDoc = XDocument.Load(input);
            if (baseUri == "")
                baseUri = AppDomain.CurrentDomain.BaseDirectory;
            ReadXml(xDoc);
        }

        /// <summary>
        /// Creates a TiledMap from an XDocument.
        /// </summary>
        /// <param name="xDoc">The XDocument used to generate the TiledMap.</param>
        public TiledMap(XDocument xDoc)
        {
            if (xDoc.BaseUri == null)
                throw new ArgumentNullException("xDoc", "In order to create a TiledMap from an XDocument, it must have its BaseUri set.");
            ReadXml(xDoc);
        }

        private void ReadXml(XDocument xDoc)
        {
            if (xDoc == null)
                throw new ArgumentNullException("xDoc");
            if (xDoc.BaseUri == null)
                throw new ArgumentException("The XDocument used to create a TiledMap must have a BaseUri", "xDoc");

            var map = xDoc.Element("map");
            ReadXml(map);
        }

        protected override void ReadXml(XElement xMap)
        {
            Version = (string)xMap.Attribute("version");
            TiledVersion = (string)xMap.Attribute("tiledversion");

            var orientation = (string)xMap.Attribute("orientation");
            if (orientation != null)
                Orientation = OrientationParser[orientation];

            var renderOrder = (string)xMap.Attribute("renderorder");
            if (renderOrder != null)
                RenderOrder = RenderOrderParser[renderOrder];

            Width = (int)xMap.Attribute("width");
            Height = (int)xMap.Attribute("height");
            TileWidth = (int)xMap.Attribute("tilewidth");
            TileHeight = (int)xMap.Attribute("tileheight");
            HexSideLength = (int?)xMap.Attribute("hexsidelength");

            var axis = (string)xMap.Attribute("staggeraxis");
            if (axis != null)
                StaggerAxis = AxisParser[axis];

            var staggerIndex = (string)xMap.Attribute("staggerindex");
            if (staggerIndex != null)
                StaggerIndex = StaggerIndexParser[staggerIndex];

            BackgroundColor = new TiledColor((string)xMap.Attribute("backgroundcolor"));
            Infinite = ((int)xMap.Attribute("infinite")) == 0 ? false : true;
            NextObjectId = (int?)xMap.Attribute("nextobjectid");

            Properties = ReadProperties(xMap.Element("properties"));

            foreach (var tileset in xMap.Elements("tileset"))
                Tilesets.Add(new TiledTileset(tileset));

            foreach (var xLayer in xMap.Elements("layer"))
                Layers.Add(new TiledLayer(xLayer));

            foreach (var xObjectGroup in xMap.Elements("objectgroup"))
                Objects.Add(new TiledObjectGroup(xObjectGroup));

            foreach (var xImageLayer in xMap.Elements("imagelayer"))
                Images.Add(new TiledImageLayer(xImageLayer));

            foreach (var xGroup in xMap.Elements("group"))
                Groups.Add(new TiledGroup(xGroup));
        }
    }
}
